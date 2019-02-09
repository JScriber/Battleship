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
using System.Text.RegularExpressions;
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
        private const int MIN_MAP_SIZE = 1;
        private const int MAX_MAP_SIZE = 35;

        private const int DEFAULT_MAP_WIDTH = 5;
        private const int DEFAULT_MAP_HEIGHT = 5;
        private const int DEFAULT_POSITION_X = 1;
        private const int DEFAULT_POSITION_Y = 1;
        private const ShipType DEFAULT_TYPE = ShipType.Carrier;
        private const int DEFAULT_WIDTH = 1;
        private const int DEFAULT_HEIGHT = 1;
        private const bool DEFAULT_ROTATED = false;
        #endregion

        #region Attributs
        private int positionX;
        private int positionY;
        private int shipWidth;
        private int shipHeight;
        private bool rotation;
        private ShipType shipType;

        private GameBuilder gameBuilder;
        private ShipBuilder shipBuilder;

        private List<ShipConfiguration> configurations;
        private ObservableCollection<Ship> userShips = new ObservableCollection<Ship>();
        #endregion

        #region Properties
        public int MapWidth
        {
            get { return shipView.Map.Dimension.Width; }
            set
            {
                if (value >= MIN_MAP_SIZE && value <= MAX_MAP_SIZE)
                {
                    this.shipView.Map.Dimension.Width = value;
                    this.RebuildMap();
                }
            }
        }

        public int MapHeight
        {
            get { return shipView.Map.Dimension.Height; }
            set
            {
                if (value >= MIN_MAP_SIZE && value <= MAX_MAP_SIZE)
                {
                    this.shipView.Map.Dimension.Height = value;
                    this.RebuildMap();
                }
            }
        }
        
        public int PositionX
        {
            get { return positionX; }
            set
            {
                if (value > 0 && value <= this.MapWidth - this.ShipWidth)
                {
                    positionX = value;
                }
            }
        }
        
        public int PositionY
        {
            get { return positionY; }
            set
            {
                if (value > 0 && value <= this.MapHeight - this.ShipHeight)
                {
                    positionY = value;
                }
            }
        }

        public int ShipWidth
        {
            get { return shipWidth; }
            set
            {
                if (value > 0 && this.PositionX + value <= this.MapWidth)
                {
                    shipWidth = value;
                }
            }
        }

        public int ShipHeight
        {
            get { return shipHeight; }
            set
            {
                if (value > 0 && this.PositionY + value <= this.MapHeight)
                {
                    shipHeight = value;
                }
            }
        }

        public bool Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public ObservableCollection<Ship> UserShips
        {
            get { return userShips; }
            set { userShips = value; }
        }

        public ShipType ShipType
        {
            get { return shipType; }
            set { shipType = value; }
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
        public Settings()
        {
            InitializeComponent();
            this.DataContext = this;

            // Builders.
            this.GameBuilder = new GameBuilder();

            Dimension dimension = new Dimension(DEFAULT_MAP_WIDTH, DEFAULT_MAP_HEIGHT);
            this.ShipBuilder = new ShipBuilder(dimension);
            this.shipView.Map = new Map(dimension);
            this.shipView.Build();

            // Default configurations modified by the user.
            this.configurations = new List<ShipConfiguration>();

            this.formShipType.ItemsSource = Enum.GetValues(typeof(Models.ShipType)).Cast<Models.ShipType>();
            this.ResetForm();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private Game GenGame()
        {
            // Selected ships.
            List<Ship> ships = this.userShips.ToList();
            Dimension dimension = this.shipView.Map.Dimension;

            // Random placement for IA.
            Map robotMap = this.ShipBuilder.RandomFromConfigurations(configurations);

            return this.GameBuilder.CreateGame(configurations, ships, robotMap, dimension);
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartPlaying()
        {
            if (this.userShips.Count() > 0)
            {
                Game game = this.GenGame();
                (this.Parent as Window).Content = new Play(game);
            } else
            {
                String message = "You must have at least one ship.";
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Sets the form with the initial values.
        /// </summary>
        private void ResetForm()
        {
            this.PositionX = DEFAULT_POSITION_X;
            this.PositionY = DEFAULT_POSITION_Y;
            this.formShipType.SelectedItem = DEFAULT_TYPE;
            this.ShipWidth = DEFAULT_WIDTH;
            this.ShipHeight = DEFAULT_HEIGHT;
            this.Rotation = DEFAULT_ROTATED;

            this.OnPropertyChanged("PositionX");
            this.OnPropertyChanged("PositionY");
            this.OnPropertyChanged("ShipWidth");
            this.OnPropertyChanged("ShipHeight");
            this.OnPropertyChanged("Rotation");
        }

        private void RebuildMap()
        {
            this.userShips.Clear();
            this.shipView.Map.Ships.Clear();
            this.shipView.Build();
        }
        #endregion

        #region Events
        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            this.StartPlaying();
        }

        private void FormShipType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ShipType = (ShipType)this.formShipType.SelectedItem;
        }

        private void Add_Ship(object sender, RoutedEventArgs e)
        {
            int x = this.PositionX - 1;
            int y = this.PositionY - 1;
            int width = this.ShipWidth;
            int height = this.ShipHeight;
            bool rotated = this.Rotation;
            ShipType type = this.ShipType;

            Dimension dimension = new Dimension(width, height);
            ShipConfiguration configuration = new ShipConfiguration(type, dimension, 1);

            this.configurations.Add(configuration);

            Ship ship = this.ShipBuilder.FromConfiguration(configuration, x, y, rotated);

            if (ship == null)
            {
                String message = "Invalid ship";
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else
            {
                this.UserShips.Add(ship);
                this.shipView.Map.Ships = this.UserShips.ToList();
                this.shipView.Update();

                this.ResetForm();
            }
        }

        private void Select_Ship(object sender, SelectionChangedEventArgs e)
        {
            int index = this.shipListView.SelectedIndex;

            if (index > 0 && index < this.UserShips.Count())
            {
                Ship ship = this.UserShips[index];
                // TODO: Highlight ship.
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
