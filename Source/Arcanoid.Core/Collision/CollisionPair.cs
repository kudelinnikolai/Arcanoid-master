#region using

using System;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Core.Collision
{
    public class CollisionPair : Tuple<BaseUnit, BaseUnit>
    {
        #region Constructors

        public CollisionPair(BaseUnit item1, BaseUnit item2)
            : base(item1, item2)
        {
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            CollisionPair newPair = obj as CollisionPair;
            return _equals(newPair);
        }

        public override int GetHashCode()
        {
            return Item2.GetHashCode() + Item1.GetHashCode();
        }

        #endregion

        #region Private Methods

        private bool _equals(CollisionPair other)
        {
            if (other == null)
                return false;

            bool straitCompare = other.Item1 == Item1 && other.Item2 == Item2;
            bool reverseCompare = other.Item2 == Item1 && other.Item1 == Item2;

            return straitCompare || reverseCompare;
        }

        #endregion

        public void Collided()
        {
            Item2.Collided(Item1);
            Item1.Collided(Item2);
        }
    }
}