namespace Contorio
{
    public class World
    {
        private List<Planet> _planets;
        private Dictionary<string, int> _tokens;
        private Player _player;
        private ResearchSystem _researchSystem;
        
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

        public Player Player
        {
            get { return _player; }
            init { _player = value; }
        }

        public ResearchSystem ResearchSystem
        {
            get { return _researchSystem; }
            init { _researchSystem = value; }
        }

        public World()
        {
            _planets = new List<Planet>();
            _tokens = new Dictionary<string, int>();
            _player = new Player();

            _planets.Add(new Planet());

            List<Research> tempResearcheList = new List<Research>();
            foreach (var item in ResourceManager.Instance.Blocks)
            {
                tempResearcheList.Add(item.Value.Research);
            }
            _researchSystem = new ResearchSystem(tempResearcheList);
        }
    }
}
