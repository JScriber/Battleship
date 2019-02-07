using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models.Utils;

namespace BattleShip.Models
{
    public class ShipConfiguration
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private ShipType type;
        private int multiplicity;
        private Dimension dimension;
        #endregion

        #region Properties
        [Key]
        public int id { set; get; }

        public ShipType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Multiplicity
        {
            get { return multiplicity; }
            set { multiplicity = value; }
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
        public ShipConfiguration()
        {

        }

        public ShipConfiguration(ShipType type, Dimension dimension, int multiplicity)
        {
            this.type = type;
            this.dimension = dimension;
            this.multiplicity = multiplicity;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        #endregion


    }
}
