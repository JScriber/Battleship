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
using BattleShip.Controllers;
using BattleShip.UserControls;

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
        private readonly SolidColorBrush SHOT_SUCCESS = Brushes.Green;
        private readonly SolidColorBrush SHOT_FAILED = Brushes.Red;
        private readonly SolidColorBrush NO_SHOT = Brushes.Gray;

        private readonly SolidColorBrush SHIP = Brushes.Blue;
        private readonly SolidColorBrush NO_SHIP = Brushes.Gray;
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private Game game;
        private GameHandler gameHandler;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Play(Game game)
        {
            this.game = game;
            this.gameHandler = new GameHandler();

            InitializeComponent();

            // Data binding with view.
            this.DataContext = this;

            this.BuildMap();
            this.BuildShots();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        /// <summary>
        /// Builds the map.
        /// </summary>
        private void BuildMap()
        {
            // Map of the current player.
            Map map = this.game.Human.Map;
            Dimension dimension = map.Dimension;

            // Initialize the map.
            this.SetGridDimension(this.mapPlayer, dimension);

            // Set the buttons.
            this.UpdateMap();
        }

        /// <summary>
        /// Updates the content of the map.
        /// </summary>
        private void UpdateMap()
        {
            Map map = this.game.Human.Map;

            // Button rendering.
            Task.Factory.StartNew(() =>
            {
                List<Ship> ships = map.Ships;

                for (int i = 0; i < map.Dimension.Height; i++)
                {
                    for (int j = 0; j < map.Dimension.Width; j++)
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

        /// <summary>
        /// Builds the shots map.
        /// </summary>
        private void BuildShots()
        {
            Dimension dimension = this.game.Computer.Map.Dimension;
            this.SetGridDimension(this.mapShots, dimension);

            this.UpdateShots();
        }

        private void UpdateShots()
        {
            Map map = this.game.Computer.Map;

            // Button rendering.
            Task.Factory.StartNew(() =>
            {
                List<Shot> playerShots = this.game.Shots.Where(shot => shot.Player.IsHuman).ToList();

                foreach (var s in playerShots)
                {
                    System.Console.WriteLine(s.IsSuccessful);
                }

                for (int i = 0; i < map.Dimension.Height; i++)
                {
                    for (int j = 0; j < map.Dimension.Width; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            Shot shot = playerShots.FirstOrDefault(s => s.Cell.X == i && s.Cell.Y == j);
                            ShotState state = ShotState.None;

                            if (shot != null)
                            {
                                state = shot.IsSuccessful
                                    ? ShotState.Success
                                    : ShotState.Fail;
                            }

                            ShotControl shotControl = new ShotControl(i, j, state);

                            Grid.SetColumn(shotControl, i);
                            Grid.SetRow(shotControl, j);

                            this.mapShots.Children.Add(shotControl);
                        }));
                    }
                }
            });
        }

        /// <summary>
        /// Sets the dimensions of the grid.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="dimension"></param>
        private void SetGridDimension(Grid grid, Dimension dimension)
        {
            int i;

            // Clears out the grid.
            this.ClearGrid(grid);

            for (i = 0; i < dimension.Width; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                grid.ColumnDefinitions.Add(column);
            }

            for (i = 0; i < dimension.Height; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
        }

        /// <summary>
        /// Clears out the grid.
        /// </summary>
        /// <param name="grid"></param>
        private void ClearGrid(Grid grid)
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
        }

        /// <summary>
        /// Triggers the AI to play.
        /// </summary>
        private void TriggerAI()
        {
            System.Console.WriteLine("AI has been triggered.");
            // TODO: Implement.
            // Refresh all.
        }
        #endregion

        #region Events
        public void HitMap(int x, int y)
        {
            Cell cell = new Cell(x, y);
            Map foo = this.game.Computer.Map;

            // Hits the cell.
            this.gameHandler.Hit(cell, foo, this.game.Human, this.game);

            this.UpdateShots();

            this.TriggerAI();
        }
        #endregion
    }
}
