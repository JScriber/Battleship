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
        private Dimension dimension;
        private List<Cell> cells;
        #endregion

        #region Properties
        public ShipType Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public List<Cell> Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        public Dimension Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Ship()
        {
        }
        
        public Ship(ShipType type, Dimension dimension)
        {
            this.type = type;
            this.dimension = dimension;
        }

        public Ship(ShipType type, Dimension dimension, List<Cell> cells): this(type, dimension)
        {
            this.cells = cells;
        }
        #endregion
    }
}
