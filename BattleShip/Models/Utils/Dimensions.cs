using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models.Utils
{
    public class Dimensions
    {
        #region Attributs
        private int width;
        private int height;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dimensions()
        {

        }

        public Dimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        #endregion
    }
}
