using Contorio.CharEngine;
using Contorio.Core;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneWorld : Scene
    {
        private World _world;
        private Player _player;
        private WorldHandler _worldHandler;

        private ContainerTileMap _containerTileMap;

        public SceneWorld(World world)
        {
            _world = world;
            _player = _world.Player;
            _worldHandler = new WorldHandler(_world);

            _containerTileMap = new ContainerTileMap(_world);

            // Include Container
            IncludeСontainer(_containerTileMap);

            // OnTick
            OnTick += _worldHandler.Tick;

            // OnInput

            // 
            _containerTileMap.Enable = true;
        }
    }
}
