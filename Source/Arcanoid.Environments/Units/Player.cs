#region using

using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Arcanoid.Core.Collision;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Environments.Units
{
    public class Player : BaseUnit
    {
        #region Constants

        private const double Speed = Ball.Speed;

        #endregion

        #region Public Fields

        public Ball Ball;
        private Point _lastPosition;

        #endregion

        #region Constructors

        public Player(ILevelInfo info)
            : base(info)
        {
            Width = 80;
            Height = 20;
            UnitType = UnitType.Player;
                Random rnd = new Random();
            Ball = (rnd.Next(0, 2)==0)? new Ball(info): Ball = new GooBall(info);
            info.AddUnit(Ball);
        }

        #endregion

        #region Public Methods

        public override void Collided(BaseUnit baseUnit)
        {
            if (baseUnit.UnitType != UnitType.Wall) return;

            ESide side = CollisionChecker.GetCollisionSide(this, _lastPosition, baseUnit);
            switch (side)
            {
                case ESide.Left:
                    Position = new Point(baseUnit.Position.X - Width, Position.Y);
                    break;
                case ESide.Right:
                    Position = new Point(baseUnit.Position.X + baseUnit.Width, Position.Y);
                    break;
            }

            if (baseUnit.UnitType == UnitType.Bonus)
            {
                //Width = Width * 2;
            }
        }

        public override void Load()
        {
            Load(@"Assets\player.png");
        }

        public override void Update()
        {
            _lastPosition = Position;

            Point newPosition = Position;

            if (IsPressed(VirtualKeyStates.Left))
                newPosition = new Point(Position.X - (int)Speed, Position.Y);
            if (IsPressed(VirtualKeyStates.Right))
                newPosition = new Point(Position.X + (int)Speed, Position.Y);

            Position = newPosition;

            _testPhysic();

            if (Ball.IsFlying)
                return;

            Ball.Position = new Point(newPosition.X + Width / 2 - Ball.Width / 2, newPosition.Y - Ball.Height);

            if (IsPressed(VirtualKeyStates.Space))
            {
                Ball.IsFlying = true;
            }
        }

        #endregion

        #region Private Methods

        private void _testPhysic()
        {
            if (!Environment.GetCommandLineArgs().Contains("physic")) return;

            if (!IsPressed(VirtualKeyStates.Return)) return;

            if (!Ball.IsFlying) return;

            Ball = new Ball(Info);
            Ball.Load();
            Ball.Position = Position;
            Info.AddUnit(Ball);
        }

        #endregion

    }

    #region Interfaces

    public interface IBallable
    {
        bool IsFlying { get; set; }
        Point Position { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
    #endregion
}