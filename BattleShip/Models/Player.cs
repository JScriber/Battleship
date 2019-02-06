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
        private List<Coordinates<int>> successShot;
        private List<Coordinates<int>> failShot;
        #endregion

        #region Properties
        public List<Ship> Ships
        {
            get { return ships; }
            set { ships = value; }
        }

        public List<Coordinates<int>> SuccessShot
        {
            get { return successShot; }
            set { successShot = value; }
        }

        public List<Coordinates<int>> FailShot
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
            this.successShot = new List<Coordinates<int>>();
            this.failShot = new List<Coordinates<int>>();
        }

        public Player(List<Ship> ships)
        {
            this.ships = ships;
        }
        #endregion
    }
}
