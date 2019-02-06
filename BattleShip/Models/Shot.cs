using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShip.Models.Utils;

namespace BattleShip.Models
{
    public class Shot
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private Player player;
        private bool isSuccessful;
        private Cell cell;
        private Map map;
        #endregion

        #region Properties
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        public Cell Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Shot()
        {

        }

        public Shot(Player player, bool isSuccessful, Cell cell, Map map)
        {
            this.player = player;
            this.isSuccessful = isSuccessful;
            this.cell = cell;
            this.map = map;
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
