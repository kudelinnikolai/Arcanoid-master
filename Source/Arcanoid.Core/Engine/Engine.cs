#region using

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Arcanoid.Core.Environment;

#endregion

namespace Arcanoid.Core.Engine
{
    public class Engine
    {
        #region Private Fields

        private readonly PictureBox _canvas;
        private readonly int _targetFps;
        private BaseLevel _level;

        #endregion

        #region Constructors

        public Engine(PictureBox canvas, Type firstLevel)
        {
            _canvas = canvas;
            _targetFps = 60;

            StartScreen.LevelOne = firstLevel;
        }

        #endregion

        #region Private Properties

        private bool IsRunning { get; set; }

        private BaseLevel Level
        {
            get { return _level; }
            set
            {
                _level = value;
                _level.Load();
                _canvas.Width = _level.Size.Width;
                _canvas.Height = _level.Size.Height;
            }
        }

        private Thread RunningThread { get; set; }

        #endregion

        #region Public Methods

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            Level = new StartScreen();

            IsRunning = true;
            RunningThread = new Thread(_threadRun);
            RunningThread.Start();
        }

        public void Stop()
        {
            IsRunning = false;

            while (RunningThread.IsAlive)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        #endregion

        #region Private Methods

        private void _threadRun()
        {
            Stopwatch framecounter = new Stopwatch();
            Stopwatch tickcounter = new Stopwatch();

            while (IsRunning)
            {
                framecounter.Start();
                tickcounter.Start();

                _tick();

                tickcounter.Stop();

                float targettime = 1f/_targetFps*1000f;
                long actualtime = tickcounter.ElapsedMilliseconds;
                float sleep = targettime - actualtime;
                if (sleep > 0)
                {
                    Thread.Sleep((int) sleep);
                }

                tickcounter.Reset();
                framecounter.Stop();

                if (framecounter.ElapsedMilliseconds < 1000)
                {
                    continue;
                }

                framecounter.Reset();
            }
        }

        private void _tick()
        {
            Bitmap frame = new Bitmap(Level.Size.Width, Level.Size.Height);

            using (Graphics graphics = Graphics.FromImage(frame))
            {
                Level.Update();
                Level.Draw(graphics);
                graphics.Flush();
            }

            if (Level.Success)
                Level = Level.NextLevel();

            if (Level.Failed)
            {
                Level = new FailScreen();
            }

            try
            {
                _canvas.Invoke(new Action(() =>
                {
                    _canvas.Parent.Size = frame.Size;
                    _canvas.Size = frame.Size;
                    if (_canvas.Image != null)
                    {
                        _canvas.Image.Dispose();
                    }
                    _canvas.Image = frame;
                }));
            }
            catch (ObjectDisposedException)
            {
            }
        }

        #endregion
    }
}