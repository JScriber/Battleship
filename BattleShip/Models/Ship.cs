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
        private int width;
        private int height;
        private List<Cell> coordinates;
        #endregion

        #region Properties
        public ShipType Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public List<Cell> Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Ship()
        {
        }

        public Ship(ShipType type, int width, int height, List<Cell> coordinates)
        {
            this.type = type;
            this.width = width;
            this.height = height;
            this.coordinates = coordinates;
        }

        #endregion
    }
}
