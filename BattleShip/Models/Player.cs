using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Models.Utils;

namespace BattleShip.Models
{
    public class Player
    {
        #region Attributs
        private Map map;
        private bool isHuman;
        #endregion

        #region Properties

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }
        
        public bool IsHuman
        {
            get { return isHuman; }
            set { isHuman = value; }
        }
        #endregion

        #region Constructors
        public Player()
        {
        }

        public Player(bool isHuman, Map map)
        {
            this.isHuman = isHuman;
            this.map = map;
        }
        #endregion
    }
}
