using System.Diagnostics;

namespace Contorio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Contorio().Run();
            Stopwatch sw = Stopwatch.StartNew();
        }
    }
}