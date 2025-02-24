﻿using System.Drawing;

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
        private List<Dictionary<Point, Cell>> _cells;

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

        public TileMap(
            int width,
            int height,
            TileSet tileSet,
            int countLayer,
            Point position,
            int cellPaddingRight = 0,
            int cellPaddingBottom = 0
        ) 
            : base(new Pixel[height, width], position)
        {
            _tileSet = tileSet;
            _cellPaddingRight = cellPaddingRight;
            _cellPaddingBottom = cellPaddingBottom;
            _cells = new List<Dictionary<Point, Cell>>();
            for (int i = 0; i < countLayer; i++)
            {
                AddLayer();
            }
        }

        public void AddLayer()
        {
            _cells.Add(new Dictionary<Point, Cell>());
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
            foreach (var cell in _cells) 
            {
                cell.Clear();
            }
        }

        public void RenderVisibleArea(Point coord)
        {
            FillPixels(Pixel.Empty);

            int startY = (int)Math.Ceiling(coord.Y - (Height / 2.0) / (TileSet.TileHeight + _cellPaddingBottom));
            int finishY = (int)Math.Ceiling(coord.Y + (Height / 2.0) / (TileSet.TileHeight + _cellPaddingBottom));
            int startX = (int)Math.Ceiling(coord.X - (Width / 2.0) / (TileSet.TileWidth + _cellPaddingRight));
            int finishX = (int)Math.Ceiling(coord.X + (Width / 2.0) / (TileSet.TileWidth + _cellPaddingRight));
            
            for (int layer = 0; layer < _cells.Count; layer++)
            {
                RenderLayer(layer, startY, finishY, startX, finishX);
            }
        }

        private void RenderLayer(int layer, int startY, int finishY, int startX, int finishX)
        {
            int rowOffset = 0;
            for (int y = startY; y < finishY; y++)
            {
                int colOffset = 0;
                for (int x = startX; x < finishX; x++)
                {
                    if (_cells[layer].TryGetValue(new Point(x, y), out Cell cell))
                    {
                        RenderTile(cell, rowOffset, colOffset);
                    }
                    colOffset += TileSet.TileWidth + _cellPaddingRight;
                }
                rowOffset += TileSet.TileHeight + _cellPaddingBottom;
            }
        }

        private void RenderTile(Cell cell, int rowOffset, int colOffset)
        {
            var tile = _tileSet.Tiles[cell.TileId];
            for (int tileY = 0; tileY < TileSet.TileHeight; tileY++)
            {
                for (int tileX = 0; tileX < TileSet.TileWidth; tileX++)
                {
                    if (colOffset + tileX < Width && rowOffset + tileY < Height)
                    {
                        _pixels[rowOffset + tileY, colOffset + tileX] = tile[tileY, tileX];
                    }
                }
            }
        }
    }
}