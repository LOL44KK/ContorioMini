using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneTileMap : Scene
    {
        ResourceManager _resourceManager;

        private World _world;
        private Player _player;

        public TileMap Map;
        public Label LabelPlayerCoord;
        public Sprite SpriteBlockPlayerCoord;

        public const int LayerGround = 0;
        public const int LayerBlock = 1;

        public SceneTileMap(World world)
        {
            _resourceManager = ResourceManager.Instance;
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            Map = new TileMap(
                66,
                30,
                _resourceManager.TileSet,
                new Point(27, 1),
                cellPaddingRight: 2,
                cellPaddingBottom: 1
            );
            Map.AddLayer(LayerGround);
            Map.AddLayer(LayerBlock);
            LabelPlayerCoord = new Label("X|Y", ConsoleColor.White, new Point(95, 0));
            SpriteBlockPlayerCoord = new Sprite(_resourceManager.TileSet.Tiles[0].Pixels, position: new Point(95, 1));

            // AddSprite
            AddSprite(Map);
            AddSprite(LabelPlayerCoord);
            AddSprite(SpriteBlockPlayerCoord);
        }

        public override void Ready()
        {
            LoadMap(_world.Planets[_world.Player.Planet]);
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    _player.Move(0, -1);
                    break;
                case ConsoleKey.S:
                    _player.Move(0, 1);
                    break;
                case ConsoleKey.A:
                    _player.Move(-1, 0);
                    break;
                case ConsoleKey.D:
                    _player.Move(1, 0);
                    break;
            }
        }

        public override void Tick()
        {
            Map.RenderVisibleArea(_world.Player.Coord);
            UpdateLabelPlayerCoord();
            UpdateSpritePlayerCoordBlock();
        }

        public void LoadMap(Planet planet)
        {

            Map.Clear();
            foreach (var groundState in planet.Ground)
            {
                Map.SetCell(LayerGround, groundState.Key, _resourceManager.TileIds[groundState.Value.Name]);
            }
            foreach (var blockState in planet.Blocks)
            {
                Map.SetCell(LayerBlock, blockState.Key, _resourceManager.TileIds[blockState.Value.Name]);
            }
        }

        private void UpdateLabelPlayerCoord()
        {
            LabelPlayerCoord.Text = _player.Coord.X + "|" + _player.Coord.Y;
        }

        private void UpdateSpritePlayerCoordBlock()
        {
            if (_world.Planets[_player.Planet].Ground.ContainsKey(_player.Coord))
            {
                if (_world.Planets[_player.Planet].Blocks.ContainsKey(_player.Coord))
                {
                    SpriteBlockPlayerCoord.Pixels = _resourceManager.Blocks[_world.Planets[_player.Planet].Blocks[_player.Coord].Name].PixelCanvas.Pixels;
                }
                else
                {
                    SpriteBlockPlayerCoord.Pixels = _resourceManager.Grounds[_world.Planets[_player.Planet].Ground[_player.Coord].Name].PixelCanvas.Pixels;
                }
            }
            else
            {
                SpriteBlockPlayerCoord.Pixels = new Pixel[0, 0];
            }
        }
    }
}
