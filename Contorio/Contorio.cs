using Contorio.CharEngine;
using Contorio.Core;
using Contorio.Core.Managers;
using Contorio.Scenes.SceneMenu;
using Contorio.Scenes.SceneWorld;

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

            World world;
            while (true)
            {
                _engine.SetScene(SceneMenu);
                _engine.Run();

                switch (SceneMenu.Choice)
                {
                    case null:  // Quit
                        return;
                    case "new": // NewGame
                        world = new World();
                        world.Planets.Add(new Planet(ResourceManager.Instance.PlanetPresets[SceneMenu.SceneNewGame.ItemListPlanetPresets.SelectedIndex]));
                        world.Player.Move(world.Planets[0].Size / 2, world.Planets[0].Size / 2);
                        
                        SaveManager.SaveWorld($"{world.Planets[0].Name}.ctsave", world);
                        _engine.SetScene(new SceneWorld(_engine, world));
                        _engine.Run();
                        break;
                    case "load": // LoadGame
                        world = SaveManager.LoadWorld(SceneMenu.SceneLoadGame.ItemListSavesList.SelectedItem ?? throw new("ERORR"));
                        _engine.SetScene(new SceneWorld(_engine, world));
                        _engine.Run();
                        break;
                }
            }
        }
    }
}
