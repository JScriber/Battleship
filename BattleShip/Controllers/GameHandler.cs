using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Exceptions;

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
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameHandler()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        /// <summary>
        /// Hits in the map.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="map"></param>
        //public void Hit(Coordinates<int> coordinates, Map map, Player player)
        //{
        //    if (this.CanHit(coordinates, player))
        //    {
        //        Ship ship = this.FindShip(map, coordinates);

        //        if (ship == null)
        //        {
        //            player.FailShot.Add(coordinates);
        //        }
        //        else
        //        {
        //            player.SuccessShot.Add(coordinates);
        //            this.HitShip(ship);
        //        }
        //    }
        //}

        /// <summary>
        /// Says if the player can hit at the given coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CanHit(Coordinates<int> coordinates, Player player)
        {
            return !player.SuccessShot.Contains(coordinates) &&
                !player.FailShot.Contains(coordinates);
        }

        /// <summary>
        /// Hits the given ship.
        /// </summary>
        /// <param name="ship"></param>
        //private void HitShip(Ship ship)
        //{
        //    if (!ship.HasSunk())
        //    {
        //        ship.Hits++;
        //    }
        //}

        /// <summary>
        /// Finds the ship in the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        //private Ship FindShip(Map map, Coordinates<int> coordinates)
        //{
        //    if (this.CoordinatesInMap(coordinates, map))
        //    {
        //        return map.Representation[coordinates.X, coordinates.Y];
        //    } else
        //    {
        //        throw new OutOfBoundException();
        //    }
        //}

        /// <summary>
        /// Says if the coordinates are in the map.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool CoordinatesInMap(Coordinates<int> coordinates, Map map)
        {
            Dimensions dimensions = map.Dimensions;

            return coordinates.X >= 0 && coordinates.Y >= 0
                && coordinates.X < dimensions.Width
                && coordinates.Y < dimensions.Height;
        }
        #endregion

        #region Events
        #endregion


    }
}
