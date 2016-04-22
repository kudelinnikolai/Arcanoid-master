#region using

using System;

#endregion

namespace Arcanoid.Core.Environment
{
    public class StartScreen : BaseLevel
    {
        #region Constructors

        public StartScreen()
        {
            FileName = @"Assets\StartScreen.png";
        }

        #endregion

        #region Public Properties

        public static Type LevelOne { get; set; }

        #endregion

        #region Public Methods

        public override BaseLevel NextLevel()
        {
            return (BaseLevel) Activator.CreateInstance(LevelOne);
        }

        public override void Update()
        {
            base.Update();

            if (IsPressed(VirtualKeyStates.Return))
                Success = true;
        }

        #endregion
    }
}