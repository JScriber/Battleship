using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        public int id { set; get; }

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

        [NotMapped]
        public bool HasLost
        {
            get
            {
                return this.Map.Ships.All(ship => ship.Sunk);
            }
        }
        #endregion

        #region Constructors
        public Player()
        {
        }

        public Player(bool isHuman, Map map)
        {
            this.IsHuman = isHuman;
            this.Map = map;
        }
        #endregion
    }
}
