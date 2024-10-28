// Contorio
// На будущие
// 1. Добавить в Mod PlanetPreset
//    При генерациий мира сделать выбор присета планеты
//    также при поиске

namespace Contorio
{
    internal class Program
    {
        static void Main(string[] args)
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