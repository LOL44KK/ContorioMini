using System.Drawing;

namespace Contorio.Utils
{
    public class ClusterGenerator
    {
        public static Point[] GenerateCluster(int minClusterSize, int maxClusterSize)
        {
            Random Random = new Random();

            Point start = new Point(0, 0);

            HashSet<Point> cluster = new HashSet<Point> { start };

            List<Point> growthPoints = new List<Point> { start };

            int targetClusterSize = Random.Next(minClusterSize, maxClusterSize + 1);

            while (cluster.Count < targetClusterSize && growthPoints.Count > 0)
            {
                Point currentPoint = growthPoints[Random.Next(growthPoints.Count)];
                growthPoints.Remove(currentPoint);

                List<Point> neighbors = GetNeighbors(currentPoint);

                foreach (var neighbor in neighbors)
                {
                    if (!cluster.Contains(neighbor) && cluster.Count < targetClusterSize)
                    {
                        cluster.Add(neighbor);
                        growthPoints.Add(neighbor);
                    }
                }
            }
            return new List<Point>(cluster).ToArray();
        }

        private static List<Point> GetNeighbors(Point point)
        {
            return new List<Point>
            {
                new Point(point.X + 1, point.Y),
                new Point(point.X - 1, point.Y),
                new Point(point.X, point.Y + 1),
                new Point(point.X, point.Y - 1)
            };
        }
    }
}
