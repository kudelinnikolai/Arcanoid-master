#region using

using System;
using System.Drawing;
using Arcanoid.Core.Collision;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Environments.Units
{
    class GooBall : Ball, IBallable
    {
        #region Private Fields

        private double _angle = Math.PI / 5;
        private Point _lastPosition;

        #endregion

        #region Constructors

        public GooBall(ILevelInfo info)
            : base(info)
        {   }

        #endregion

        #region Public Properties

        //public bool IsFlying { get; set; }
        //public override Point Position { get; set; }
        //public new int Width { get; set; }
        //public new int Height { get; set; }

        #endregion

        #region Public Methods

        public override void Collided(BaseUnit baseUnit)
        {
            if (baseUnit.UnitType == UnitType.Player)
            {
                IsFlying = false;
            }

            if (baseUnit.UnitType != UnitType.Bonus)
            {
                double angle = CollisionChecker.GetAngle(_angle, _lastPosition, this, baseUnit);
                double randomAngle = new Random().NextDouble() / 10;
                int sign = new Random().Next() % 2 == 0 ? -1 : 1;
                _angle = angle + sign * randomAngle;
            }
        }

        public override void Load()
        {
            Load(@"Assets\ball.png");
        }

        public override void Update()
        {
            _lastPosition = Position;

            if (!IsFlying)
                return;

            double x = Speed * Math.Cos(_angle);
            double y = (-1) * Speed * Math.Sin(_angle);
            Position = new Point((int)(Position.X + x), (int)(Position.Y + y));

            Size levelSize = Info.GetLevelSize();
            if (Position.Y > levelSize.Height)
                Info.RemoveUnit(this);

        }
    #endregion
    }
}
