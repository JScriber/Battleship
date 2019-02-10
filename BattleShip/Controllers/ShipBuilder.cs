using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShip.Models;
using BattleShip.Models.Utils;

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
                        Cell cell = new Cell(i, j, ship);
                        ship.Cells.Add(cell);
                    }
                }

                return ship;
            }

            return null;
        }

        /// <summary>
        /// Creates a random map based on the configurations.
        /// </summary>
        /// <param name="shipConfigurations"></param>
        /// <returns></returns>
        public Map RandomFromConfigurations(List<ShipConfiguration> shipConfigurations)
        {
            Map map = new Map(this.Bounds);

            if (this.ConfigurationFitsInMap(shipConfigurations))
            {
                foreach (var configuration in shipConfigurations)
                {
                    int multiplicity = configuration.Multiplicity;
                    Random random = new Random();

                    if (multiplicity > 0)
                    {
                        for (int i = 0; i < multiplicity; i++)
                        {
                            Ship ship = null;
                            Dimension dimension = new Dimension(configuration.Dimension);

                            do
                            {
                                // Determine if the ship is rotated.
                                bool rotated = random.Next(2) == 1;

                                if (rotated)
                                {
                                    int width = dimension.Width;
                                    dimension.Width = dimension.Height;
                                    dimension.Height = width;
                                }

                                int x = random.Next(0, this.Bounds.Width - dimension.Width);
                                int y = random.Next(0, this.Bounds.Height - dimension.Height);
                                
                                if (this.FitBounds(x, y, dimension))
                                {
                                    ship = this.FromConfiguration(configuration, x, y, rotated);
                                } else
                                {
                                    Console.WriteLine("Doesnt fit bounds");
                                }
                            } while (ship == null || this.ShipIntersects(ship, map));

                            map.Ships.Add(ship);
                        }
                    }
                }
            }

            // Debug purpose.
            //foreach (var ship in map.Ships)
            //{
            //    Console.WriteLine("Ship AI. Width: " + ship.Dimension.Width + " Height: " + ship.Dimension.Height);

            //    foreach (var cell in ship.Cells)
            //    {
            //        Console.WriteLine("Cell - X: " + cell.X + " Y: " + cell.Y);
            //    }

            //    Console.WriteLine("-----");
            //}

            return map;
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

        /// <summary>
        /// Says if a list of configuration can fit in the dimension.
        /// </summary>
        /// <param name="shipConfigurations"></param>
        /// <returns></returns>
        public bool ConfigurationFitsInMap(List<ShipConfiguration> shipConfigurations)
        {
            int totalCells = this.Bounds.Width * this.Bounds.Height;
            int shipCells = 0;

            foreach (var shipConfiguration in shipConfigurations)
            {
                for (int i = 0; i < shipConfiguration.Multiplicity; i++)
                {
                    Dimension d = shipConfiguration.Dimension;
                    shipCells += d.Width * d.Height;
                }
            }

            return shipCells <= totalCells;
        }

        /// <summary>
        /// Says if a ship intersects with another.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool ShipIntersects(Ship ship, Map map)
        {
            Cell[,] representation = map.MatrixRepresentation;

            foreach (var cell in ship.Cells)
            {
                if (representation[cell.X, cell.Y] != null)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
