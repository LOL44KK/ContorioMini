using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core.Managers;
using Contorio.Core.Types;

namespace Contorio.Scenes.SceneMenu
{
    public class SceneNewGame : Scene
    {
        ResourceManager _resourceManager;

        ItemList ItemListPlanetPresets;
        Label LabelInfoPlanetPreset;

        public SceneNewGame()
        {
            _resourceManager = ResourceManager.Instance;

            // InitializeWidgets
            ItemListPlanetPresets = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(45, 10),
                15
            );
            LabelInfoPlanetPreset = new Label("INFO", ConsoleColor.White, new Point(70, 10));

            // AddSprite
            AddSprite(ItemListPlanetPresets);
            AddSprite(LabelInfoPlanetPreset);
        }

        public override void Ready()
        {
            UpdateItemListPlanetPreset();
            UpdateInfoPlanetPreset();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListPlanetPresets.NextItem();
                    UpdateInfoPlanetPreset();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListPlanetPresets.PreviousItem();
                    UpdateInfoPlanetPreset();
                    break;
                case ConsoleKey.Enter:
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

        public void UpdateInfoPlanetPreset()
        {
            PlanetPreset planetPreset = _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex];

            LabelInfoPlanetPreset.Text  = $"Name: {planetPreset.Name}\n";
            LabelInfoPlanetPreset.Text += $"Starting Planet\n";

            LabelInfoPlanetPreset.Text += $"Size: {planetPreset.Size}\n";
            LabelInfoPlanetPreset.Text += $"Soil Type: {planetPreset.Dirt}\n";
            LabelInfoPlanetPreset.Text += $"Planet Type: {planetPreset.Type}\n";
            LabelInfoPlanetPreset.Text += "Ores: \n";
            foreach (var ore in planetPreset.Ores)
            {
                LabelInfoPlanetPreset.Text += $"{ore.Name} (Chance: {ore.Chance}%, Cluster: {ore.MinClusterSize}-{ore.MaxClusterSize});\n";
            }

        }
    }
}
