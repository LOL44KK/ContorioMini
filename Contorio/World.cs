namespace Contorio
{
    public class World
    {
        private List<Planet> _planets;
        private Dictionary<string, int> _tokens;
        
        public List<Planet> Planets
        {
            get { return _planets; }
            init { _planets = value; }
        }

        public Dictionary<string, int> Tokens
        {
            get { return _tokens; }
            set { _tokens = value; }
        }

        public World()
        {
            _planets = new List<Planet>();
            _tokens = new Dictionary<string, int>();

            _planets.Add(new Planet());
        }
    }
}
