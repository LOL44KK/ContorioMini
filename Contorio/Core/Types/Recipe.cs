namespace Contorio
{
    public struct Recipe
    {
        private Dictionary<string, int> _input;
        private Dictionary<string, int> _output;

        public Dictionary<string, int> Input => _input;
        public Dictionary<string, int> Output => _output;

        public Recipe(Dictionary<string, int> input, Dictionary<string, int> output)
        {
            _input = input;
            _output = output;
        }
    }
}
