using Contorio.CharEngine;
using Contorio.Core;
using Contorio.Core.Managers;
using Contorio.Scenes;

namespace Contorio
{
    public class Contorio
    {
        Renderer _renderer;
        Engine _engine;

        public Contorio()
        {
            ModManager.Instance.AddMode(new BaseMod());
            ModManager.Instance.InitializeResources();

            _renderer = new Renderer(120, 30);
            _engine = new Engine(_renderer);
        }

        public void Run()
        {
            SceneMenu SceneMenu = new SceneMenu(_engine);

            while (true)
            {
                _engine.SetScene(SceneMenu);
                _engine.Run();

                switch (SceneMenu.Choice)
                {
                    case null:  // Quit
                        return;
                    case "new": // NewGame
                        World world = new World();
                        SaveManager.SaveWorld($"{world.Planets[0].Name}.ctsave", world);
                        _engine.SetScene(new Scenes.SceneWorld.SceneWorld(world));
                        _engine.Run();
                        break;
                    default:    // LoadGame
                        _engine.SetScene(new Scenes.SceneWorld.SceneWorld(SaveManager.LoadWorld(SceneMenu.Choice)));
                        _engine.Run();
                        break;
                }
            }
        }
    }
}
