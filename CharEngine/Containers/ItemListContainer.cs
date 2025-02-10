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

        public ItemListContainer(List<ItemList> itemLists)
        {
            _itemLists = itemLists;
            _selectedIndex = 0;
        }

        public ItemListContainer() : this(new List<ItemList>()) { }

        public void AddItemList(ItemList itemList)
        {
            if (_selectedIndex == _itemLists.Count)
            {
                itemList.Select();
            }
            else
            {
                itemList.Unselect();
            }
            _itemLists.Add(itemList);
        }

        public void RemoveItemList(ItemList itemList)
        {
            _itemLists.Remove(itemList);
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

        public bool IsSelectedItemList(ItemList itemList)
        {
            return _itemLists[_selectedIndex] == itemList;
        }

        private void InitializeItemListColors()
        {
            for (int i = 0; i < _itemLists.Count; i++)
            {
                if (i == _selectedIndex)
                {
                    _itemLists[i].Select();
                }
                else
                {
                    _itemLists[i].Unselect();
                }
            }
        }
    }
}
