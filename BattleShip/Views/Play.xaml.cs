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
            List<Shot> robotShots = this.game.Shots.Where(shot => !shot.Player.IsHuman).ToList();

            // Button rendering.
            Task.Factory.StartNew(() =>
            {
                List<Ship> ships = map.Ships;
                Cell[,] cells = map.MatrixRepresentation;

                for (int i = 0; i < map.Dimension.Height; i++)
                {
                    for (int j = 0; j < map.Dimension.Width; j++)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                        {
                            Cell cell = cells[i, j];
                            Shot shot = robotShots.FirstOrDefault(s => s.Cell.X == i && s.Cell.Y == j);

                            ShipState state = ShipState.None;

                            if (cell != null)
                            {
                                if (cell.IsDestroyed)
                                {
                                    if (cell.Ship.Sunk)
                                    {
                                        state = ShipState.Sunk;
                                    } else
                                    {
                                        state = ShipState.Attacked;
                                    }
                                } else
                                {
                                    state = ShipState.Alive;
                                }
                            } else
                            {
                                // Handle missed shot.
                                if (shot != null)
                                {
                                    state = ShipState.Missed;
                                }
                            }

                            ShipControl shipControl = new ShipControl(i, j, state);

                            Grid.SetColumn(shipControl, i);
                            Grid.SetRow(shipControl, j);

                            this.mapPlayer.Children.Add(shipControl);
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
            List<Shot> playerShots = this.game.Shots.Where(shot => shot.Player.IsHuman).ToList();

            // Button rendering.
            Task.Factory.StartNew(() =>
            {
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
                                if (shot.IsSuccessful)
                                {
                                    if (shot.Cell.Ship.Sunk)
                                    {
                                        state = ShotState.Sunk;
                                    } else
                                    {
                                        state = ShotState.Success;
                                    }
                                } else
                                {
                                    state = ShotState.Fail;
                                }
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
            this.gameHandler.AIPlay(this.game);

            this.CheckWinner();
            this.UpdateMap();
        }

        private bool CheckWinner()
        {
            if (this.game.Human.HasLost || this.game.Computer.HasLost)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Events
        public void HitMap(int x, int y)
        {
            Map foo = this.game.Computer.Map;

            // Hits the cell.
            this.gameHandler.Hit(x, y, foo, this.game.Human, this.game);

            if (!this.CheckWinner())
            {
                this.UpdateShots();
                this.TriggerAI();
            }
            else
            {
                (this.Parent as Window).Content = new End(this.game.Human.HasLost);
            }
        }
        #endregion
    }
}
