#region using

using System.Collections.Generic;
using System.Drawing;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Core.Environment
{
    public abstract class BaseLevel : ILevelInfo
    {
        #region Constants

        public const int DefaultHeight = 480;
        public const int DefaultWidth = 650;

        #endregion

        #region Private Fields

        private Image _image;

        #endregion

        #region Constructors

        protected BaseLevel()
        {
            Units = new List<BaseUnit>();

            Size = new Size(DefaultWidth, DefaultHeight);
        }

        #endregion

        #region Public Properties

        public bool Failed { get; set; }
        public Size Size { get; set; }
        public bool Success { get; set; }

        #endregion

        #region Protected Properties

        protected string FileName { get; set; }
        protected Point LeftTop { get; set; }
        protected Size PlayingSize { get; set; }
        protected List<BaseUnit> Units { get; set; }

        #endregion

        #region ILevelInfo Members

        public Size GetLevelSize()
        {
            return PlayingSize;
        }

        public void AddUnit(BaseUnit baseUnit)
        {
            Units.Add(baseUnit);
        }

        public void RemoveUnit(BaseUnit baseUnit)
        {
            Units.Remove(baseUnit);
        }

        #endregion

        #region Public Methods

        public void Draw(Graphics graphics)
        {
            _draw(graphics);

            BaseUnit[] baseUnits = new BaseUnit[Units.Count];
            Units.CopyTo(baseUnits);
            foreach (BaseUnit unit in baseUnits)
            {
                unit.Draw(graphics, LeftTop);
            }
        }

        public void Load()
        {
            _load();

            BaseUnit[] baseUnits = new BaseUnit[Units.Count];
            Units.CopyTo(baseUnits);
            foreach (BaseUnit unit in baseUnits)
            {
                unit.Load();
            }
        }

        public abstract BaseLevel NextLevel();

        public virtual void Update()
        {
            BaseUnit[] baseUnits = new BaseUnit[Units.Count];
            Units.CopyTo(baseUnits);
            foreach (BaseUnit baseUnit in baseUnits)
            {
                baseUnit.Update();
            }
        }

        #endregion

        #region Protected Methods

        protected bool IsPressed(VirtualKeyStates key)
        {
            return KeyState.IsPressed(key);
        }

        #endregion

        #region Private Methods

        private void _draw(Graphics graphics)
        {
            graphics.DrawImage(_image, 0, 0);
        }

        private void _load()
        {
            _image = (Bitmap) Image.FromFile(FileName);
            _image = new Bitmap(_image, Size);
        }

        #endregion
    }
}