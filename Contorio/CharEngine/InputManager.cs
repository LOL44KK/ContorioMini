namespace Contorio.CharEngine
{
    public delegate void InputDelegate(ConsoleKey key);

    public class InputManager
    {
        private static InputManager? _instance;
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }

        public event InputDelegate? InputHandlers;

        public void Tick()
        {
            if (Console.KeyAvailable)
            {
                if (InputHandlers != null)
                {
                    InputHandlers.Invoke(Console.ReadKey(intercept: true).Key);
                }
            }
        }
    }
}
