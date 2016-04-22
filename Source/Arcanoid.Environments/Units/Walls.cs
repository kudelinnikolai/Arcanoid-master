#region using

using System.Drawing;
using Arcanoid.Core.Environment;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Environments.Units
{
    public class Walls : BaseUnit
    {
        #region Constructors

        private Walls(ILevelInfo info)
            : base(info)
        {
            UnitType = UnitType.Wall;
        }

        #endregion

        #region Public Methods

        public override void Collided(BaseUnit baseUnit)
        {
        }

        public static Walls GetLeftWall(ILevelInfo info)
        {
            Size levelSize = info.GetLevelSize();

            const int width = 30;
            Walls wall = new Walls(info)
            {
                Position = new Point(-width, -1*width),
                Width = width,
                Height = levelSize.Height + 2*width
            };

            return wall;
        }

        public static Walls GetRightWall(ILevelInfo info)
        {
            Size levelSize = info.GetLevelSize();

            const int width = 30;
            Walls wall = new Walls(info)
            {
                Position = new Point(levelSize.Width, -1*width),
                Width = width,
                Height = levelSize.Height + 2*width
            };

            return wall;
        }

        public static Walls GetTopWall(ILevelInfo info)
        {
            Size levelSize = info.GetLevelSize();

            const int height = 30;
            Walls wall = new Walls(info)
            {
                Position = new Point(-height, -height),
                Width = levelSize.Width + 2*height,
                Height = height
            };

            return wall;
        }

        public override void Load()
        {
            Load("Assets\\wall.png");
        }

        public override void Update()
        {
        }

        #endregion
    }
}