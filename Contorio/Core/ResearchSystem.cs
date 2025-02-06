using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Core
{
    public class ResearchSystem
    {
        private Dictionary<string, bool> _researchs;

        public Dictionary<string, bool> Researchs
        {
            get { return _researchs; } 
            init { _researchs = value; }
        }

        public int CountCloseResearchs
        {
            get 
            {
                int count = 0;
                foreach (var research in _researchs)
                {
                    if (research.Value == false)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public ResearchSystem()
        {
            _researchs = new Dictionary<string, bool>();
        }

        public ResearchSystem(List<Research> researchs)
        {
            _researchs = new Dictionary<string, bool>();
            
            foreach (var research in researchs)
            {
                _researchs.Add(research.Name, false);
            }

            foreach (var research in researchs)
            {
                if (research.ResearchCost == null || research.ResearchCost.Count == 0)
                {
                    _researchs[research.Name] = true;
                }
            }
        }

        public bool UnlockResearch(string researchName)
        {
            if (_researchs.ContainsKey(researchName))
            {
                Research research = ResourceManager.Instance.Researches[researchName];
                if (research.RequiredResearch != null)
                {
                    if (!_researchs.GetValueOrDefault(research.RequiredResearch, false))
                    {
                        return false;
                    }
                }
                _researchs[researchName] = true;
                return true;
            }
            return false;
        }
    }
}
