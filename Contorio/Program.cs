// Contorio
// На будущие
// 1. При поиске планеты добавить выбор пресета планеты
// 2. Придумать как ограничить FPS в Engine 
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