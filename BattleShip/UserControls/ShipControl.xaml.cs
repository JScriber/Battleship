using BattleShip.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BattleShip.UserControls
{
    public enum ShipState { None, Alive, Attacked, Sunk, Missed }

    /// <summary>
    /// Logique d'interaction pour ShipControl.xaml
    /// </summary>
    public partial class ShipControl : UserControl, INotifyPropertyChanged
    {
        #region Constants
        private readonly String ALIVE = "ship_alive.jpg";
        private readonly String ATTACKED = "ship_attacked.jpg";
        private readonly String SUNK = "ship_dead.jpg";
        private readonly String MISSED = "missed_shot.jpg";
        private readonly String NONE = "sea.jpg";
        #endregion

        #region Attributs
        private BitmapImage imageSource;
        private ShipState state;
        private int x;
        private int y;
        #endregion

        #region Properties
        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public ShipState State
        {
            get { return state; }
            set
            {
                state = value;

                // Base link.
                String link = "pack://application:,,,/BattleShip;component/Resources/";

                switch (state)
                {
                    case ShipState.Alive:
                        link += this.ALIVE;
                        break;
                    case ShipState.Attacked:
                        link += this.ATTACKED;
                        break;
                    case ShipState.Sunk:
                        link += this.SUNK;
                        break;
                    case ShipState.Missed:
                        link += this.MISSED;
                        break;
                    default:
                        link += this.NONE;
                        break;
                }

                // Set the new image source.
                this.ImageSource = new BitmapImage(new Uri(link));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShipControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ShipControl(int x, int y, ShipState state) : this()
        {
            this.X = x;
            this.Y = y;
            this.State = state;
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
