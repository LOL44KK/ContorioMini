// Contorio
// На будущие
// 1. Переписать WorldScene и MenuScene на новый подход
//    с использованием Engine
// 2. При генерациий мира сделать выбор присета планеты
//    также при поиске
// 3. В Menu добавить надпись с версией игры


using Contorio.CharEngine;
using Contorio.Core;
using Contorio.Core.Managers;

namespace Contorio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModManager.Instance.AddMode(new BaseMod());
            ModManager.Instance.InitializeResources();

            Renderer renderer = new Renderer(120, 30);
            Engine engine = new Engine(renderer);
            engine.SetScene(new Scenes.SceneWorld.SceneWorld(new World()));

            engine.Run();

            /*
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
            */
        }
    }
}