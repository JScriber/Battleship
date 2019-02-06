using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Models
{
    public class Game
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private Player human;
        private Player computer;
        private List<Shot> shots;
        #endregion

        #region Properties
        [Key]
        public int id { set; get; }

        public Player Human
        {
            get { return human; }
            set { human = value; }
        }

        public Player Computer
        {
            get { return computer; }
            set { computer = value; }
        }

        public List<Shot> Shots
        {
            get { return shots; }
            set { shots = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Game()
        {

        }

        public Game(Player human, Player computer)
        {
            this.human = human;
            this.computer = computer;
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
