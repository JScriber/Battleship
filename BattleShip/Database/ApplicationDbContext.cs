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
    public class ApplicationDbContext : DbContext
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
        private DbSet<Shot> dbShots;
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

        public DbSet<Shot> DbShots
        {
            get { return dbShots; }
            set { dbShots = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationDbContext()
        {
            //this.Database.Initialize(true);
            this.DevResetDatabase();
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
