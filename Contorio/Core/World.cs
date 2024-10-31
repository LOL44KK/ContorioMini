using System.Drawing;

using Contorio.Core.Types;
using Contorio.Core.Managers;

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

            _planets.Add(new Planet());

            List<Research> tempResearcheList = new List<Research>();
            foreach (var item in ResourceManager.Instance.Blocks)
            {
                tempResearcheList.Add(item.Value.Research);
            }
            _researchSystem = new ResearchSystem(tempResearcheList);
            _player.Coord = new Point(_planets[0].Size / 2, _planets[0].Size / 2);
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
            foreach (var token in _researchSystem.CloseResearch[researchName].ResearchCost)
            {
                if (_tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                {
                    return false;
                }
            }
            if (_researchSystem.CloseResearch[researchName].RequiredResearch != null)
            {
                if (!_researchSystem.OpenResearch.ContainsKey(_researchSystem.CloseResearch[researchName].RequiredResearch))
                {
                    return false;
                }
            }
            foreach (var token in _researchSystem.CloseResearch[researchName].ResearchCost)
            {
                _tokens[token.Key] -= token.Value;
            }
            _researchSystem.UnlockResearch(researchName);
            return true;
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
