using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models.Utils
{
    public class Coordinates<T>
    {
        #region Attributs
        private T x;
        private T y;
        #endregion

        #region Properties
        public T X
        {
            get { return x; }
            set { x = value; }
        }

        public T Y
        {
            get { return y; }
            set { y = value; }
        }
        #endregion

        #region Constructors
        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion 
    }
}
