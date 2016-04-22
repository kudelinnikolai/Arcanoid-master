#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Core.Collision
{
    public static class CollisionChecker
    {
        #region Public Methods

        public static List<CollisionPair> GetAllCollisions(BaseUnit[] allUnits)
        {
            List<CollisionPair> collided = new List<CollisionPair>();
            foreach (BaseUnit unit1 in allUnits)
            {
                collided.AddRange(
                    from unit2 in allUnits
                    where _collideCondition(unit1, unit2)
                    select new CollisionPair(unit1, unit2)
                    );
            }

            return collided.Distinct().ToList();
        }

        public static double GetAngle(double currentAngle, Point lastPosition, BaseUnit movingUnit, BaseUnit staticUnit)
        {
            ESide side = GetCollisionSide(movingUnit, lastPosition, staticUnit);

            return _getAngle(currentAngle, side);
        }

        public static ESide GetCollisionSide(BaseUnit movingUnit, Point lastPosition, BaseUnit staticUnit)
        {
            Point center = _getCenter(movingUnit, movingUnit.Position);
            Point lastCenter = _getCenter(movingUnit, lastPosition);

            Rectangle movingRectangle = _getRectangle(movingUnit);
            Rectangle staticRectangle = _getRectangle(staticUnit);

            if (staticRectangle.Contains(center))
                center = lastCenter;

            int leftDelta = center.X - staticRectangle.Left;
            int rightDelta = center.X - staticRectangle.Right;
            int topDelta = center.Y - staticRectangle.Top;
            int bottomDelta = center.Y - staticRectangle.Bottom;

            leftDelta = Math.Abs(leftDelta);
            rightDelta = Math.Abs(rightDelta);
            topDelta = Math.Abs(topDelta);
            bottomDelta = Math.Abs(bottomDelta);

            int minDeltaX = Math.Min(leftDelta, rightDelta);
            int minDeltaY = Math.Min(topDelta, bottomDelta);
            int minDelta = Math.Min(minDeltaY, minDeltaX);

            if (minDelta == leftDelta)
                return ESide.Left;

            if (minDelta == rightDelta)
                return ESide.Right;

            if (minDelta == topDelta)
                return ESide.Top;

            if (minDelta == bottomDelta)
                return ESide.Bottom;

            return ESide.Left;
        }

        private static Point _getCenter(BaseUnit movingUnit, Point position)
        {
            int centerX = position.X + movingUnit.Width / 2;
            int centerY = position.Y + movingUnit.Height / 2;
            var center = new Point(centerX, centerY);
            return center;
        }

        #endregion

        #region Private Methods

        private static bool _collideCondition(BaseUnit baseUnit, BaseUnit unitTmp)
        {
            return baseUnit != unitTmp
                   && baseUnit.UnitType != unitTmp.UnitType
                   && _collidedWith(unitTmp, baseUnit);
        }

        private static bool _collidedWith(BaseUnit unit1, BaseUnit unit2)
        {
            Rectangle rectangle1;
            {
                int unitX = unit1.Position.X;
                int unitY = unit1.Position.Y;
                rectangle1 = new Rectangle(unitX, unitY, unit1.Width, unit1.Height);
            }

            Rectangle rectangle2;
            {
                int unitX = unit2.Position.X;
                int unitY = unit2.Position.Y;
                rectangle2 = new Rectangle(unitX, unitY, unit2.Width, unit2.Height);
            }

            bool collidedWith = rectangle1.IntersectsWith(rectangle2);

            return collidedWith;
        }

        private static double _getAngle(double currentAngle, ESide side)
        {
            switch (side)
            {
                case ESide.Left:
                case ESide.Right:
                    return Math.PI - currentAngle;
                case ESide.Bottom:
                case ESide.Top:
                    return -currentAngle;
            }

            return 0;
        }

        private static Rectangle _getRectangle(BaseUnit baseUnit)
        {
            int unitX = baseUnit.Position.X;
            int unitY = baseUnit.Position.Y;
            return new Rectangle(unitX, unitY, baseUnit.Width, baseUnit.Height);
        }

        #endregion
    }
}