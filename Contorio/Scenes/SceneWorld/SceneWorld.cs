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

        public SceneTileMap SceneTileMap;
        public ScenePlanetInfo ScenePlanetInfo;
        public SceneBuilding SceneBuilding;
        public SceneResearch SceneResearch;

        public Message MessageMessage;

        public SceneWorld(World world)
        {
            _world = world;
            _player = _world.Player;
            _worldHandler = new WorldHandler(_world);
            
            // InitializeComponent
            MessageMessage = new Message(60, 29);

            SceneTileMap = new SceneTileMap(_world);
            ScenePlanetInfo = new ScenePlanetInfo(_world);
            SceneBuilding = new SceneBuilding(this, _world);
            SceneResearch = new SceneResearch(this, world);

            // AddSprite
            AddSprite(MessageMessage);

            // Include Container
            IncludeScene(SceneTileMap);
            IncludeScene(ScenePlanetInfo);
            IncludeScene(SceneBuilding);
            IncludeScene(SceneResearch);

            // OnTick
            OnTick += _worldHandler.Tick;
        }

        public override void Ready()
        {
            ScenePlanetInfo.Enable = true;
            SceneTileMap.Enable = true;

            SceneBuilding.Enable = false;
            SceneResearch.Enable = false;
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.B:
                    if (SceneTileMap.Enable)
                    {
                        SceneBuilding.Enable = !SceneBuilding.Enable;
                    }
                    break;
                case ConsoleKey.R:
                    SceneResearch.Enable = !SceneResearch.Enable;
                    
                    SceneTileMap.Enable  = !SceneResearch.Enable;
                    SceneBuilding.Enable = false;
                    break;
            }
        }
    }
}
