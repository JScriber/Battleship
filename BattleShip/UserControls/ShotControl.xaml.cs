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
    public enum ShotState { None, Success, Fail }

    /// <summary>
    /// Logique d'interaction pour ShotControl.xaml
    /// </summary>
    public partial class ShotControl : UserControl, INotifyPropertyChanged
    {

        #region Constants
        private readonly String SUCCESS_SHOT = "success_shot.jpg";
        private readonly String FAIL_SHOT = "fail_shot.jpg";
        private readonly String NO_SHOT = "sea.jpg";
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private BitmapImage imageSource;
        private ShotState state;
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
        
        public ShotState State
        {
            get { return state; }
            set {
                state = value;

                // Base link.
                String link = "pack://application:,,,/BattleShip;component/Resources/";

                switch (state)
                {
                    case ShotState.Success:
                        link += this.SUCCESS_SHOT;
                        break;
                    case ShotState.Fail:
                        link += this.FAIL_SHOT;
                        break;
                    default:
                        link += this.NO_SHOT;
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
        public ShotControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ShotControl(int x, int y, ShotState state): this()
        {
            this.X = x;
            this.Y = y;
            this.State = state;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (((this.Parent as Grid).Parent as Grid).Parent as Play).HitMap(this.X, this.Y);
        }
        
        #endregion

        #region Property changed implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
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
