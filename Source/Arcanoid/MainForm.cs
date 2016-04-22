#region using

using System.Windows.Forms;
using Arcanoid.Core.Engine;
using Arcanoid.Environments;

#endregion

namespace Arcanoid
{
    public partial class MainForm : Form
    {
        #region Private Fields

        private readonly Engine m_engine;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            m_engine = new Engine(canvas, typeof (LevelOne));

            FormClosing += (o, e) => m_engine.Stop();

            m_engine.Start();
        }

        #endregion
    }
}