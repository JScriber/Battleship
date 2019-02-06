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

namespace BattleShip
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
    }
}
