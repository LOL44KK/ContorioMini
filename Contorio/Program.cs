// Contorio
// На будущие
// 1. Доделать WorldScene
// 2. В Menu добавить надпись с версией игры
// 3. Подумать на структурой движка
// 4. Подумать над менеджарами в Core
// 5. При генерациий мира сделать выбор присета планеты
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