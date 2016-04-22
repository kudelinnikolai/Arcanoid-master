#region using

using System.Drawing;
using Arcanoid.Core.Environment;

#endregion

namespace Arcanoid.Core.Units
{
    public abstract class BaseUnit
    {
        #region Private Fields

        private Point _position = Point.Empty;

        #endregion

        #region Constructors

        protected BaseUnit(ILevelInfo info)
        {
            Info = info;
        }

        #endregion

        #region Public Properties

        public int Height { get; protected set; }

        public virtual Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnitType UnitType { get; protected set; }
        public int Width { get; protected set; }

        #endregion

        #region Protected Properties

        protected Bitmap Image { get; set; }
        protected ILevelInfo Info { get; set; }

        #endregion

        #region Public Methods

        public abstract void Collided(BaseUnit baseUnit);

        public virtual void Draw(Graphics g, Point levelLeftTop)
        {
            //g.DrawImage(Image, Position.X + levelLeftTop.X - Width/2, Position.Y + levelLeftTop.Y - Height/2);
            g.DrawImage(Image, Position.X + levelLeftTop.X, Position.Y + levelLeftTop.Y);
        }

        public bool IsPressed(VirtualKeyStates key)
        {
            bool isPressed = KeyState.IsPressed(key);
            //
            return isPressed;
        }

        public abstract void Load();

        public abstract void Update();

        #endregion

        #region Protected Methods

        protected void Load(string fileName)
        {
            Image = (Bitmap) System.Drawing.Image.FromFile(fileName);
            Image = new Bitmap(Image, new Size(Width, Height));
        }

        #endregion
    }
}