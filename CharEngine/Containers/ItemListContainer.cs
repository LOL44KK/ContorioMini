using CharEngine.Widgets;

namespace CharEngine.Containers
{
    public class ItemListContainer
    {
        private List<ItemList> _itemLists;
        private int _selectedIndex;
        private ConsoleColor _activeListSelectedItemColor;
        private ConsoleColor _inactiveListSelectedItemColor;

        public ConsoleColor ActiveListSelectedItemColor
        {
            get { return _activeListSelectedItemColor; }
            set
            {
                _activeListSelectedItemColor = value;
                InitializeItemListColors();
            }
        }

        public ConsoleColor InactiveListSelectedItemColor
        {
            get { return _inactiveListSelectedItemColor; }
            set
            {
                _inactiveListSelectedItemColor = value;
                InitializeItemListColors();
            }
        }

        public int SelectedItemList
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                InitializeItemListColors();
            }
        }

        public ItemListContainer(List<ItemList> itemLists, ConsoleColor activeListSelectedItemColor, ConsoleColor inactiveListSelectedItemColor)
        {
            _itemLists = new List<ItemList>();
            _selectedIndex = 0;
            _activeListSelectedItemColor = activeListSelectedItemColor;
            _inactiveListSelectedItemColor = inactiveListSelectedItemColor;
        }

        public ItemListContainer(ConsoleColor activeListSelectedItemColor = ConsoleColor.DarkBlue, ConsoleColor inactiveListSelectedItemColor = ConsoleColor.Blue) : this(new List<ItemList>(), activeListSelectedItemColor, inactiveListSelectedItemColor) { }

        public void AddItemList(ItemList itemList)
        {
            itemList.SelectedItemColor = _inactiveListSelectedItemColor;
            if (_selectedIndex == _itemLists.Count)
            {
                itemList.SelectedItemColor = _activeListSelectedItemColor;
            }
            else
            {
                itemList.SelectedItemColor = _inactiveListSelectedItemColor;
            }
            _itemLists.Add(itemList);
        }

        public void RemoveItemList(ItemList itemList)
        {
            _itemLists.Remove(itemList);
            itemList.SelectedItemColor = _activeListSelectedItemColor;
        }

        public void PreviousItem()
        {
            _itemLists[_selectedIndex].PreviousItem();
        }

        public void NextItem()
        {
            _itemLists[_selectedIndex].NextItem();
        }

        public void NextItemList()
        {
            if (_selectedIndex < _itemLists.Count - 1)
            {
                SelectedItemList++;
            }
        }

        public void PreviousItemList()
        {
            if (_selectedIndex > 0)
            {
                SelectedItemList--;
            }
        }

        private void InitializeItemListColors()
        {
            for (int i = 0; i < _itemLists.Count; i++)
            {
                if (i == _selectedIndex)
                {
                    _itemLists[i].SelectedItemColor = _activeListSelectedItemColor;
                }
                else
                {
                    _itemLists[i].SelectedItemColor = _inactiveListSelectedItemColor;
                }
            }
        }
    }
}
