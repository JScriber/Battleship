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
        public ObservableCollection<Ship> ShipType { get; set; }

        public MainWindow()
        {
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
            Dimensions dimensions = new Dimensions(width, height);
            Ship ship = new Ship(type, dimensions);

            this.ShipType.Add(ship);
        }
    }
}
