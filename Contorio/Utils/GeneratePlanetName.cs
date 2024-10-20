namespace Contorio.Utils
{
    public class GeneratePlanetName
    {
        private static List<string> prefixes = new List<string>
            { "Neo", "Exo", "Zeta", "Omega", "Alpha", "Nova", "Astro", "Cosmo" };

        private static List<string> roots = new List<string>
            { "terra", "tron", "prime", "mund", "stella", "orbis", "sphere", "globe" };

        private static List<string> suffixes = new List<string>
            { "X", "Prime", "Major", "Minor", "Alpha", "Beta", "Gamma", "Delta" };

        public static string GenerateName()
        {
            Random random = new Random();
            return prefixes[random.Next(prefixes.Count)] + roots[random.Next(roots.Count)] + "-" + suffixes[random.Next(suffixes.Count)];
        }
    }
}
