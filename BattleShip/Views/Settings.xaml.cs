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

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

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
                ResizeMap(this.gameGridPlayer1);
            }
        }

        public int MapHeight
        {
            get { return mapHeight; }
            set
            {
                mapHeight = value;
                ResizeMap(this.gameGridPlayer1);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Test()
        {
            Dimension dimension = new Dimension(10, 10);

            // Builders.
            var gb = new GameBuilder();
            var sb = new ShipBuilder(dimension);

            // Default configurations modified by the user.
            List<ShipConfiguration> configurations = new List<ShipConfiguration>()
            {
                new ShipConfiguration(Models.ShipType.Carrier, new Dimension(2, 1), 2),
                new ShipConfiguration(Models.ShipType.Cruiser, new Dimension(2, 1), 2),
                new ShipConfiguration(Models.ShipType.Submarine, new Dimension(2, 1), 2),
                new ShipConfiguration(Models.ShipType.Destroyer, new Dimension(2, 1), 2),
                new ShipConfiguration(Models.ShipType.BattleShip, new Dimension(2, 1), 2),
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

            Game game = gb.CreateGame(configurations, ships, robotShips, dimension);

            foreach (var ship in game.Human.Map.Ships)
            {
                System.Console.WriteLine(ship.Type);
            }
        }

        private void ResizeMap(Grid gridName)
        {
            gridName.Children.Clear();
            gridName.ColumnDefinitions.Clear();
            gridName.RowDefinitions.Clear();

            for (int i = 0; i < this.MapHeight; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                gridName.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < this.MapWidth; i++)
            {
                RowDefinition row = new RowDefinition();
                gridName.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.MapHeight; i++)
                {
                    for (int j = 0; j < this.MapWidth; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            Button btn = new Button();
                            btn.Content = "H:" + i + "W:" + j;
                            Grid.SetColumn(btn, i);
                            Grid.SetRow(btn, j);

                            gridName.Children.Add(btn);
                        }));
                    }
                }
            });
        }
        #endregion

        #region Events
        #endregion

    }
}
