using BattleShip.Controllers;
using BattleShip.Models;
using BattleShip.Models.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Settings : Page
    {
        #region Variables
        //ObservableCollection<Game> games = new ObservableCollection<Game>();
        ObservableCollection<Ship> ships = new ObservableCollection<Ship>();
        //ObservableCollection<Player> players = new ObservableCollection<Player>();
        //ObservableCollection<Map> map = new ObservableCollection<Map>();
        //ObservableCollection<Cell> coordinates = new ObservableCollection<Cell>();
        //ObservableCollection<Dimension> dimensions = new ObservableCollection<Dimension>();
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
        #endregion

        #region Properties
        public int MapWidth
        {
            get { return mapWidth; }
            set
            {
                mapWidth = value;
                ResizeMap(this.gameGridLeft);
                dimension.Width = value;
            }
        }

        public int MapHeight
        {
            get { return mapHeight; }
            set
            {
                mapHeight = value;
                ResizeMap(this.gameGridLeft);
                dimension.Height = value;
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
            this.Gb = new GameBuilder();
            this.Dimension = new Dimension(MapWidth, MapHeight);
            this.Sb = new ShipBuilder(dimension);

            InitializeComponent();

            BindListviews();
            this.DataContext = this;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private Game GenGame()
        {
            Dimension dimension = new Dimension(8, 8);

            // Builders.
            var gb = new GameBuilder();
            var sb = new ShipBuilder(dimension);

            // Default configurations modified by the user.
            List<ShipConfiguration> configurations = new List<ShipConfiguration>()
            {
                new ShipConfiguration(Models.ShipType.Destroyer, new Dimension(2, 1), 1),
                new ShipConfiguration(Models.ShipType.Cruiser, new Dimension(3, 1), 1),
                new ShipConfiguration(Models.ShipType.Submarine, new Dimension(3, 1), 1),
                new ShipConfiguration(Models.ShipType.BattleShip, new Dimension(4, 1), 1),
                new ShipConfiguration(Models.ShipType.Carrier, new Dimension(5, 1), 1),
            };

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

            for (int i = 0; i < this.MapWidth; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                gridName.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < this.MapHeight; i++)
            {
                RowDefinition row = new RowDefinition();
                gridName.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.MapWidth; i++)
                {
                    for (int j = 0; j < this.MapHeight; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            Button btn = new Button();
                            btn.Content = "X:" + i + "Y:" + j;
                            Grid.SetColumn(btn, i);
                            Grid.SetRow(btn, j);

                            gridName.Children.Add(btn);
                        }));
                    }
                }
            });

        }

        private void BindListviews()
        {
            this.shipListAdded.ItemsSource = ships;
        }

        private void AddShip(ShipType type, int width, int height)
        {
            Dimension dimension = new Dimension(width, height);
            ShipConfiguration configuration = new ShipConfiguration(type, dimension, 1);
            
            ConfigurationList.Add(configuration);
            var lC = ConfigurationList.Last();

            ships.Add(sb.FromConfiguration(lC, this.PositionX, this.PositionY, this.Rotation));
        }

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
        #endregion

        private void ShipListAdded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
