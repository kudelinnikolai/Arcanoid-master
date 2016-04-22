#region using

using System;
using System.Runtime.InteropServices;

#endregion

namespace Arcanoid.Core.Environment
{
    public class KeyState
    {
        #region Public Methods

        public static bool IsPressed(VirtualKeyStates key)
        {
            return Convert.ToBoolean(GetKeyState(key) & 0x8000);
        }

        #endregion

        #region Private Methods

        [DllImport("USER32.dll")]
        private static extern short GetKeyState(VirtualKeyStates nVirtKey);

        #endregion
    }
}