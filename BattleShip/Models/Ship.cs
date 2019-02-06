using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models.Utils;

namespace BattleShip.Models
{
    /// <summary>
    /// All types of ship.
    /// </summary>
    public enum ShipType { Carrier, BattleShip, Cruiser, Submarine, Destroyer }

    public class Ship
    {
        #region Attributs
        private ShipType type;
        private Dimensions dimensions;
        private List<Coordinates<int>> coordinates;
        #endregion

        #region Properties
        public ShipType Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public List<Coordinates<int>> Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }

        public Dimensions Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }


        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Ship()
        {
        }

        public Ship(ShipType type, Dimensions dimensions)
        {
            this.type = type;
            this.dimensions = dimensions;
        }

        public Ship(ShipType type, Dimensions dimensions, List<Coordinates<int>> coordinates): this(type, dimensions)
        {
            this.coordinates = coordinates;
        }
        #endregion
    }
}
