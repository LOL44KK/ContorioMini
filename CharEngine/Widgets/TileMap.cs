using System.Drawing;

namespace CharEngine.Widgets
{
    public struct Cell 
    {
        public int TileId;

        public Cell(int tileId)
        { 
            TileId = tileId;
        }
    }

    public class TileMap : Sprite
    {
        private TileSet _tileSet;
        private Dictionary<int, Dictionary<Point, Cell>> _cells;

        private int _cellPaddingRight;
        private int _cellPaddingBottom;

        public TileSet TileSet 
        { 
            get { return _tileSet; }
            set { _tileSet = value; }
        }

        public int CellPaddingRight
        {
            get { return _cellPaddingRight; }
            set { _cellPaddingRight = value; }
        }

        public int CellPaddingBottom
        {
            get { return _cellPaddingBottom; }
            set { _cellPaddingBottom = value; }
        }

        public TileMap(int width, int height, TileSet tileSet, Point position, int cellPaddingRight = 0, int cellPaddingBottom = 0) 
            : base(new Pixel[height, width], position:position)
        {
            _tileSet = tileSet;
            _cells = new Dictionary<int, Dictionary<Point, Cell>>();
            _cellPaddingRight = cellPaddingRight;
            _cellPaddingBottom = cellPaddingBottom;
        }

        public void AddLayer(int index)
        {
            _cells[index] = new Dictionary<Point, Cell>();
        }

        public void SetCell(int layer, Point coord, int tileId)
        {
            _cells[layer][coord] = new Cell(tileId);
        }

        public void RemoveCell(int layer, Point coord)
        {
            _cells[layer].Remove(coord);
        }

        public Cell? GetCell(int layer, Point coord)
        {
            Cell cell;
            if (_cells[layer].TryGetValue(coord, out cell)) {
                return cell;
            }
            return null;
        }

        public void Clear()
        {
            foreach (var cell in _cells.Values) 
            {
                cell.Clear();
            }
        }

        public void UpdatePixels(Point coord)
        {
            FillPixels(Pixel.Empty);

            int i = 0;
            int j = 0;
            
            foreach (int layer in _cells.Keys)
            {
                int numberOfTilesY = Height / TileSet.TileHeight;
                int numberOfTilesX = Width / TileSet.TileWidth;

                int startY = (int)Math.Ceiling(coord.Y - (Height / 2.0) / (TileSet.TileHeight + _cellPaddingBottom));
                int finishY = (int)Math.Ceiling(coord.Y + (Height / 2.0) / (TileSet.TileHeight + _cellPaddingBottom));
                int startX = (int)Math.Ceiling(coord.X - (Width / 2.0) / (TileSet.TileWidth + _cellPaddingRight));
                int finishX = (int)Math.Ceiling(coord.X + (Width / 2.0) / (TileSet.TileWidth + _cellPaddingRight));

                i = 0;
                for (int y = startY; y < finishY; y++)
                {
                    j = 0;
                    for (int x = startX; x < finishX; x++)
                    {
                        if (_cells[layer].TryGetValue(new Point(x, y), out Cell cell))
                        {
                            for (int tileY = 0; tileY < TileSet.TileHeight; tileY++)
                            {
                                for (int tileX = 0; tileX < TileSet.TileWidth; tileX++)
                                {
                                    if (j + tileX < Width && i + tileY < Height)
                                    {
                                        _pixels[i + tileY, j + tileX] = _tileSet.Tiles[cell.TileId].Pixels[tileY, tileX];
                                    }
                                }
                            }
                        }
                        j += TileSet.TileWidth + _cellPaddingRight;
                    }
                    i += TileSet.TileHeight + _cellPaddingBottom;
                }
            }
        }
    }
}