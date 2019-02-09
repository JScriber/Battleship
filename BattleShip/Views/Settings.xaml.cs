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
        private int positionX;
        private int positionY;
        private int shipWidth;
        private int shipHeight;
        private bool rotation;
        private String selectedConfiguration;

        private GameBuilder gameBuilder;
        private ShipBuilder shipBuilder;

        private List<ShipConfiguration> configurations;
        private List<Ship> userShips;
        #endregion

        #region Properties
        public int MapWidth
        {
            get { return shipView.MapWidth; }
            set
            {
                this.shipView.MapWidth = value;
            }
        }

        public int MapHeight
        {
            get { return shipView.MapHeight; }
            set
            {
                this.shipView.MapHeight = value;
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

        public List<Ship> UserShips
        {
            get { return userShips; }
            set { userShips = value; }
        }

        public String SelectedConfiguration
        {
            get { return selectedConfiguration; }
            set { selectedConfiguration = value; }
        }
        
        public GameBuilder GameBuilder
        {
            get { return gameBuilder; }
            set { gameBuilder = value; }
        }

        public ShipBuilder ShipBuilder
        {
            get { return shipBuilder; }
            set { shipBuilder = value; }
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

            this.GameBuilder = new GameBuilder();
            this.ShipBuilder = new ShipBuilder(new Dimension(MapWidth, MapHeight));

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
            Dimension dimension = new Dimension(8, 8);

            // Builders.
            var gb = new GameBuilder();
            var sb = new ShipBuilder(dimension);

            // TODO: View modifies the configurations.
            this.UserShips = new List<Ship>()
            {
                sb.FromConfiguration(configurations[0], 1, 1, false),
                sb.FromConfiguration(configurations[1], 3, 2, true),
                sb.FromConfiguration(configurations[2], 4, 3, true),
            };

            // Random placement for IA.
            Map robotMap = sb.RandomFromConfigurations(configurations);

            return gb.CreateGame(configurations, this.UserShips, robotMap, dimension);
        }

        private void ResizeMap()
        {
            
        }

        private void BindListviews()
        {
            //this.shipListAdded.ItemsSource = ships;
        }

        private void AddShip(ShipType type, int width, int height)
        {
            Dimension dimension = new Dimension(width, height);
            ShipConfiguration configuration = new ShipConfiguration(type, dimension, 1);
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
        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            this.StartPlaying();
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

        private void Add_Ship(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Add a ship");
            System.Console.WriteLine("X: " + this.PositionX);
            System.Console.WriteLine("Y: " + this.PositionY);
            System.Console.WriteLine("Width: " + this.ShipWidth);
            System.Console.WriteLine("Height: " + this.ShipHeight);
            System.Console.WriteLine("Rotated: " + this.Rotation);

        }
        #endregion

        #region Property changed implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
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
