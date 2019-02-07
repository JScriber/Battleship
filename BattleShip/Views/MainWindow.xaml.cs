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
    public partial class MainWindow : Window
    {

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new Settings();
        }
        #endregion
    }
}