using Contorio.CharGraphics;
using Contorio.Scenes;
using Contorio.Core;
using Contorio.Core.Managers;

namespace Contorio
{
    public class Contorio
    {
        private Renderer renderer;
        public Contorio()
        {
            renderer = new Renderer(120, 30);
        }

        public void Run()
        {
            ContorioMenu contorioMenu = new ContorioMenu(renderer);
            ContorioWorld contorioWorld = new ContorioWorld(renderer);

            while (true)
            {
                contorioMenu.Run();

                switch (contorioMenu.Choice)
                {
                    case null:  //quit
                        return;
                    case "new": //new game
                        World world = new World();
                        SaveManager.SaveWorld($"{world.Planets[0].Name}.ctsave", world);
                        contorioWorld.Run(world);
                        break;
                    default:    //load game
                        contorioWorld.Run(SaveManager.LoadWorld(contorioMenu.Choice));
                        break;
                }
            }
        }
    }
}
