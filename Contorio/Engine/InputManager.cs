namespace Contorio.Engine
{
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


        public delegate void InputHandler(ConsoleKey key);

        private InputHandler? _inputHandlers;

        public void Tick()
        {
            if (Console.KeyAvailable)
            {
                if (_inputHandlers != null)
                {
                    _inputHandlers.Invoke(Console.ReadKey(intercept: true).Key);
                }
            }
        }

        public void AddInputHandler(InputHandler handler)
        {
            _inputHandlers += handler;
        }

        public void RemoveInputHandler(InputHandler handler)
        {
            if (_inputHandlers != null)
            {
                _inputHandlers -= handler;
            }
        }
    }
}
