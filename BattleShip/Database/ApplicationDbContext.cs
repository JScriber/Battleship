using BattleShip.Models;
using BattleShip.Models.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Database
{
    class ApplicationDbContext : DbContext
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private DbSet<Game> dbGame;
        private DbSet<Map> dbMap;
        private DbSet<Ship> dbShip;
        private DbSet<Dimension> dbDimension;
        private DbSet<Cell> dbCoordinates;
        #endregion

        #region Properties
        public DbSet<Game> DbGame
        {
            get { return dbGame; }
            set { dbGame = value; }
        }

        public DbSet<Map> DbMap
        {
            get { return dbMap; }
            set { dbMap = value; }
        }

        public DbSet<Ship> DbShip
        {
            get { return dbShip; }
            set { dbShip = value; }
        }

        public DbSet<Dimension> DbDimension
        {
            get { return dbDimension; }
            set { dbDimension = value; }
        }

        public DbSet<Cell> DbCoordinates
        {
            get { return dbCoordinates; }
            set { dbCoordinates = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationDbContext()
        {
            DevResetDatabase();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void DevResetDatabase()
        {
            if (!this.Database.CompatibleWithModel(false))
            {
                this.Database.Delete();
                this.Database.Create();
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
