using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Exceptions;

namespace BattleShip.Controllers
{
    public class ShipBuilder
    {

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private Dimension bounds;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShipBuilder()
        {

        }

        public ShipBuilder(Dimension bounds)
        {
            this.bounds = bounds;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public Ship FromConfiguration(ShipConfiguration configuration, int x, int y, bool rotated)
        {
            Dimension dimension = new Dimension(configuration.Dimension);

            if (this.FitBounds(x, y, dimension))
            {
                if (rotated)
                {
                    // TODO: Invert dimension.
                }

                Ship ship = new Ship(configuration.Type, configuration.Dimension);

                for (int i = x; i < x + dimension.Width; i++)
                {
                    for (int j = y; j < y + dimension.Height; j++)
                    {
                        Cell cell = new Cell(i, j);
                        ship.Cells.Add(cell);
                    }
                }

                return ship;
            }

            throw new OutOfBoundException();
        }

        private bool FitBounds(int x, int y, Dimension dimension)
        {
            // TODO: Test if fit bounds.
            return true;
        }
        #endregion

        #region Events
        #endregion


    }
}
