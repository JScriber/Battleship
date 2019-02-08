using BattleShip.Controllers;
using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BattleShip.Views
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Page, INotifyPropertyChanged
    {
        #region Variables
        //ObservableCollection<Game> games = new ObservableCollection<Game>();
        ObservableCollection<Ship> ships = new ObservableCollection<Ship>();
        //ObservableCollection<Player> players = new ObservableCollection<Player>();
        //ObservableCollection<Map> map = new ObservableCollection<Map>();
        //ObservableCollection<Cell> coordinates = new ObservableCollection<Cell>();
        //ObservableCollection<Dimension> dimensions = new ObservableCollection<Dimension>();
        #endregion

        #region Constants
        // Limits.
        private const int MIN_MAP_SIZE = 1;
        private const int MAX_MAP_SIZE = 35;

        // Default sizes.
        private const int DEFAULT_MAP_WIDTH = 5;
        private const int DEFAULT_MAP_HEIGHT = 5;
        #endregion

        #region Attributs
        private int mapWidth;
        private int mapHeight;
        private bool rotation;
        private int positionX;
        private int positionY;
        private Dimension dimension;
        private List<ShipConfiguration> configurationList;
        private GameBuilder gb;
        private ShipBuilder sb;

        private int shipWidth;
        private int shipHeight;

        private List<ShipConfiguration> configurations;
        #endregion

        #region Properties
        public int MapWidth
        {
            get { return mapWidth; }
            set
            {
                if (value >= MIN_MAP_SIZE && value < MAX_MAP_SIZE)
                {
                    mapWidth = value;
                    ResizeMap(this.gameGrid);
                } else
                {
                    mapWidth = DEFAULT_MAP_WIDTH;
                }
            }
        }

        public int MapHeight
        {
            get { return mapHeight; }
            set
            {
                if (value >= MIN_MAP_SIZE && value < MAX_MAP_SIZE)
                {
                    mapHeight = value;
                    ResizeMap(this.gameGrid);
                } else
                {
                    mapHeight = DEFAULT_MAP_HEIGHT;
                }
            }
        }

        public bool Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }
        
        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }


        public int ShipWidth
        {
            get { return shipWidth; }
            set { shipWidth = value; }
        }

        public int ShipHeight
        {
            get { return shipHeight; }
            set { shipHeight = value; }
        }

        private String selectedConfiguration;

        public String SelectedConfiguration
        {
            get { return selectedConfiguration; }
            set { selectedConfiguration = value; }
        }


        public Dimension Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }

        public List<ShipConfiguration> ConfigurationList
        {
            get { return configurationList; }
            set { configurationList = value; }
        }

        public ObservableCollection<Ship> Ships { get; set; }

        public GameBuilder Gb
        {
            get { return gb; }
            set { gb = value; }
        }

        public ShipBuilder Sb
        {
            get { return sb; }
            set { sb = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            BindListviews();
            this.DataContext = this;

            // Set default sizes.
            this.MapWidth = DEFAULT_MAP_WIDTH;
            this.MapHeight = DEFAULT_MAP_HEIGHT;

            this.Gb = new GameBuilder();
            this.Dimension = new Dimension(MapWidth, MapHeight);
            this.Sb = new ShipBuilder(dimension);

            // Default configurations modified by the user.
            this.configurations = new List<ShipConfiguration>()
            {
                new ShipConfiguration(Models.ShipType.Destroyer, new Dimension(2, 1), 1),
                new ShipConfiguration(Models.ShipType.Cruiser, new Dimension(3, 1), 1),
                new ShipConfiguration(Models.ShipType.Submarine, new Dimension(3, 1), 1),
                new ShipConfiguration(Models.ShipType.BattleShip, new Dimension(4, 1), 1),
                new ShipConfiguration(Models.ShipType.Carrier, new Dimension(5, 1), 1),
            };

            this.SetTypeComboxBox();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private Game GenGame()
        {
            Dimension dimension = new Dimension(10, 10);

            // Builders.
            var gb = new GameBuilder();
            var sb = new ShipBuilder(dimension);


            // TODO: View modifies the configurations.
            List<Ship> ships = new List<Ship>()
            {
                sb.FromConfiguration(configurations[0], 1, 1, false),
                sb.FromConfiguration(configurations[1], 3, 2, true),
                sb.FromConfiguration(configurations[2], 4, 3, true),
            };

            // Random placement for IA.
            Map robotMap = sb.RandomFromConfigurations(configurations);

            return gb.CreateGame(configurations, ships, robotMap, dimension);
        }

        private void ResizeMap(Grid gridName)
        {
            gridName.Children.Clear();
            gridName.ColumnDefinitions.Clear();
            gridName.RowDefinitions.Clear();

            this.ConfigurationList = new List<ShipConfiguration>();
            ships.Clear();

            // Helper size.
            GridLength helperSize = new GridLength(18);

            // Column helper.
            ColumnDefinition colHelper = new ColumnDefinition();
            colHelper.Width = helperSize;
            gridName.ColumnDefinitions.Add(colHelper);

            for (int i = 0; i < this.MapWidth; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                gridName.ColumnDefinitions.Add(col);
            }

            // Row helper.
            RowDefinition rowHelper = new RowDefinition();
            rowHelper.Height = helperSize;
            gridName.RowDefinitions.Add(rowHelper);

            for (int i = 0; i < this.MapHeight; i++)
            {
                RowDefinition row = new RowDefinition();
                gridName.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.MapWidth + 1; i++)
                {
                    for (int j = 0; j < this.MapHeight + 1; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            if (i == 0 || j == 0)
                            {
                                TextBlock text = new TextBlock();
                                text.VerticalAlignment = VerticalAlignment.Center;
                                text.HorizontalAlignment = HorizontalAlignment.Center;

                                if (i == 0 && j != 0)
                                {
                                    text.Text = j.ToString();
                                } else
                                {
                                    if (j == 0 && i != 0)
                                    {
                                        text.Text = i.ToString();
                                    }
                                }

                                Grid.SetColumn(text, i);
                                Grid.SetRow(text, j);
                                gridName.Children.Add(text);
                            } else
                            {
                                ShipControl control = new ShipControl(i - 1, j - 1, ShipState.None);

                                Grid.SetColumn(control, i);
                                Grid.SetRow(control, j);
                                gridName.Children.Add(control);
                            }
                        }));
                    }
                }
            });

        }

        private void BindListviews()
        {
            //this.shipListAdded.ItemsSource = ships;
        }

        private void AddShip(ShipType type, int width, int height)
        {
            Dimension dimension = new Dimension(width, height);
            ShipConfiguration configuration = new ShipConfiguration(type, dimension, 1);
            
            ConfigurationList.Add(configuration);
            var lC = ConfigurationList.Last();

            ships.Add(sb.FromConfiguration(lC, this.PositionX, this.PositionY, this.Rotation));

            //TODO: Remove it
            Console.WriteLine("-----ShipsList-----");
            foreach (var ship in ships)
            {
                Console.Write("Type:" + ship.Type + " Cell[");
                foreach (var cell in ship.Cells)
                {
                    Console.Write("x::" + cell.X + " y:" + cell.Y + " ");
                }
                Console.WriteLine("]");
            }
        }

        /// <summary>
        /// Set the availables configurations in the combobox.
        /// </summary>
        private void SetTypeComboxBox()
        {
            this.formShipType.Items.Clear();

            foreach (var configuration in this.configurations)
            {
                ComboBoxItem item = new ComboBoxItem();
                Dimension dimension = configuration.Dimension;
                String name = configuration.Type + "  W" + dimension.Width + " - H" + dimension.Height;

                item.Content = name;
                item.Name = configuration.Type.ToString();

                this.formShipType.Items.Add(item);
            }
        }

        /// <summary>
        /// Changes the selected configuration in the view.
        /// </summary>
        /// <param name="configuration"></param>
        private void SetSelectedConfiguration(ShipConfiguration configuration)
        {
            this.ShipHeight = configuration.Dimension.Height;
            this.ShipWidth = configuration.Dimension.Width;

            OnPropertyChanged("ShipHeight");
            OnPropertyChanged("ShipWidth");
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartPlaying()
        {
            Game game = this.GenGame();
            (this.Parent as Window).Content = new Play(game);
        }
        #endregion

        #region Events
        private void BtnAddShipType1(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Destroyer, 2, 1);
        }
        private void BtnAddShipType2(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Cruiser, 3, 1);
        }
        private void BtnAddShipType3(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Submarine, 3, 1);
        }
        private void BtnAddShipType4(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.BattleShip, 4, 1);
        }
        private void BtnAddShipType5(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Carrier, 5, 1);
        }

        private void MapWidthTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            StartPlaying();
        }

        private void FormShipType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = this.formShipType.SelectedIndex;
            ShipConfiguration configuration = this.configurations[value];

            this.SetSelectedConfiguration(configuration);
        }

        private void ShipListAdded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Property changed implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
