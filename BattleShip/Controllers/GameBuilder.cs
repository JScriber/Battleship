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
        public GameBuilder()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public Game CreateGame(List<ShipConfiguration> configurations, List<Ship> ships, Dimension dimension)
        {
            using (var db = new ApplicationDbContext())
            {
                // Create human map.
                Map humanMap = new Map(dimension);
                humanMap.Ships = ships;

                // Create IA map.
                Map robotMap = new Map(dimension);
                // TODO: Generate ships based on the configuration.
                robotMap.Ships = new List<Ship>();

                // Persist the maps.
                db.DbMap.Add(humanMap);
                db.DbMap.Add(robotMap);

                // Create human player.
                Player human = new Player(true, humanMap);
                Player robot = new Player(false, robotMap);

                // Create the game
                Game game = new Game(human, robot, configurations);

                db.DbGame.Add(game);
                db.SaveChanges();

                return game;
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
