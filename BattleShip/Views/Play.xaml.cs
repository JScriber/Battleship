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

using BattleShip.Models;
using BattleShip.Models.Utils;

namespace BattleShip.Views
{
    /// <summary>
    /// Logique d'interaction pour Play.xaml
    /// </summary>
    public partial class Play : Page
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Play()
        {
            InitializeComponent();
            this.DataContext = this;

            Map map = new Map(new Dimension(10, 10));

            this.BuildMap(map);
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void BuildMap(Map map)
        {
            // Clears the attributes that will change.
            this.mapPlayer.Children.Clear();
            this.mapPlayer.ColumnDefinitions.Clear();
            this.mapPlayer.RowDefinitions.Clear();

            // Get the dimensions.
            Dimension dimension = map.Dimension;
            int i;

            for (i = 0; i < dimension.Width; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                this.mapPlayer.ColumnDefinitions.Add(column);
            }

            for (i = 0; i < dimension.Height; i++)
            {
                RowDefinition row = new RowDefinition();
                this.mapPlayer.RowDefinitions.Add(row);
            }

            // Button rendering.
            Task.Factory.StartNew(() =>
            {
                for (i = 0; i < dimension.Height; i++)
                {
                    for (int j = 0; j < dimension.Width; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            //  TODO: get boat in map.

                            Button btn = new Button();
                            btn.Content = "H:" + i + "W:" + j;
                            Grid.SetColumn(btn, i);
                            Grid.SetRow(btn, j);

                            this.mapPlayer.Children.Add(btn);
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
