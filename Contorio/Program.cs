// Contorio
// На будущие
// 1. Переписать WorldScene и MenuScene на новый подход
//    с использованием Engine
// 2. При генерациий мира сделать выбор присета планеты
//    также при поиске
// 3. В Menu добавить надпись с версией игры


using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;

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