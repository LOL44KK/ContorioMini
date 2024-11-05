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
        public ContainerResearch ContainerResearch;

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
            ContainerResearch = new ContainerResearch(this, world);

            // AddSprite
            AddSprite(MessageMessage);

            // Include Container
            IncludeСontainer(ContainerTileMap);
            IncludeСontainer(ContainerPlanetInfo);
            IncludeСontainer(ContainerBuilding);
            IncludeСontainer(ContainerResearch);

            // OnTick
            OnTick += _worldHandler.Tick;

            // OnInput
            OnInput += Input;

            // 
            ContainerTileMap.Enable = true;
            ContainerPlanetInfo.Enable = true;
        }

        private void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.B:
                    if (ContainerTileMap.Enable)
                    {
                        ContainerBuilding.Enable = !ContainerBuilding.Enable;
                    }
                    break;
                case ConsoleKey.R:
                    ContainerResearch.Enable = !ContainerResearch.Enable;
                    
                    ContainerTileMap.Enable  = !ContainerResearch.Enable;
                    ContainerBuilding.Enable = false;
                    break;
            }
        }
    }
}
