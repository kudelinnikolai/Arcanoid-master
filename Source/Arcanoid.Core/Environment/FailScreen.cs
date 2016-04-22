#region using

#endregion

namespace Arcanoid.Core.Environment
{
    public class FailScreen : BaseLevel
    {
        #region Constructors

        public FailScreen()
        {
            FileName = @"Assets\FailScreen.png";
        }

        #endregion

        #region Public Methods

        public override BaseLevel NextLevel()
        {
            return new StartScreen();
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