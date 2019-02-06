using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private Dimension dimension;
        private List<Ship> ships;
        #endregion

        #region Properties
        [Key]
        public int id { set; get; }

        public Dimension Dimension
        {
            get { return dimension; }
            set { dimension = value; }
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

        public Map(Dimension dimension): this()
        {
            this.dimension = dimension;
        }
        #endregion
    }
}
