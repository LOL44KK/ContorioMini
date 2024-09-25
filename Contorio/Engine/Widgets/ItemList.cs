using System.Drawing;

namespace Contorio.Engine.Widgets
{
    public class ItemList : Sprite
    {
        private List<string> _items;
        private int _selectedIndex;
        private int _scrollOffset;
        private int _visibleItemCount;
        private ConsoleColor _textColor;
        private ConsoleColor _selectedItemColor;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value >= 0 && value < _items.Count)
                {
                    _selectedIndex = value;
                    EnsureItemVisible();
                    UpdatePixels();
                }
            }
        }

        public string SelectedItem
        { 
            get { return _items[SelectedIndex]; }
            set
            {
                int index = _items.FindIndex(a => a == value);
                if (index != -1)
                {
                    _selectedIndex = index;
                    EnsureItemVisible();
                    UpdatePixels();
                }
            }
        }

        public ConsoleColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                UpdatePixels();
            }
        }

        public ConsoleColor SelectedItemColor
        {
            get { return _selectedItemColor; }
            set
            {
                _selectedItemColor = value;
                UpdatePixels();
            }
        }

        public ItemList(ConsoleColor textColor, ConsoleColor selectedItemColor, Point position, int visibleItemCount, int layer = 0, bool visible = true)
            : base(new Pixel[visibleItemCount, 0], layer, visible, position)
        {
            _items = new List<string>();
            _textColor = textColor;
            _selectedItemColor = selectedItemColor;
            _selectedIndex = 0;
            _scrollOffset = 0;
            _visibleItemCount = visibleItemCount;
        }

        public void AddItem(string item)
        {
            _items.Add(item);
            UpdatePixels();
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items.RemoveAt(index);
                if (_selectedIndex >= _items.Count)
                {
                    _selectedIndex = _items.Count - 1;
                }
                EnsureItemVisible();
                UpdatePixels();
            }
        }

        public void ClearItems()
        {
            _items.Clear();
            _selectedIndex = 0;
            _scrollOffset = 0;
            UpdatePixels();
        }

        public void NextItem()
        {
            if (_selectedIndex < _items.Count - 1)
            {
                SelectedIndex++;
            }
        }

        public void PreviousItem()
        {
            if (_selectedIndex > 0)
            {
                SelectedIndex--;
            }
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0)
            {
                _scrollOffset--;
                UpdatePixels();
            }
        }

        public void ScrollDown()
        {
            if (_scrollOffset + _visibleItemCount < _items.Count)
            {
                _scrollOffset++;
                UpdatePixels();
            }
        }

        private void EnsureItemVisible()
        {
            if (_selectedIndex < _scrollOffset)
            {
                _scrollOffset = _selectedIndex;
            }
            else if (_selectedIndex >= _scrollOffset + _visibleItemCount)
            {
                _scrollOffset = _selectedIndex - _visibleItemCount + 1;
            }
        }

        private void UpdatePixels()
        {
            int width = _items.Count > 0 ? GetMaxItemWidth() : 0;
            Pixel[,] pixels = new Pixel[_visibleItemCount, width];

            for (int y = 0; y < _visibleItemCount; y++)
            {
                int itemIndex = y + _scrollOffset;

                if (itemIndex >= _items.Count)
                {
                    for (int x = 0; x < width; x++)
                    {
                        pixels[y, x] = new Pixel(' ', _textColor);
                    }
                }
                else
                {
                    string item = _items[itemIndex];
                    ConsoleColor color = itemIndex == _selectedIndex ? _selectedItemColor : _textColor;

                    for (int x = 0; x < item.Length; x++)
                    {
                        pixels[y, x] = new Pixel(item[x], color);
                    }

                    for (int x = item.Length; x < width; x++)
                    {
                        pixels[y, x] = new Pixel(' ', color);
                    }
                }
            }

            Pixels = pixels;
        }

        private int GetMaxItemWidth()
        {
            int maxWidth = 0;
            foreach (string item in _items)
            {
                if (item.Length > maxWidth)
                {
                    maxWidth = item.Length;
                }
            }
            return maxWidth;
        }
    }
}
