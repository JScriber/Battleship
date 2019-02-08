using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Exceptions;
using BattleShip.Database;

namespace BattleShip.Controllers
{
    public class GameHandler
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private ApplicationDbContext dbContext;
        #endregion

        #region Properties

        public ApplicationDbContext DbContext
        {
            get { return dbContext; }
            set { dbContext = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameHandler()
        {
            this.DbContext = new ApplicationDbContext();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public void AIPlay(Game game)
        {
            Map foo = game.Human.Map;
            Cell[,] cells = foo.MatrixRepresentation;
            Dimension dimension = foo.Dimension;

            // All shots of the AI.
            List<Shot> shots = game.Shots.Where(shot => !shot.Player.IsHuman).ToList();

            Random random = new Random();
            int x, y;

            do
            {
                x = random.Next(0, dimension.Width);
                y = random.Next(0, dimension.Height);
            } while (shots.Any(shot => shot.Cell.X == x && shot.Cell.Y == y));

            this.Hit(x, y, foo, game.Computer, game);
        }

        /// <summary>
        /// Hits at the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <param name="player"></param>
        /// <param name="game"></param>
        public void Hit(int x, int y, Map map, Player player, Game game)
        {
            if (this.CanShot(x, y, map, game))
            {
                Cell cell = map.MatrixRepresentation[x, y];
                bool success = cell != null;

                if (success)
                {
                    this.HitShip(cell);
                } else
                {
                    // Cell with no ship.
                    cell = new Cell(x, y);
                }

                // Logs the shot.
                Shot shot = new Shot(success, player, cell, map);

                this.SaveShot(game, shot);
            }
        }

        /// <summary>
        /// Says if the player can hit at the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool CanShot(int x, int y, Map map, Game game)
        {
            List<Shot> shots = game.Shots;

            // Test if a shot hasn't been done in the same map before.
            Shot shot = shots.Find(s => s.Map.id == map.id && s.Cell.X == x && s.Cell.Y == y);
            
            return shot == null;
        }

        /// <summary>
        /// Hits the given ship.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="target"></param>
        private void HitShip(Cell target)
        {
            Ship ship = target.Ship;

            if (!this.HasSunk(ship))
            {
                int x = target.X;
                int y = target.Y;

                Cell cell = ship.Cells.Find(c => c.X == x && c.Y == y);

                if (cell != null)
                {
                    cell.IsDestroyed = true;
                }
            }
        }

        private bool HasSunk(Ship ship)
        {
            return ship.Cells.All(cell => cell.IsDestroyed);
        }

        /// <summary>
        /// Finds the ship in the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private Ship FindShip(Map map, Cell cell)
        {
            if (this.CellInMap(cell, map))
            {
                return map.Ships.FirstOrDefault(ship => ship.Cells.Any(c => this.CellsAreEqual(c, cell)));
            }
            else
            {
                throw new OutOfBoundException();
            }
        }

        /// <summary>
        /// Says if the cell is in the map.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool CellInMap(Cell cell, Map map)
        {
            Dimension dimensions = map.Dimension;

            return cell.X >= 0 && cell.Y >= 0
                && cell.X < dimensions.Width
                && cell.Y < dimensions.Height;
        }

        /// <summary>
        /// Says if two cells are equal.
        /// </summary>
        /// <param name="compared"></param>
        /// <param name="comparing"></param>
        /// <returns></returns>
        private bool CellsAreEqual(Cell compared, Cell comparing)
        {
            return compared.X == comparing.X &&
                compared.Y == comparing.Y;
        }

        /// <summary>
        /// Saves a shot in the game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="shot"></param>
        private void SaveShot(Game game, Shot shot)
        {
            game.Shots.Add(shot);
            
            // Add a shot to the list of shots.
            this.DbContext.DbGame.Where(g => g.id == game.id).First().Shots.Add(shot);

            this.DbContext.SaveChanges();
        }
        #endregion

        #region Events
        #endregion
    }
}
