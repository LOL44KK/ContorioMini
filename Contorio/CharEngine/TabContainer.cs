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

        public void AddSprite(int indexTab, Sprite sprite)
        {
            _containers[indexTab].AddSprite(sprite);
        }

        public void RemoveSprite(int indexTab, Sprite sprite)
        {
            _containers[indexTab].RemoveSprite(sprite);
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
