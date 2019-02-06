﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int id { set; get; }

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
        public Cell()
        {

        }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.IsDestroyed = false;
        }
        #endregion 
    }
}
