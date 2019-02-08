using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int id { set; get; }

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
            this.cells = new List<Cell>();
        }
        
        public Ship(ShipType type, Dimension dimension): this()
        {
            this.type = type;
            this.dimension = dimension;
        }

        public Ship(ShipType type, Dimension dimension, List<Cell> cells): this(type, dimension)
        {
            this.cells = cells;
        }
        #endregion
        #region 

        #endregion
    }
}
