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
        #region Attributs
        private Dimension bounds;
        #endregion

        #region Properties
        public Dimension Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

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
            this.Bounds = bounds;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public Ship FromConfiguration(ShipConfiguration configuration, int x, int y, bool rotated)
        {
            Dimension dimension = new Dimension(configuration.Dimension);

            if (rotated)
            {
                int width = dimension.Width;
                dimension.Width = dimension.Height;
                dimension.Height = width;
            }

            if (this.FitBounds(x, y, dimension))
            {
                // Make a ship from the configuration.
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

        /// <summary>
        /// Creates random ships from the configurations.
        /// </summary>
        /// <param name="shipConfigurations"></param>
        /// <returns></returns>
        public List<Ship> RandomFromConfigurations(List<ShipConfiguration> shipConfigurations)
        {
            List<Ship> ships = new List<Ship>();

            foreach (var configuration in shipConfigurations)
            {
                int multiplicity = configuration.Multiplicity;
                Dimension dimension = configuration.Dimension;
                Random random = new Random();

                for (int i = 0; i < multiplicity; i++)
                {
                    int x = random.Next(0, this.Bounds.Width - dimension.Width);
                    int y = random.Next(0, this.Bounds.Height - dimension.Height);
                    
                    Ship ship = this.FromConfiguration(configuration, x, y, false);
                    ships.Add(ship);
                }
            }

            return ships;
        }

        /// <summary>
        /// Says if the position fits in the bounds.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        private bool FitBounds(int x, int y, Dimension dimension)
        {
            return x >= 0 && y >= 0
                && x + dimension.Width < this.Bounds.Width
                && y + dimension.Height < this.Bounds.Height;
        }
        #endregion
    }
}
