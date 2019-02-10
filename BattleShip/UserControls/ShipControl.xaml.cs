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

    public sealed class Coordinates
    {
        #region Attributs
        private int x;
        private int y;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public Coordinates()
        {

        }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
    }

    /// <summary>
    /// Logique d'interaction pour ShipControl.xaml
    /// </summary>
    public partial class ShipControl : UserControl, INotifyPropertyChanged
    {
        #region Constants
        private const String RESOURCES = "pack://application:,,,/BattleShip;component/Resources/";

        private const String ALIVE = "ship_alive.jpg";
        private const String ATTACKED = "ship_attacked.jpg";
        private const String SUNK = "ship_dead.jpg";
        private const String MISSED = "missed_shot.jpg";
        private const String NONE = "sea.jpg";
        #endregion

        #region Attributs
        private BitmapImage imageSource;
        private ShipState state;
        private int x;
        private int y;

        public event EventHandler<Coordinates> Fire;
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
                String link = RESOURCES;

                switch (state)
                {
                    case ShipState.Alive:
                        link += ALIVE;
                        break;
                    case ShipState.Attacked:
                        link += ATTACKED;
                        break;
                    case ShipState.Sunk:
                        link += SUNK;
                        break;
                    case ShipState.Missed:
                        link += MISSED;
                        break;
                    default:
                        link += NONE;
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

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Fire != null)
            {
                this.Fire(this, new Coordinates(this.X, this.Y));
            }
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
