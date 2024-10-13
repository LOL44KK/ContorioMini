namespace Contorio
{

    internal class Program
    {
        Dictionary<int, Dictionary<string, int>> map = new Dictionary<int, Dictionary<string, int>>();

        static void Main(string[] args)
        {
            new Contorio().Run();
        }
    }
}