#region using

using System.Drawing;
using Arcanoid.Core.Units;

#endregion

namespace Arcanoid.Core.Environment
{
    public interface ILevelInfo
    {
        #region Public Methods

        void AddUnit(BaseUnit baseUnit);
        Size GetLevelSize();
        void RemoveUnit(BaseUnit baseUnit);

        #endregion
    }
}