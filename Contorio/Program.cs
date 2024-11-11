// Contorio
// На будущие
// 1. Переделать ResearchSystem
//    _openResearch и _closeResearch вместо Research будет хранится названия иследования
//    Dictionary<string(названия иследования), bool(изучено ли)>
// 2. Начать делать ресурс паки
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