#region using

using System;
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
                Info.RemoveUnit(this);
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