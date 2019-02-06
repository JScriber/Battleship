using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models.Utils
{
    public class Dimension
    {
        #region Attributs
        private int width;
        private int height;
        #endregion

        #region Properties
        [Key]
        public int id { set; get; }

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
        public Dimension()
        {

        }

        public Dimension(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Dimension(Dimension dimension): this(dimension.Width, dimension.Height)
        {
            
        }
        #endregion
    }
}
