using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneDebugUI : Scene
    {
        private Engine _engine;

        private World _world;

        public Label LabelFPS;
        public Label LabelBlockInfo;

        public SceneDebugUI(Engine engine, World world) 
        {
            _engine = engine;

            _world = world;

            // InitializeWidgets
            LabelFPS = new Label(
                "FPS: ",
                ConsoleColor.White,
                new Point(120, 0),
                alignment: Alignment.Right,
                textAlignment: TextAlignment.Right
            );

            LabelBlockInfo = new Label(
                "BLOCK INFO",
                ConsoleColor.White,
                new Point(120, 1),
                alignment: Alignment.Right,
                textAlignment: TextAlignment.Right
            );

            // AddSprite
            AddSprite(LabelFPS);
            AddSprite(LabelBlockInfo);
        }

        public override void Tick()
        {
            LabelFPS.Text = "FPS: " + _engine.FPS;
        }
    }
}
