using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneWorld : Scene
    {
        private World _world;
        private Player _player;
        private WorldHandler _worldHandler;

        public ContainerTileMap ContainerTileMap;
        public ContainerPlanetInfo ContainerPlanetInfo;
        public ContainerBuilding ContainerBuilding;

        public Message MessageMessage;

        public SceneWorld(World world)
        {
            _world = world;
            _player = _world.Player;
            _worldHandler = new WorldHandler(_world);

            // InitializeComponent
            MessageMessage = new Message(60, 29);

            ContainerTileMap = new ContainerTileMap(_world);
            ContainerPlanetInfo = new ContainerPlanetInfo(_world);
            ContainerBuilding = new ContainerBuilding(this, _world);

            // AddSprite
            AddSprite(MessageMessage);

            // Include Container
            IncludeСontainer(ContainerTileMap);
            IncludeСontainer(ContainerPlanetInfo);
            IncludeСontainer(ContainerBuilding);

            // OnTick
            OnTick += _worldHandler.Tick;

            // OnInput
            OnInput += Input;

            // 
            ContainerTileMap.Enable = true;
            ContainerPlanetInfo.Enable = true;
            ContainerBuilding.Enable = true;
        }

        private void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.B:
                    ContainerBuilding.Enable = !ContainerBuilding.Enable;
                    break;
            }
        }
    }
}
