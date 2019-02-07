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
        }

        
        /// <summary>
        /// Setups the default configuration.
        /// </summary>
        private void AddDefaultTypes()
        {

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
