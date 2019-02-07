using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace BattleShip.Views
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private int mapWidth;
        private int mapHeight;
        #endregion

        #region Properties
        public int MapWidth
        {
            get { return mapWidth; }
            set
            {
                mapWidth = value;
                ResizeMap();
            }
        }

        public int MapHeight
        {
            get { return mapHeight; }
            set
            {
                mapHeight = value;
                ResizeMap();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void ResizeMap()
        {
            this.gameGridPlayer1.Children.Clear();
            this.gameGridPlayer1.ColumnDefinitions.Clear();
            this.gameGridPlayer1.RowDefinitions.Clear();

            for (int i = 0; i < this.MapHeight; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                this.gameGridPlayer1.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < this.MapWidth; i++)
            {
                RowDefinition row = new RowDefinition();
                this.gameGridPlayer1.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.MapHeight; i++)
                {
                    for (int j = 0; j < this.MapWidth; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            Button btn = new Button();
                            btn.Content = "H:" + i + "W:" + j;
                            Grid.SetColumn(btn, i);
                            Grid.SetRow(btn, j);

                            this.gameGridPlayer1.Children.Add(btn);
                        }));
                    }
                }
            });
        }
        #endregion

        #region Events
        #endregion

    }
}
