using CharEngine;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneBuilding : Scene
    {
        private SceneWorld _rootScene;
        private World _world;
        private Player _player;

        public SceneBuilding(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;
            _player = world.Player;
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    if (_player.SelectedBlockToBuild == null)
                    {
                        return;
                    }

                    Block block = ResourceManager.Instance.Blocks[_player.SelectedBlockToBuild];
                    if (!_world.Planets[_player.Planet].Ground.ContainsKey(_player.Coord))
                    {
                        _rootScene.MessageMessage.Show("No building here", ConsoleColor.DarkRed);
                        break;
                    }

                    foreach (var resource in block.Cost)
                    {
                        if (_player.Resources.GetValueOrDefault(resource.Key, 0) < resource.Value)
                        {
                            _rootScene.MessageMessage.Show("not enough " + resource.Key, ConsoleColor.DarkRed);
                            break;
                        }
                    }

                    if (_player.BuildBlock(_player.Coord, block, _world.Planets[_player.Planet]))
                    {
                        _rootScene.SceneTileMap.Map.SetCell(1, _player.Coord, ResourceManager.Instance.TileIds[_player.SelectedBlockToBuild]);
                    }
                    break;
                case ConsoleKey.E:
                    _player.DestroyBlock(_player.Coord, _world.Planets[_player.Planet]);
                    _rootScene.SceneTileMap.Map.RemoveCell(SceneTileMap.LayerBlock, _player.Coord);
                    break;
            }
        }
    }
}
