namespace Contorio
{
    public class Research
    {
        private string _name;
        private string _category;
        private string? _requiredResearch;
        private Dictionary<string, int> _researchCost;

        public string Name
        {
            get { return _name; }
        }
        public string Category
        {
            get { return _category; }
        }
        public string? RequiredResearch
        {
            get { return _requiredResearch; }
        }
        public Dictionary<string, int> ResearchCost
        {
            get { return _researchCost; }
        }

        public Research(string name, string category, string requiredResearch, Dictionary<string, int> researchCost)
        {
            _name = name;
            _category = category;
            _requiredResearch = requiredResearch;
            _researchCost = researchCost;
        }
    }
}
