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
        public SceneTransfer SceneTransfer;
        public SceneBlockUI SceneBlockUI;

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
            SceneResearch = new SceneResearch(this, _world);
            SceneTransfer = new SceneTransfer(_world);
            SceneBlockUI = new SceneBlockUI(_world);

            // AddSprite
            AddSprite(MessageMessage);

            // Include Container
            IncludeScene(SceneTileMap);
            IncludeScene(ScenePlanetInfo);
            IncludeScene(SceneBuilding);
            IncludeScene(SceneResearch);
            IncludeScene(SceneTransfer);
            IncludeScene(SceneBlockUI);

            // OnTick
            OnTick += _worldHandler.Tick;
        }

        public override void Ready()
        {
            ScenePlanetInfo.Enable = true;
            SceneTileMap.Enable = true;

            SceneBuilding.Enable = false;
            SceneResearch.Enable = false;
            SceneTransfer.Enable = false;
            SceneBlockUI.Enable = false;
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    ScenePlanetInfo.Enable = true;
                    SceneTileMap.Enable = true;

                    SceneBuilding.Enable = false;
                    SceneResearch.Enable = false;
                    SceneTransfer.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.R:
                    SceneResearch.Enable = !SceneResearch.Enable;
                    
                    SceneTileMap.Enable  = !SceneResearch.Enable;
                    SceneBuilding.Enable = false;
                    SceneTransfer.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.T:
                    SceneTransfer.Enable = !SceneTransfer.Enable;

                    SceneTileMap.Enable = !SceneTransfer.Enable;
                    SceneBuilding.Enable = false;
                    SceneResearch.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.B:
                    if (SceneTileMap.Enable)
                    {
                        SceneBuilding.Enable = !SceneBuilding.Enable;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (SceneBlockUI.Enable)
                    {
                        SceneBlockUI.Enable = false;

                        SceneTileMap.Enable = true;
                    }
                    else if (SceneTileMap.Enable && !SceneBuilding.Enable && SceneBlockUI.OpenUI(_player.Coord))
                    {
                        SceneTileMap.Enable  = false;
                        SceneTransfer.Enable = false;
                        SceneBuilding.Enable = false;
                        SceneResearch.Enable = false;
                    }
                    break;
            }
        }
    }
}
