using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BattleShip.UserControls
{
    public partial class MapControl : UserControl, INotifyPropertyChanged
    {
        #region StaticVariables
        // Limits.
        public static readonly int MIN_MAP_SIZE = 1;
        public static readonly int MAX_MAP_SIZE = 35;
        // Default sizes.
        public static readonly int DEFAULT_MAP_WIDTH = 5;
        public static readonly int DEFAULT_MAP_HEIGHT = 5;
        #endregion

        #region Attributs
        private int mapHeight;
        private int mapWidth;
        #endregion

        #region Properties
        public int MapHeight
        {
            get { return mapHeight; }
            set {
                if (this.ValidSize(value))
                {
                    mapHeight = value;
                } else
                {
                    mapHeight = DEFAULT_MAP_HEIGHT;
                }

                this.Render();
            }
        }

        public int MapWidth
        {
            get { return mapWidth; }
            set {
                if (this.ValidSize(value))
                {
                    mapWidth = value;
                } else
                {
                    mapWidth = DEFAULT_MAP_WIDTH;
                }

                this.Render();
            }
        }
        #endregion

        #region Constructors
        public MapControl()
        {
            InitializeComponent();

            this.SetSize(DEFAULT_MAP_WIDTH, DEFAULT_MAP_HEIGHT);
        }

        public MapControl(int width, int height)
        {
            InitializeComponent();

            this.SetSize(width, height);
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        /// <summary>
        /// Initializes the map.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void SetSize(int width, int height)
        {
            // Doesn't use setters to avoid two rendering.
            this.mapWidth = width;
            this.mapHeight = height;

            this.Render();
        }

        private bool ValidSize(int size)
        {
            return size >= MIN_MAP_SIZE && size < MAX_MAP_SIZE;
        }

        public void Render()
        {
            Console.WriteLine(this.MapHeight + " : " + this.MapWidth);

            this.map.Children.Clear();
            this.map.ColumnDefinitions.Clear();
            this.map.RowDefinitions.Clear();

            // Helper size.
            GridLength helperSize = new GridLength(18);

            // Column helper.
            ColumnDefinition colHelper = new ColumnDefinition();
            colHelper.Width = helperSize;
            this.map.ColumnDefinitions.Add(colHelper);

            for (int i = 0; i < this.MapWidth; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                this.map.ColumnDefinitions.Add(col);
            }

            // Row helper.
            RowDefinition rowHelper = new RowDefinition();
            rowHelper.Height = helperSize;
            this.map.RowDefinitions.Add(rowHelper);

            for (int i = 0; i < this.MapHeight; i++)
            {
                RowDefinition row = new RowDefinition();
                this.map.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.MapWidth + 1; i++)
                {
                    for (int j = 0; j < this.MapHeight + 1; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            if (i == 0 || j == 0)
                            {
                                TextBlock text = new TextBlock();
                                text.VerticalAlignment = VerticalAlignment.Center;
                                text.HorizontalAlignment = HorizontalAlignment.Center;

                                if (i == 0 && j != 0)
                                {
                                    text.Text = j.ToString();
                                }
                                else
                                {
                                    if (j == 0 && i != 0)
                                    {
                                        text.Text = i.ToString();
                                    }
                                }

                                Grid.SetColumn(text, i);
                                Grid.SetRow(text, j);
                                this.map.Children.Add(text);
                            }
                            else
                            {
                                ShipControl control = new ShipControl(i - 1, j - 1, ShipState.None);

                                Grid.SetColumn(control, i);
                                Grid.SetRow(control, j);
                                this.map.Children.Add(control);
                            }
                        }));
                    }
                }
            });
        }
        #endregion

        #region Events
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
