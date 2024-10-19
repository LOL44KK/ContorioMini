namespace Contorio.Core.Types
{
    public class GroundState
    {
        private string _name;

        public string Name => _name;

        public GroundState(string name)
        {
            _name = name;
        }
    }
}