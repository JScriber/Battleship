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
        private List<Ship> ships;
        private List<Cell> successShot;
        private List<Cell> failShot;
        #endregion

        #region Properties
        public List<Ship> Ships
        {
            get { return ships; }
            set { ships = value; }
        }

        public List<Cell> SuccessShot
        {
            get { return successShot; }
            set { successShot = value; }
        }

        public List<Cell> FailShot
        {
            get { return failShot; }
            set { failShot = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Player()
        {
            this.successShot = new List<Cell>();
            this.failShot = new List<Cell>();
        }

        public Player(List<Ship> ships)
        {
            this.ships = ships;
        }
        #endregion
    }
}
