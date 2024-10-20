namespace Contorio.Core.Types
{

    // Используется для хранение состояния пола
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