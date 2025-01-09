using Contorio.Core.Types;
using Contorio.Core.Managers;
using Contorio.Core.Presets;

namespace Contorio.Core
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

            List<Research> tempResearcheList = new List<Research>();
            foreach (var item in ResourceManager.Instance.Blocks)
            {
                tempResearcheList.Add(item.Value.Research);
            }
            _researchSystem = new ResearchSystem(tempResearcheList);
        }

        public World(PlanetPreset basePlanetPreset) : this()
        {
            _planets.Add(new Planet(basePlanetPreset));
            _player.Move(basePlanetPreset.Size / 2, basePlanetPreset.Size / 2);
        }

        public bool SearchPlanet(PlanetPreset preset)
        {
            int cost = CalculateCostSearchPlanet(preset);
            if (_tokens.GetValueOrDefault("PL", 0) >= cost)
            {
                _planets.Add(new Planet(preset));
                _tokens["PL"] -= cost;
                return true;
            }
            return false;
        }

        public bool StudyResearch(string researchName)
        {
            if (ResourceManager.Instance.Researches.TryGetValue(researchName, out Research? research))
            {
                foreach (var token in research.ResearchCost)
                {
                    if (_tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                    {
                        return false;
                    }
                }
                if (research.RequiredResearch != null)
                {
                    if (!_researchSystem.Researchs.GetValueOrDefault(research.RequiredResearch, false))
                    {
                        return false;
                    }
                }
                foreach (var token in research.ResearchCost)
                {
                    _tokens[token.Key] -= token.Value;
                }
                _researchSystem.UnlockResearch(researchName);
                return true;
            }
            return false;
        }

        public static int CalculateCostSearchPlanet(PlanetPreset preset)
        {
            int cost = preset.Size * 16;
            foreach (var ore in preset.Ores)
            {
                cost += ((int)(ore.Chance * 10000)) * (int)Math.Pow(preset.Size, 2);
            }
            return cost;
        }
    }
}
