// Contorio
// На будущие
// 1. До переписать MenuScene
// 2. Доделать WorldScene
// 3. В Menu добавить надпись с версией игры
// 4. Подумать на структурой движка
// 5. Подумать над менеджарами в Core
// 6. При генерациий мира сделать выбор присета планеты
//    также при поиске


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