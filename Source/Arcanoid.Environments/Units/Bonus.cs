using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcanoid.Core.Collision;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;
using System.Drawing;

namespace Arcanoid.Environments.Units
{
    class Bonus : BaseUnit
        
    {
        #region Constants

        internal const double Speed = 2;

        #endregion

        #region Private Fields
        #endregion

        #region Constructors

        public Bonus(ILevelInfo info)
            : base(info)
        {
            Width = 20;
            Height = 20;
            UnitType = UnitType.Bonus;
        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Methods

        public override void Collided(BaseUnit baseUnit)
        {
            if (baseUnit.UnitType == UnitType.Player)
            {
                Info.RemoveUnit(this);
            }
        }

        public override void Load()
        {
            int bonusNumber = new Random().Next(1, 4);
            Load(string.Format(@"Assets\\bonuses\bonus{0}.png", bonusNumber));
        }

        public override void Update()
        {
            Point newPosition = new Point(Position.X, Position.Y + (int)Speed);

            Position = newPosition;

            Size levelSize = Info.GetLevelSize();
            if (Position.Y > levelSize.Height)
                Info.RemoveUnit(this);

        }

        #endregion
    }

    public enum TypeOfBonus
    {
        IncreasePlayer = 1,
        MoreLife = 2,
        SlowBall= 3,
        DoubleBall = 4
    }
}
