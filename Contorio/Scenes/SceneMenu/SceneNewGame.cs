using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core.Managers;
using Contorio.Core.Types;

namespace Contorio.Scenes.SceneMenu
{
    public class SceneNewGame : Scene
    {
        Engine _engine;
        SceneMenu _rootScene;

        ResourceManager _resourceManager;

        public Label LabelPlanetPreset;
        public ItemList ItemListPlanetPresets;
        public Label LabelInfoWorldPreset;

        public SceneNewGame(Engine engine, SceneMenu rootScene)
        {
            _engine = engine;
            _rootScene = rootScene;

            _resourceManager = ResourceManager.Instance;

            // InitializeWidgets
            LabelPlanetPreset = new Label("WORLD  PRESET", ConsoleColor.White, new Point(60, 9), alignment: Alignment.Center);
            ItemListPlanetPresets = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(60, 11),
                15,
                alignment: Alignment.Center,
                textAlignment: TextAlignment.Center
            );
            LabelInfoWorldPreset = new Label("INFO", ConsoleColor.White, new Point(0, 15));

            // AddSprite
            AddSprite(LabelPlanetPreset);
            AddSprite(ItemListPlanetPresets);
            AddSprite(LabelInfoWorldPreset);
        }

        public override void Ready()
        {
            UpdateItemListPlanetPreset();
            UpdateInfoWorldPreset();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListPlanetPresets.NextItem();
                    UpdateInfoWorldPreset();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListPlanetPresets.PreviousItem();
                    UpdateInfoWorldPreset();
                    break;
                case ConsoleKey.Enter:
                    _rootScene.Choice = "new";
                    _engine.Quit();
                    break;
            }
        }

        public void UpdateItemListPlanetPreset()
        {
            ItemListPlanetPresets.ClearItems();
            foreach (var preset in _resourceManager.PlanetPresets)
            {
                ItemListPlanetPresets.AddItem(preset.Name);
            }
        }

        public void UpdateInfoWorldPreset()
        {
            PlanetPreset planetPreset = _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex];

            LabelInfoWorldPreset.Text  = $"INFO PRESET\n";
            LabelInfoWorldPreset.Text += $"Name: {planetPreset.Name}\n";
            LabelInfoWorldPreset.Text += $"Starting Planet\n";

            LabelInfoWorldPreset.Text += $"Size: {planetPreset.Size}\n";
            LabelInfoWorldPreset.Text += $"Soil Type: {planetPreset.Dirt}\n";
            LabelInfoWorldPreset.Text += $"Planet Type: {planetPreset.Type}\n";
            LabelInfoWorldPreset.Text += $"Ores\n";
            for (int i = 0; i < planetPreset.Ores.Count; i++)
            {
                OrePreset ore = planetPreset.Ores[i];
                LabelInfoWorldPreset.Text += $"{i+1}. {ore.Name} (Chance: {ore.Chance}%, Cluster: {ore.MinClusterSize}-{ore.MaxClusterSize});\n";
            }
            LabelInfoWorldPreset.Position = new Point(LabelInfoWorldPreset.Position.X, 31 - LabelInfoWorldPreset.Height);
        }
    }
}
