namespace Contorio.Core.Types
{
    public struct Recipe
    {
        private Dictionary<string, int> _input;
        private Dictionary<string, (int Quantity, float Chance)> _output;

        public Dictionary<string, int> Input => _input;
        public Dictionary<string, (int Quantity, float Chance)> Output => _output;

        public Recipe(Dictionary<string, int> input, Dictionary<string, (int Quantity, float Chance)> output)
        {
            _input = input;
            _output = output;
        }
    }
}
