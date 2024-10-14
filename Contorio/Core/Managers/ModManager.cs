namespace Contorio
{
    public class Mod
    {
        private string _name;
        private string _description;
        private string _version;
        private string _author;

        private List<Block> _blocks;
        private List<Ground> _grounds;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            init { _description = value; }
        }

        public string Version
        {
            get { return _version; }
            init { _version = value; }
        }

        public string Author
        {
            get { return _author; }
            init { _author = value; }
        }
    }

    public class ModManager
    {
        private static ModManager _instance;
        public static ModManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModManager();
                }
                return _instance;
            }
        }
    }
}
