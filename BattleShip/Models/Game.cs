using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models
{
    class Game
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private DateTime startAt;
        private DateTime endAt;
        #endregion

        #region Properties
        public DateTime StartAt
        {
            get { return startAt; }
            set { startAt = value; }
        }

        public DateTime EndAt
        {
            get { return endAt; }
            set { endAt = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Game()
        {

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
