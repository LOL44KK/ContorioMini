using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;
using System.Drawing;

namespace Contorio.Scenes.SceneWorld.SceneTokensMenu
{
    public class SceneTokensMenu : Scene
    {
        private SceneWorld _rootScene;

        private World _world;

        public Label LabelTokensInfo;
        public Label LabelTab;
        public Label LabelTabResearch;
        public Label LabelTabSearchPlanet;

        public SceneResearch SceneResearch;
        public SceneSearchPlanet SceneSearchPlanet;

        public SceneTokensMenu(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;

            // InitializeComponent
            SceneResearch = new SceneResearch(_rootScene, _world);
            SceneSearchPlanet = new SceneSearchPlanet(_rootScene, _world);

            // InitializeWidgets
            LabelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1));
            LabelTab = new Label("Use number: ", ConsoleColor.White, new Point(60, 1));
            LabelTabResearch = new Label("1", ConsoleColor.Blue, new Point(72, 1));
            LabelTabSearchPlanet = new Label("2", ConsoleColor.White, new Point(74, 1));

            // IncludeScene
            IncludeScene(SceneResearch);
            IncludeScene(SceneSearchPlanet);

            // AddSprite
            AddSprite(LabelTokensInfo);
            AddSprite(LabelTab);
            AddSprite(LabelTabResearch);
            AddSprite(LabelTabSearchPlanet);
        }

        public override void Ready()
        {
            if (Enable)
            {
                SceneResearch.Enable = true;
                SceneSearchPlanet.Enable = false;
            }
        }

        public override void Tick()
        {
            UpdateTokensInfo();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    LabelTabResearch.TextColor = ConsoleColor.Blue;
                    LabelTabSearchPlanet.TextColor = ConsoleColor.White;
                    
                    SceneResearch.Enable = true;
                    SceneSearchPlanet.Enable = false;
                    break;
                case ConsoleKey.D2:
                    LabelTabResearch.TextColor = ConsoleColor.White;
                    LabelTabSearchPlanet.TextColor = ConsoleColor.Blue;
                    
                    SceneSearchPlanet.Enable = true;
                    SceneResearch.Enable = false;
                    break;
            }
        }

        private void UpdateTokensInfo()
        {
            LabelTokensInfo.Text = "TOKENS\n";
            foreach (var token in _world.Tokens)
            {
                LabelTokensInfo.Text += "  " + token.Key + ": " + token.Value + "\n";
            }
        }
    }
}
