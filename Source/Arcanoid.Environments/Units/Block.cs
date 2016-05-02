#region using

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Environments.Units
{
    public class Block : BaseUnit
    {
        public const int DefaultWidth = 30;

        private const int MaxHits = 4;
        private const int ImgToOneNumHits = 2;
        private const int BonusNoChance = 2;

        #region Private fields      
        private int _hitsToDestroy;
        private bool _isBonus;
        #endregion

        #region Public Properties     

        public int TypeBonus { get; set; }
        #endregion

        #region Constructors

        public Block(ILevelInfo info) : base(info)
        {
            _GenerateHitsToDestroy();
            _GenerateBonusOrNo();

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
                if (_hitsToDestroy == 1)
                {
                    Info.RemoveUnit(this);

                    if (_isBonus)
                    {
                        Bonus bonus = new Bonus(Info);
                        bonus.Load();
                        bonus.Position = new Point(Position.X, Position.Y);

                        Info.AddUnit(bonus);
                    }
                }
                else
                {
                    _Reload(--_hitsToDestroy);

                }
            }
        }

        public override void Load()
        {
            int blockNumber = _hitsToDestroy * 2 - new Random().Next(0, ImgToOneNumHits);
            Load(string.Format(@"Assets\\blocks\block{0}.png", blockNumber));
        }

        public override void Update()
        {
        }

        #endregion

        #region Private methods
        
        private void _GenerateBonusOrNo()
        {
            Random rnd = new Random();
            _isBonus = (rnd.Next(0, BonusNoChance) == 0);
        }

        private void _GenerateHitsToDestroy()
        {
            Random rnd = new Random();
            _hitsToDestroy = rnd.Next(1, MaxHits + 1); //   ПЕРЕСЧИТАТЬ ВСЕ РАНДОМНЫЕ ЧИСЛА!!!
        }

        private void _Reload(int hitsToDestroy)
        {
            int blockNum = hitsToDestroy * 2 - new Random().Next(0, ImgToOneNumHits);
            Load(string.Format(@"Assets\\blocks\block{0}.png", blockNum));
        }
        #endregion
    }
}