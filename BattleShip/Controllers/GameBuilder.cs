using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Database;

namespace BattleShip.Controllers
{
    public class GameBuilder
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameBuilder()
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Initiates the game.
        /// </summary>
        /// <param name="configurations"></param>
        /// <param name="humanShips"></param>
        /// <param name="robotMap"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public Game CreateGame(List<ShipConfiguration> configurations, List<Ship> humanShips, Map robotMap, Dimension dimension)
        {
            using (var db = new ApplicationDbContext())
            {
                // Create human map.
                Map humanMap = new Map(dimension);
                humanMap.Ships = humanShips;

                // Persist the maps.
                db.DbMap.Add(humanMap);
                db.DbMap.Add(robotMap);

                // Create human player.
                Player human = new Player(true, humanMap);
                Player robot = new Player(false, robotMap);

                // Create the game
                Game game = new Game(human, robot, configurations);

                // Save the initial state of the game.
                db.DbGame.Add(game);
                db.SaveChanges();

                return game;
            }
        }
        #endregion
    }
}
