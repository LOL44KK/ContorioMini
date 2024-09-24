namespace Contorio
{
    public class World
    {
        private List<Planet> _planets;
        private Dictionary<string, int> _tokens;

        public List<Planet> Plantes
        {
            get { return _planets; }
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
