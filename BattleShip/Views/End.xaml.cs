using System;
using System.Collections.Generic;
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

namespace BattleShip.Views
{
    /// <summary>
    /// Logique d'interaction pour End.xaml
    /// </summary>
    public partial class End : Page
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private bool computerHasWon;
        #endregion

        #region Properties
        public bool ComputerHasWon
        {
            get { return computerHasWon; }
            set { computerHasWon = value; }
        }

        public String Winner
        {
            get
            {
                return this.ComputerHasWon
                    ? "The AI has beaten you!"
                    : "You defeated the AI!";
            }
        }
        #endregion

        #region Constructors
        public End()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public End(bool computerHasWon)
        {
            InitializeComponent();
            this.ComputerHasWon = computerHasWon;
            this.DataContext = this;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void RestartPlaying()
        {
            (this.Parent as Window).Content = new Settings();
        }
        #endregion

        #region Events
        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            RestartPlaying();
        }
        #endregion
    }
}
