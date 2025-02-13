using System.Collections.ObjectModel;
using System.Drawing;

namespace CharEngine.Widgets
{
    public class ItemList : Sprite
    {
        private List<string> _items;
        private int _selectedIndex;
        private int _scrollOffset;
        private int _visibleItemCount;
        private bool _selected;
        private ConsoleColor _textColor;
        private ConsoleColor _selectedItemTextColor;
        private ConsoleColor _unselectItemTextColor;
        private TextAlignment _textAlignment;

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

        public string? SelectedItem
        {
            get
            {
                if (_items.Count == 0)
                {
                    return null;
                }
                return _items[_selectedIndex];
            }
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

        public ConsoleColor SelectedItemTextColor
        {
            get { return _selectedItemTextColor; }
            set
            {
                _selectedItemTextColor = value;
                UpdatePixels();
            }
        }

        public ConsoleColor UnselectItemTextColor
        {
            get { return _unselectItemTextColor; }
            set
            {
                _unselectItemTextColor = value;
                UpdatePixels();
            }
        }

        public TextAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            {
                _textAlignment = value;
                UpdatePixels();
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                UpdatePixels();
            }
        }

        public ReadOnlyCollection<string> Items
        {
            get { return new ReadOnlyCollection<string>(_items); }
        }

        public ItemList(
            ConsoleColor textColor,
            ConsoleColor selectedItemTextColor,
            ConsoleColor unselectedItemTextColor,
            Point position,
            int visibleItemCount,
            bool selected = true,
            int layer = 0,
            bool visible = true,
            Alignment alignment = Alignment.Left,
            TextAlignment textAlignment = TextAlignment.Left
        )
        : base(new Pixel[visibleItemCount, 0], position, layer, visible, alignment)
        {
            _items = new List<string>();
            _textColor = textColor;
            _selectedItemTextColor = selectedItemTextColor;
            _unselectItemTextColor = unselectedItemTextColor;
            _selectedIndex = 0;
            _scrollOffset = 0;
            _visibleItemCount = visibleItemCount;
            _textAlignment = textAlignment;
            _selected = true;
        }

        public ItemList(
            ConsoleColor textColor,
            ConsoleColor selectedItemTextColor,
            Point position,
            int visibleItemCount,
            bool selected = true,
            int layer = 0,
            bool visible = true,
            Alignment alignment = Alignment.Left,
            TextAlignment textAlignment = TextAlignment.Left
        )
            : this(
                  textColor,
                  selectedItemTextColor,
                  selectedItemTextColor,
                  position,
                  visibleItemCount,
                  selected,
                  layer,
                  visible,
                  alignment,
                  textAlignment
            )
        { }
        
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

        public void Select()
        {
            Selected = true;
        }

        public void Unselect()
        {
            Selected = false;
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
            int width = _items.Any() ? _items.Max(item => item.Length) : 0;
            Pixel[,] pixels = new Pixel[_visibleItemCount, width];

            for (int y = 0; y < _visibleItemCount; y++)
            {
                int itemIndex = y + _scrollOffset;

                string item = (itemIndex < _items.Count) ? _items[itemIndex] : new string(' ', width);
                // блять
                ConsoleColor color = itemIndex == _selectedIndex ? _selected ? _selectedItemTextColor : _unselectItemTextColor : _textColor;

                int offsetX = 0;
                switch (_textAlignment)
                {
                    case TextAlignment.Center:
                        offsetX = (width - item.Length) / 2;
                        break;
                    case TextAlignment.Right:
                        offsetX = width - item.Length;
                        break;
                }

                for (int x = 0; x < width; x++)
                {
                    pixels[y, x] = new Pixel(
                        x >= offsetX && x < offsetX + item.Length ? item[x - offsetX] : ' ',
                        color
                    );
                }
            }
            Pixels = pixels;
        }
    }
}
