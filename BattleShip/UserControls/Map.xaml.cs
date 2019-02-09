using BattleShip.Models;
using BattleShip.Models.Utils;
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
        #endregion

        #region Attributs
        private Map map;
        #endregion

        #region Properties


        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        #endregion

        #region Constructors
        public MapControl()
        {
            InitializeComponent();
        }

        public MapControl(Map map): this()
        {
            this.map = map;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public void Build()
        {
            int width = this.Map.Dimension.Width;
            int height = this.Map.Dimension.Height;

            this.mapView.Children.Clear();
            this.mapView.ColumnDefinitions.Clear();
            this.mapView.RowDefinitions.Clear();

            // Helper size.
            GridLength helperSize = new GridLength(18);

            // Column helper.
            ColumnDefinition colHelper = new ColumnDefinition();
            colHelper.Width = helperSize;
            this.mapView.ColumnDefinitions.Add(colHelper);

            for (int i = 0; i < width; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                this.mapView.ColumnDefinitions.Add(col);
            }

            // Row helper.
            RowDefinition rowHelper = new RowDefinition();
            rowHelper.Height = helperSize;
            this.mapView.RowDefinitions.Add(rowHelper);

            for (int i = 0; i < height; i++)
            {
                RowDefinition row = new RowDefinition();
                this.mapView.RowDefinitions.Add(row);
            }

            Task.Factory.StartNew(() =>
            {
                Cell[,] cells = this.Map.MatrixRepresentation;

                for (int i = 0; i < width + 1; i++)
                {
                    for (int j = 0; j < height + 1; j++)
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
                                this.mapView.Children.Add(text);
                            }
                            else
                            {
                                int x = i - 1;
                                int y = j - 1;

                                ShipControl control = new ShipControl(x, y, ShipState.None);
                                this.SetShipControlState(control, cells[x, y]);

                                Grid.SetColumn(control, i);
                                Grid.SetRow(control, j);
                                this.mapView.Children.Add(control);
                            }
                        }));
                    }
                }
            });
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        public void Update()
        {
            Task.Factory.StartNew(() =>
            {
                Cell[,] cells = this.Map.MatrixRepresentation;

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                {
                    UIElementCollection mapElements = this.mapView.Children;

                    foreach (UIElement mapElement in mapElements)
                    {
                        if (mapElement is ShipControl)
                        {
                            ShipControl shipControl = mapElement as ShipControl;

                            int x = shipControl.X;
                            int y = shipControl.Y;

                            this.SetShipControlState(shipControl, cells[x, y]);
                        }
                    }
                }));
            });
        }

        private void SetShipControlState(ShipControl shipControl, Cell cell)
        {
            ShipState state = ShipState.None;

            if (cell != null)
            {
                if (cell.IsDestroyed)
                {
                    if (cell.Ship.Sunk)
                    {
                        state = ShipState.Sunk;
                    }
                    else
                    {
                        state = ShipState.Attacked;
                    }
                }
                else
                {
                    state = ShipState.Alive;
                }
            }

            shipControl.State = state;
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
