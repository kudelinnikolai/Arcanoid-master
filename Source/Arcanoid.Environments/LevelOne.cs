#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Arcanoid.Core.Collision;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;
using Arcanoid.Environments.Units;

#endregion

namespace Arcanoid.Environments
{
    /// <summary>
    ///     The level class.  This will be the first level that the player interacts with.
    /// </summary>
    public class LevelOne : BaseLevel
    {
        #region Private Fields

        private readonly Player _player;
        private readonly IEnumerable<CollisionPair> _lastCollided = new List<CollisionPair>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LevelOne" /> class.
        /// </summary>
        public LevelOne()
        {
            // Backgrounds
            FileName = @"Assets\LevelOne.png";

            LeftTop = new Point(100, 45);
            PlayingSize = new Size(Size.Width - 2 * LeftTop.X, Size.Height - LeftTop.Y);

            // Enemies
            const int lines = 5;
            int rows = GetLevelSize().Width / Block.DefaultWidth;


            for (int j = 0; j < lines; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    Block block = new Block(this);
                    int positionY = j*block.Height;
                    int positionX = i * (block.Width);

                    block.Position = new Point(positionX, positionY);

                    Units.Add(block);
                }
            }

            // Player
            _player = new Player(this);
            int playerPositionX = PlayingSize.Width / 2 - _player.Width / 2;
            int playerPositionY = PlayingSize.Height - _player.Height - 50;
            _player.Position = new Point(playerPositionX, playerPositionY);
            Units.Add(_player);

            Units.Add(Walls.GetLeftWall(this));
            Units.Add(Walls.GetRightWall(this));
            Units.Add(Walls.GetTopWall(this));
        }

        #endregion

        #region Public Methods

        public override BaseLevel NextLevel()
        {
            return new StartScreen(); // return new LevelTwo()
        }

        public override void Update()
        {
            base.Update();

            BaseUnit[] baseUnits = new BaseUnit[Units.Count];
            Units.CopyTo(baseUnits);

            IEnumerable<CollisionPair> collidedUnits = CollisionChecker.GetAllCollisions(baseUnits);

            foreach (CollisionPair tuple in collidedUnits)
            {
                if (_lastCollided.All(prevoiseTuple => !prevoiseTuple.Equals(tuple)))
                {
                    tuple.Collided();
                }
            }

            bool ballOutside = _player.Ball.Position.Y > PlayingSize.Height;
            if (ballOutside && !testPhysic())
                Failed = true;

            //has no blocks
            if (baseUnits.All(unit => unit.UnitType != UnitType.Block))
                Success = true;
        }

        #endregion

        #region Private Methods

        private bool testPhysic()
        {
            return Environment.GetCommandLineArgs().Contains("physic");
        }

        #endregion
    }
}