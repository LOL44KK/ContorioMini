// Contorio
// На будущие
// 1. CharGraphics в CharEngine
//    1. В Sprite добавить метод Tick
//    2. В Scene Добавить
//       Делегаты
//       1. Input(ConsoleKey key) будет вызыватся при каждом нажатии клавиш
//       2. Tick будет вызыватся при каждом тике программы
//       Методы
//       1. public InputInvoke(ConsoleKey key)
//          будет вызывать делегаты Input .Invoke(key)
//       2. public TickInvoke()
//          будет вызывать делегаты Tick .Invoke()
//    3. Добавить класс Engine
//       Поля:
//       1. private Renderer _renderer
//       2. private Scene _scene
//       Методы:
//       1. public Run()
//          Проверка присутствует ли сцена
//          Вызывает MainLoop()
//       2. public SetScene(Scene scene)
//          _scene = scene
//          _renderer.SetScene(scene)
//          if scene != null
//              InputManager.RemoveInputHandler(scene.InputInvoke)
//          InputManager.AddInputHandler(scene.InputInvoke)
//       3. private MainLoop() 
//          Будет вызывать Tick из InputManager
//          Будет вызывать делагати из Scene Tick, из Scene Sprites метод Tick
//          Из Renderer будет вызивать метод Render
// 2. При генерациий мира сделать выбор присета планеты
//    также при поиске
// 3. В Menu добавить надпись с версией игры

namespace Contorio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool DEBUG = true;

            if (DEBUG)
            {
                new Contorio().Run();
            }
            else
            {
                try
                {
                    new Contorio().Run();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}