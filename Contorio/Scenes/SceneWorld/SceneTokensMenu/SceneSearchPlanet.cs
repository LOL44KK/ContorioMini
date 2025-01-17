using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using CharEngine.Containers;
using Contorio.Core;
using Contorio.Core.Managers;
using Contorio.Core.Presets;

namespace Contorio.Scenes.SceneWorld.SceneTokensMenu
{
    public class SceneSearchPlanet : Scene
    {
        private SceneWorld _rootScene;

        private ResourceManager _resourceManager;

        private World _world;
        private ResearchSystem _researchSystem;

        public Label LabelSearchPlanet;
        public Label LabelPlanetPreset;
        public ItemList ItemListPlanetPresets;
        public Label LabelPlanetSize;
        public ItemList ItemListPlanetSize;
        public Label LabelOreName;
        public ItemList ItemListOreName;
        public Label LabelOreChance;
        public ItemList ItemListOreChance;
        public Label LabelPalkaPeredCostSearchPlanet;
        public Label LabelCostSearchPlanet;
        public Label LabelPressGtoSearchPlanet;

        public ItemListContainer ItemListContainer;

        public Dictionary<string, double> _oreChance;

        public SceneSearchPlanet(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _resourceManager = ResourceManager.Instance;
            _world = world;
            _researchSystem = world.ResearchSystem;
            _oreChance = new Dictionary<string, double>();

            // InitializeWidgets
            LabelSearchPlanet = new Label("SEARCH PLANET", ConsoleColor.White, new Point(60, 3));
            LabelPlanetPreset = new Label("Preset: ", ConsoleColor.White, new Point(60, 5));
            ItemListPlanetPresets = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(68, 5),
                1
            );

            LabelPlanetSize = new Label("Size: ", ConsoleColor.White, new Point(60, 6));
            ItemListPlanetSize = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(66, 6),
                1
            );
            for (int i = 16; i < 130; i += 2) ItemListPlanetSize.AddItem("" + i);

            LabelOreName = new Label("Ore name: ", ConsoleColor.White, new Point(60, 7));
            ItemListOreName = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(70, 7),
                1
            );

            LabelOreChance = new Label("Chance: ", ConsoleColor.White, new Point(60, 8));
            ItemListOreChance = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(68, 8),
                1
            );
            for (double i = 0; i < 1000; i += 5) ItemListOreChance.AddItem("" + i / 10000);

            LabelPalkaPeredCostSearchPlanet = new Label("=================", ConsoleColor.White, new Point(60, 9));
            LabelCostSearchPlanet = new Label("Cost: 0 PL", ConsoleColor.White, new Point(60, 10));
            LabelPressGtoSearchPlanet = new Label("Press G to search", ConsoleColor.White, new Point(60, 11));

            // ItemListContainer
            ItemListContainer = new ItemListContainer(inactiveListSelectedItemColor: ConsoleColor.White);
            ItemListContainer.AddItemList(ItemListPlanetPresets);
            ItemListContainer.AddItemList(ItemListPlanetSize);
            ItemListContainer.AddItemList(ItemListOreName);
            ItemListContainer.AddItemList(ItemListOreChance);

            AddSprite(LabelSearchPlanet);
            AddSprite(LabelPlanetPreset);
            AddSprite(ItemListPlanetPresets);
            AddSprite(LabelPlanetSize);
            AddSprite(ItemListPlanetSize);
            AddSprite(LabelOreName);
            AddSprite(ItemListOreName);
            AddSprite(LabelOreChance);
            AddSprite(ItemListOreChance);
            AddSprite(LabelPalkaPeredCostSearchPlanet);
            AddSprite(LabelCostSearchPlanet);
            AddSprite(LabelPressGtoSearchPlanet);
        }

        public override void Ready()
        {
            UpdatePlanetPresets();
            UpdateOreNameList(_resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex]);
            UpdateCostSearchPlanet();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListContainer.NextItemList();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListContainer.PreviousItemList();
                    break;
                case ConsoleKey.LeftArrow:
                    ItemListContainer.PreviousItem();
                    if (ItemListContainer.SelectedItemList == 0)
                    {
                        UpdateOreNameList(_resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex]);
                        ItemListOreChance.SelectedIndex = 0;
                    }
                    if (ItemListContainer.SelectedItemList == 2)
                    {
                        ItemListOreChance.SelectedItem = "" + _oreChance[ItemListOreName.SelectedItem];
                    }
                    if (ItemListContainer.SelectedItemList == 3)
                    {
                        _oreChance[ItemListOreName.SelectedItem] = double.Parse(ItemListOreChance.SelectedItem);
                    }
                    UpdateCostSearchPlanet();
                    break;
                case ConsoleKey.RightArrow:
                    ItemListContainer.NextItem();
                    if (ItemListContainer.SelectedItemList == 0)
                    {
                        UpdateOreNameList(_resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex]);
                        ItemListOreChance.SelectedIndex = 0;
                    }
                    if (ItemListContainer.SelectedItemList == 2)
                    {
                        ItemListOreChance.SelectedItem = "" + _oreChance[ItemListOreName.SelectedItem];
                    }
                    if (ItemListContainer.SelectedItemList == 3)
                    {
                        _oreChance[ItemListOreName.SelectedItem] = double.Parse(ItemListOreChance.SelectedItem);
                    }
                    UpdateCostSearchPlanet();
                    break;
                case ConsoleKey.G:
                    if (_world.Tokens.GetValueOrDefault("PL", 0) >= CalculateCostSearchPlanet())
                    {
                        _world.SearchPlanet(GetPlanetPreset());
                    }
                    else
                    {
                        _rootScene.MessageMessage.Show("not enough PL", ConsoleColor.DarkRed);
                    }
                    break;
            }
        }
        private PlanetPreset GetPlanetPreset()
        {
            return
                new PlanetPreset(
                    _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex].Name,
                    int.Parse(ItemListPlanetSize.SelectedItem ?? "32"),
                    _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex].Dirt,
                    _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex].Type,
                    _resourceManager.PlanetPresets[ItemListPlanetPresets.SelectedIndex].Ores
                    .Select(ore => new OrePreset(ore.Name, _oreChance[ore.Name], ore.MinClusterSize, ore.MaxClusterSize))
                    .ToList()
                );
        }


        private int CalculateCostSearchPlanet()
        {
            return World.CalculateCostSearchPlanet(GetPlanetPreset());
        }

        private void UpdatePlanetPresets()
        {
            ItemListPlanetPresets.ClearItems();
            foreach (var preset in _resourceManager.PlanetPresets)
            {
                ItemListPlanetPresets.AddItem(preset.Name);
            }
        }

        private void UpdateOreNameList(PlanetPreset planetPreset)
        {
            ItemListOreName.ClearItems();
            _oreChance.Clear();
            foreach (var ore in planetPreset.Ores)
            {
                ItemListOreName.AddItem(ore.Name);
                _oreChance.Add(ore.Name, 0.0f);
            }
        }

        private void UpdateCostSearchPlanet()
        {
            LabelCostSearchPlanet.Text = "cost: " + CalculateCostSearchPlanet() + " PL";
        }
    }
}
