namespace Contorio.CharEngine
{
    public class TabContainer
    {
        private List<Container> _containers;
        private int _selectedTab;

        public int SelectedTab
        {
            get { return _selectedTab; }
            set 
            { 
                _selectedTab = value;
                foreach (var container in _containers)
                {
                    container.Visible = false;
                }
                _containers[_selectedTab].Visible = true;
            }
        }

        public TabContainer()
        {
            _containers = new List<Container>();
        }

        public int AddTab()
        {
            _containers.Add(new Container());
            return _containers.Count - 1;
        }

        public void AddSprite(int tabIndex, Sprite sprite)
        {
            _containers[tabIndex].AddSprite(sprite);
        }

        public void RemoveSprite(int tabIndex, Sprite sprite)
        {
            _containers[tabIndex].RemoveSprite(sprite);
        }

        public void NextTab()
        {
            if (_selectedTab < _containers.Count - 1)
            {
                SelectedTab = _selectedTab + 1;
            }
        }

        public void PreviousTab()
        {
            if (_selectedTab > 0)
            {
                SelectedTab = _selectedTab - 1;
            }
        }
    }
}
