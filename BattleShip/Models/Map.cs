using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Exceptions;
using BattleShip.Models.Utils;

namespace BattleShip.Models
{
    public class Map
    {
        #region Attributs
        private Dimensions dimensions;
        private List<Ship> ships;
        #endregion

        #region Properties
        public Dimensions Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public List<Ship> Ships
        {
            get { return ships; }
            set { ships = value; }
        }

        #endregion

        #region Constructors
        public Map()
        {
            this.ships = new List<Ship>();
        }

        public Map(Dimensions dimensions): this()
        {
            this.dimensions = dimensions;
        }
        #endregion
    }
}
