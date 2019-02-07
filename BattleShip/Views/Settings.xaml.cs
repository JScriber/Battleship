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
        ObservableCollection<Game> games = new ObservableCollection<Game>();
        ObservableCollection<Ship> ships = new ObservableCollection<Ship>();
        ObservableCollection<Player> players = new ObservableCollection<Player>();
        ObservableCollection<Map> map = new ObservableCollection<Map>();
        ObservableCollection<Cell> coordinates = new ObservableCollection<Cell>();
        ObservableCollection<Dimension> dimensions = new ObservableCollection<Dimension>();
        #endregion

        #region Attributs
        private int mapWidth;
        private int mapHeight;
        #endregion

        #region Properties
        public ObservableCollection<Ship> ShipType { get; set; }

        public int MapWidth
        {
            get { return mapWidth; }
            set
            {
                mapWidth = value;
                ResizeMap(this.gameGridLeft);
            }
        }

        public int MapHeight
        {
            get { return mapHeight; }
            set
            {
                mapHeight = value;
                ResizeMap(this.gameGridLeft);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Settings()
        {
            this.ShipType = new ObservableCollection<Ship>();
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
            Dimension dimension = new Dimension(5, 5);

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
                sb.FromConfiguration(configurations[1], 1, 1, true),
                sb.FromConfiguration(configurations[2], 1, 1, true),
            };

            // Random placement for IA.
            List<Ship> robotShips = sb.RandomFromConfigurations(configurations);

            return gb.CreateGame(configurations, ships, robotShips, dimension);
        }

        private void ResizeMap(Grid gridName)
        {
            gridName.Children.Clear();
            gridName.ColumnDefinitions.Clear();
            gridName.RowDefinitions.Clear();

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
                            btn.Content = "W:" + i + "H:" + j;
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
            Ship ship = new Ship(type, dimension);

            ships.Add(ship);
            Console.WriteLine("type:" + ship.Type + " dimensions:" + ship.Dimension);
        }

        private void StartPlaying()
        {
            Game game = this.GenGame();
            (this.Parent as Window).Content = new Play(game);
        }
        #endregion

        #region Events
        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            StartPlaying();
        }

        private void BtnAddShipType1(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Destroyer, int.Parse(this.shipType1Size.Text), 1);
        }
        private void BtnAddShipType2(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Cruiser, int.Parse(this.shipType2Size.Text), 1);
        }
        private void BtnAddShipType3(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Submarine, int.Parse(this.shipType3Size.Text), 1);
        }
        private void BtnAddShipType4(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.BattleShip, int.Parse(this.shipType4Size.Text), 1);
        }
        private void BtnAddShipType5(object sender, RoutedEventArgs e)
        {
            AddShip(Models.ShipType.Carrier, int.Parse(this.shipType5Size.Text), 1);
        }

        private void MapWidthTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion
    }
}
