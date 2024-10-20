﻿using Contorio.Core.Types;

namespace Contorio.Core
{
    public class ResearchSystem
    {
        private Dictionary<string, Research> _openResearch;
        private Dictionary<string, Research> _closeResearch;

        public Dictionary<string, Research> OpenResearch
        {
            get { return _openResearch; }
            init { _openResearch = value; }
        }

        public Dictionary<string, Research> CloseResearch
        {
            get { return _closeResearch; }
            init { _closeResearch = value; }
        }

        public ResearchSystem()
        {
            _openResearch = new Dictionary<string, Research>();
            _closeResearch = new Dictionary<string, Research>();
        }

        public ResearchSystem(List<Research> closeResearch, List<Research>? openResearch=null)
        {
            _openResearch = new Dictionary<string, Research>();
            _closeResearch = new Dictionary<string, Research>();

            foreach (var research in closeResearch)
            {
                _closeResearch.Add(research.Name, research);
            }
            if (openResearch != null)
            {
                foreach (var research in openResearch)
                {
                    _openResearch.Add(research.Name, research);
                }
            }

            foreach (var research in _closeResearch)
            {
                if (research.Value.ResearchCost.Count == 0)
                {
                    UnlockResearch(research.Key);
                }
            }
        }

        public bool UnlockResearch(string researchName)
        {
            if (_closeResearch.ContainsKey(researchName))
            {
                Research research = _closeResearch[researchName];
                if (research.RequiredResearch != null)
                {
                    if (!_openResearch.ContainsKey(research.RequiredResearch))
                    {
                        return false;
                    }
                }
                _openResearch.Add(researchName, research);
                _closeResearch.Remove(researchName);
                return true;
            }
            return false;
        }
    }
}
