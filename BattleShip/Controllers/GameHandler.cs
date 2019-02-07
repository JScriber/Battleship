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

        public void Hit(Cell cell, Map map, Player player, Game game)
        {
            if (this.CanShot(cell, map, game))
            {
                Ship ship = this.FindShip(map, cell);
                bool success = ship != null;

                if (success)
                {
                    this.HitShip(ship, cell);
                }

                // Logs the shot.
                // TODO: persist shots by passing by the db.
                Shot shot = new Shot(success, player, cell, map);

                this.SaveShot(game, shot);
            }
        }

        /// <summary>
        /// Says if the player can hit at the given coordinates.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool CanShot(Cell cell, Map map, Game game)
        {
            List<Shot> shots = game.Shots;

            // Test if a shot hasn't been done in the same map before.
            Shot shot = shots.Find(s => s.Map.id == map.id && this.CellsAreEqual(s.Cell, cell));
            
            return shot == null;
        }

        /// <summary>
        /// Hits the given ship.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="target"></param>
        private void HitShip(Ship ship, Cell target)
        {
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

            using (var dbcontext = this.DbContext)
            {
                // Add a shot to the list of shots.
                dbcontext.DbGame.Where(g => g.id == game.id).First().Shots.Add(shot);

                dbcontext.SaveChanges();
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
