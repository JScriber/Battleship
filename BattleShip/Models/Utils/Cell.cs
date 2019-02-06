using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models.Utils
{
    public class Cell
    {
        #region Attributs
        private int x;
        private int y;
        private bool isDestroyed;
        #endregion

        #region Properties
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool IsDestroyed
        {
            get { return isDestroyed; }
            set { isDestroyed = value; }
        }

        #endregion

        #region Constructors
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion 
    }
}
