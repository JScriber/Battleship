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
        }

        public End(bool computerHasWon): this()
        {
            this.ComputerHasWon = computerHasWon;
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
