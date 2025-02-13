using System.Diagnostics;
using System.Drawing;

namespace CharEngine.Widgets
{
    public class Message : Label
    {
        private Stopwatch _stopwatch;
        private int _showMessageTime;

        public int ShowMessageTime
        {
            get { return _showMessageTime; }
            set { _showMessageTime = value; }
        }

        public Message(
            Point position,
            int showMessageTime = 1000,
            int layer = 10,
            Alignment alignment = Alignment.Center,
            TextAlignment textAlignment = TextAlignment.Center
        )
            : base(
                  "NONE",
                  ConsoleColor.Red,
                  position,
                  layer,
                  false,
                  alignment,
                  textAlignment
            )
        {
            _stopwatch = new Stopwatch();
            _showMessageTime = showMessageTime;
        }

        public void Show(string text, ConsoleColor textColor, int showMessageTime = -1)
        {
            Text = text;
            TextColor = textColor;
            Visible = true;
            if (showMessageTime != -1)
            {
                _showMessageTime = showMessageTime;
            }
            _stopwatch.Restart();
        }

        override public void Tick()
        {
            if (Visible)
            {
                if (_stopwatch.Elapsed.TotalMilliseconds >= _showMessageTime)
                {
                    Visible = false;
                    _stopwatch.Stop();
                }
            }
        }
    }
}
