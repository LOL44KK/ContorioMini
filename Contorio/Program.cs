// Contorio
// На будущие
// 1. Добавить в Mod PlanetPreset
//    При генерациий мира сделать выбор присета планеты
//    также при поиске
// 2. Добавить типы генерации планеты квадрат, круг ...
// 3. Добавить структуры(Озера, речки) сделать руды структурой 
// 4. В WorldScene 
//    Заменить Dictionary<string, double> oreChance
//    На List<(string Name, double Chance)> oreChance
// 5. Добавить ResourceManager PlanetPresets
// 6. Сделать для BlockState фабрику
// 7. В Menu добавить надпись с версией игры

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