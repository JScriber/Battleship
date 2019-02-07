using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Database;
using BattleShip.Controllers;

namespace BattleShip.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        #endregion

        #region Properties
        public ObservableCollection<Ship> ShipType { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            this.ShipType = new ObservableCollection<Ship>();

            // Default configurations.
            this.AddDefaultTypes();

            InitializeComponent();
            this.Content = new Settings();

            this.Test();
        }

        private void Test()
        {
            using (var db = new ApplicationDbContext())
            {
                // Base configuration.
                Ship carrier = new Ship(Models.ShipType.Carrier, new Dimension(2, 1));
                Ship cruiser = new Ship(Models.ShipType.Cruiser, new Dimension(3, 1));
                Ship submarine = new Ship(Models.ShipType.Submarine, new Dimension(3, 1));
                Ship destroyer = new Ship(Models.ShipType.Destroyer, new Dimension(3, 1));
                Ship battleShip = new Ship(Models.ShipType.BattleShip, new Dimension(4, 1));

                Dimension dimension = new Dimension(10, 10);

                // Builders.
                var sb = new ShipBuilder(dimension);


                // Create human map.
                Map humanMap = new Map(dimension);
                humanMap.Ships = new List<Ship>()
                {
                    sb.FromModel(carrier, 1, 1, false),
                    sb.FromModel(destroyer, 5, 6, true)
                };

                Player human = new Player(true, humanMap);

                Game game = new Game(human, null);

                db.DbGame.Add(game);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Setups the default configuration.
        /// </summary>
        private void AddDefaultTypes()
        {
            this.ShipType.Clear();

            this.AddShip(Models.ShipType.BattleShip, 4, 1);
            this.AddShip(Models.ShipType.Carrier, 2, 1);
            this.AddShip(Models.ShipType.Cruiser, 3, 1);
            this.AddShip(Models.ShipType.Submarine, 3, 1);
            this.AddShip(Models.ShipType.Destroyer, 2, 1);
        }

        /// <summary>
        /// Adds the ship into the selectable ships.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void AddShip(ShipType type, int width, int height)
        {
            Dimension dimension = new Dimension(width, height);
            Ship ship = new Ship(type, dimension);

            this.ShipType.Add(ship);
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        #endregion

        private void ShipSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
