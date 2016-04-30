#region using

using System;
using System.Drawing;
using System.Windows.Forms;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Environments.Units
{
    public class Block : BaseUnit
    {
        public const int DefaultWidth = 30;

        #region Constructors

        public Block(ILevelInfo info) : base(info)
        {
            Width = DefaultWidth;
            Height = 15;
            UnitType = UnitType.Block;
        }

        #endregion

        #region Public Methods

        public override void Collided(BaseUnit baseUnit)
        {
            if (baseUnit.UnitType == UnitType.Ball)
            {
                Info.RemoveUnit(this);

                //если блок с бонусом - на этом месте генерируем рандомный бонус
                Bonus bonus = new Bonus(Info);
                bonus.Load();
                bonus.Position = new Point(Position.X, Position.Y);
                
                Info.AddUnit(bonus);
            }
        }

        public override void Load()
        {
            int blockNumber = new Random().Next(1, 6);
            Load(string.Format(@"Assets\\blocks\block{0}.gif", blockNumber));
        }

        public override void Update()
        {
        }

        #endregion
    }
}