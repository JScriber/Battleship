using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using BattleShip.Models;
using BattleShip.Models.Utils;
using BattleShip.Controllers;
using BattleShip.UserControls;

namespace BattleShip.Views
{
    public partial class Play : Page
    {
        #region Attributs
        private Game game;
        private GameHandler gameHandler = new GameHandler();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Play(Game game)
        {
            this.game = game;

            InitializeComponent();

            // Data binding with view.
            this.DataContext = this;

            this.BuildMap();
            this.BuildShots();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Hits the map at the coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void HitMap(int x, int y)
        {
            Map foo = this.game.Computer.Map;

            // Hits the cell.
            this.gameHandler.Hit(x, y, foo, this.game.Human, this.game);

            if (!this.SomeoneLost())
            {
                this.UpdateShots();
                this.TriggerAI();
            }
            else
            {
                this.EndGame();
            }
        }

        /// <summary>
        /// Builds the map.
        /// </summary>
        private void BuildMap()
        {
            this.mapPlayer.Map = this.game.Human.Map;
            this.mapPlayer.Build();
        }

        /// <summary>
        /// Builds the AI map.
        /// </summary>
        private void BuildShots()
        {
            this.mapAI.Map = this.game.Computer.Map;
            this.mapAI.Build();
        }

        /// <summary>
        /// Updates the content of the map.
        /// </summary>
        private void UpdateMap()
        {
            this.mapPlayer.Shots = this.game.Shots.Where(shot => !shot.Player.IsHuman).ToList();
            this.mapPlayer.Update();
        }

        /// <summary>
        /// Updates the content of the AI map.
        /// </summary>
        private void UpdateShots()
        {
            this.mapAI.Shots = this.game.Shots.Where(shot => shot.Player.IsHuman).ToList();
            this.mapAI.Update();
        }

        /// <summary>
        /// Triggers the AI to play.
        /// </summary>
        private void TriggerAI()
        {
            this.gameHandler.AIPlay(this.game);

            if (this.game.Human.HasLost)
            {
                this.EndGame();
            } else
            {
                this.UpdateMap();
            }
        }

        /// <summary>
        /// Says if someone has lost the game.
        /// </summary>
        /// <returns></returns>
        private bool SomeoneLost()
        {
            return this.game.Human.HasLost || this.game.Computer.HasLost;
        }

        /// <summary>
        /// Switches screen and tells the winner.
        /// </summary>
        private void EndGame()
        {
            (this.Parent as Window).Content = new End(this.game.Human.HasLost);
        }
        #endregion

        #region Events
        private void MapAI_Fire(object sender, Coordinates e)
        {
            this.HitMap(e.X, e.Y);
        }
        #endregion
    }
}
