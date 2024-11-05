using System.Diagnostics;
using System.Drawing;

namespace Contorio.CharEngine.Widgets
{
    public class Message : Label
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private int _showMessageTime;
        private int _centerScreenX;

        public int ShowMessageTime
        {
            get { return _showMessageTime; }
            set { _showMessageTime = value; }
        }

        public int CenterScreenX
        {
            get { return _centerScreenX; } 
            set { _centerScreenX = value; }
        }

        public Message(int centerScreenX, int positionY, int showMessageTime = 1000, int layer = 10)
            : base("NONE", ConsoleColor.Red, new Point(0, positionY), layer, false)
        {
            _centerScreenX = centerScreenX;
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
            _position = new Point(CenterScreenX - (Width / 2), _position.Y);
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
