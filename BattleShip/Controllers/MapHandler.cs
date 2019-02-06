//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BattleShip.Models;
//using BattleShip.Exceptions;

//namespace BattleShip.Controllers
//{
//    public class MapHandler
//    {

//        #region StaticVariables
//        #endregion

//        #region Constants
//        #endregion

//        #region Variables
//        #endregion

//        #region Attributs
//        #endregion

//        #region Properties
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Default constructor.
//        /// </summary>
//        public MapHandler()
//        {

//        }
//        #endregion

//        #region StaticFunctions
//        #endregion

//        #region Functions
//        public void PlaceShip(Ship ship, Map map)
//        {
//            if (this.ShipFitsInMap(ship, map))
//            {
//                Tuple<int, int> limits = ship.Limits();

//                for (int x = ship.Coordinates.X; x < limits.Item1; x++)
//                {
//                    for (int y = ship.Coordinates.Y; y < limits.Item2; y++)
//                    {
//                        if (map.Representation[x, y] != null)
//                        {
//                            map.Representation[x, y] = ship;
//                        }
//                        else
//                        {
//                            throw new OverlapException("The ship overlaps another ship.");
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Says if the ship fits in the map.
//        /// </summary>
//        /// <param name="ship"></param>
//        /// <param name="map"></param>
//        /// <returns></returns>
//        private bool ShipFitsInMap(Ship ship, Map map)
//        {
//            Tuple<int, int> limits = ship.Limits();
//            int width = map.Dimensions.Width;
//            int height = map.Dimensions.Height;

//            return ship.Coordinates.X < width && ship.Coordinates.Y < height &&
//                limits.Item1 < width && limits.Item2 < height;
//        }
//        #endregion

//        #region Events
//        #endregion


//    }
//}
