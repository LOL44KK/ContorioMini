namespace CharEngine
{
    public class TileSet
    {
        // Надо заменить Sprite на матрицу Pixel[,]
        List<Sprite> _tiles;
        private int _tileWidth;
        private int _tileHeight;

        public List<Sprite> Tiles => _tiles;
        public int TileWidth => _tileWidth;
        public int TileHeight => _tileHeight;

        public TileSet(List<Sprite> tiles, int tileWidth, int tileHeight)
        {
            _tiles = tiles;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public TileSet(int tileWidth, int tileHeight)
        {
            _tiles = new List<Sprite>();
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public void AddTile(Sprite tile)
        {
            if (tile.Width == _tileWidth && tile.Height == _tileHeight)
            {
                _tiles.Add(tile);
            }
            else
            {
                throw new(
                    $"Invalid tile size: expected {_tileHeight}x{_tileWidth}, but got {tile.Height}x{tile.Width}."
                );
            }
        }
    }
}
