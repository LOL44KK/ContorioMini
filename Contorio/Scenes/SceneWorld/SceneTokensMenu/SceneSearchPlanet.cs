using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Managers;
using Contorio.Core.Types;
using System.Drawing;

namespace Contorio.Scenes.SceneWorld.SceneTokensMenu
{
    public class SceneSearchPlanet : Scene
    {
        private SceneWorld _rootScene;

        private ResourceManager _resourceManager;

        private World _world;
        private ResearchSystem _researchSystem;

        public Label LabelSearchPlanet;
        public Label LabelPlanetSize;
        public ItemList ItemListPlanetSize;
        public Label LabelOreName;
        public ItemList ItemListOreName;
        public Label LabelOreChance;
        public ItemList ItemListOreChance;
        public Label LabelPalkaPeredCostSearchPlanet;
        public Label LabelCostSearchPlanet;
        public Label LabelPressGtoSearchPlanet;

        public ItemListContainer ItemListContainerSearchPlanet;

        public Dictionary<string, double> _oreChance;

        public SceneSearchPlanet(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _resourceManager = ResourceManager.Instance;
            _world = world;
            _researchSystem = world.ResearchSystem;
            _oreChance = new Dictionary<string, double>();

            // InitializeWidgets
            LabelSearchPlanet = new Label("SEARCH PLANET", ConsoleColor.White, new Point(60, 3), visible: false);
            LabelPlanetSize = new Label("Size: ", ConsoleColor.White, new Point(60, 5), visible: false);
            ItemListPlanetSize = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(66, 5),
                1,
                visible: false
            );
            for (int i = 16; i < 130; i += 2) ItemListPlanetSize.AddItem("" + i);

            LabelOreName = new Label("Ore name: ", ConsoleColor.White, new Point(60, 6), visible: false);
            ItemListOreName = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(70, 6),
                1,
                visible: false
            );

            LabelOreChance = new Label("Chance: ", ConsoleColor.White, new Point(60, 7), visible: false);
            ItemListOreChance = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(68, 7),
                1,
                visible: false
            );
            for (double i = 0; i < 1000; i += 5) ItemListOreChance.AddItem("" + i / 10000);

            LabelPalkaPeredCostSearchPlanet = new Label("=================", ConsoleColor.White, new Point(60, 8), visible: false);
            LabelCostSearchPlanet = new Label("Cost: 0 PL", ConsoleColor.White, new Point(60, 9), visible: false);
            LabelPressGtoSearchPlanet = new Label("Press G to search", ConsoleColor.White, new Point(60, 10), visible: false);

            // ItemListContainer
            ItemListContainerSearchPlanet = new ItemListContainer(inactiveListSelectedItemColor: ConsoleColor.White);
            ItemListContainerSearchPlanet.AddItemList(ItemListPlanetSize);
            ItemListContainerSearchPlanet.AddItemList(ItemListOreName);
            ItemListContainerSearchPlanet.AddItemList(ItemListOreChance);

            AddSprite(LabelSearchPlanet);
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
            UpdateOreNameList(ResourceManager.Instance.PlanetPresets[0]);
            UpdateCostSearchPlanet();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListContainerSearchPlanet.NextItemList();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListContainerSearchPlanet.PreviousItemList();
                    break;
                case ConsoleKey.LeftArrow:
                    ItemListContainerSearchPlanet.PreviousItem();
                    if (ItemListContainerSearchPlanet.SelectedItemList == 1)
                    {
                        ItemListOreChance.SelectedItem = "" + _oreChance[ItemListOreName.SelectedItem];
                    }
                    if (ItemListContainerSearchPlanet.SelectedItemList == 2)
                    {
                        _oreChance[ItemListOreName.SelectedItem] = double.Parse(ItemListOreChance.SelectedItem);
                    }
                    UpdateCostSearchPlanet();
                    break;
                case ConsoleKey.RightArrow:
                    ItemListContainerSearchPlanet.NextItem();
                    if (ItemListContainerSearchPlanet.SelectedItemList == 1)
                    {
                        ItemListOreChance.SelectedItem = "" + _oreChance[ItemListOreName.SelectedItem];
                    }
                    if (ItemListContainerSearchPlanet.SelectedItemList == 2)
                    {
                        _oreChance[ItemListOreName.SelectedItem] = double.Parse(ItemListOreChance.SelectedItem);
                    }
                    UpdateCostSearchPlanet();
                    break;
                case ConsoleKey.G:
                    if (_world.Tokens.GetValueOrDefault("PL", 0) >= CalculateCostSearchPlanet())
                    {
                        _world.SearchPlanet(
                            new PlanetPreset(
                                // Из BaseMode
                                _resourceManager.PlanetPresets[0].Name,
                                int.Parse(ItemListPlanetSize.SelectedItem ?? "32"),
                                _resourceManager.PlanetPresets[0].Dirt,
                                _resourceManager.PlanetPresets[0].Type,
                                _resourceManager.PlanetPresets[0].Ores.Select(ore => new OrePreset(ore.Name, _oreChance[ore.Name], ore.MinClusterSize, ore.MaxClusterSize)).ToList()
                            )
                        );
                    }
                    else
                    {
                        _rootScene.MessageMessage.Show("not enough PL", ConsoleColor.DarkRed);
                    }
                    break;
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

        private int CalculateCostSearchPlanet()
        {
            return World.CalculateCostSearchPlanet(
                new PlanetPreset(
                    // Из BaseMode
                    _resourceManager.PlanetPresets[0].Name,
                    int.Parse(ItemListPlanetSize.SelectedItem ?? "32"),
                    _resourceManager.PlanetPresets[0].Dirt,
                    _resourceManager.PlanetPresets[0].Type,
                    _resourceManager.PlanetPresets[0].Ores.Select(ore => new OrePreset(ore.Name, _oreChance[ore.Name], ore.MinClusterSize, ore.MaxClusterSize)).ToList()
                )
            );
        }
    }
}
