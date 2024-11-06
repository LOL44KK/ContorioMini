using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Managers;
using Contorio.Core.Types;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneResearch : Scene
    {
        private SceneWorld _rootScene;

        private World _world;
        private ResearchSystem _researchSystem;

        public Label LabelTokensInfo;
        
        // Research
        public Label LabelResearch;
        public ItemList ItemListResearchList;
        public Label LabelResearchCost;
        public Label LabelEnterToResearch;

        // Search Planet
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

        public TabContainer TabContainer;
        public ItemListContainer ItemListContainerSearchPlanet;

        public Dictionary<string, double> _oreChance;

        public SceneResearch(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;
            _researchSystem = world.ResearchSystem;

            _oreChance = new Dictionary<string, double>();

            // InitializeWidgets
            //   Research
            LabelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1), visible: false);
            LabelResearch = new Label("Research", ConsoleColor.White, new Point(60, 1), visible: false);
            ItemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(60, 2),
                12,
                visible: false
            );
            LabelResearchCost = new Label("Cost", ConsoleColor.White, new Point(60+18, 1), visible: false);
            LabelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(60, 28), visible: false);
            
            //   Search Planet
            LabelSearchPlanet = new Label("SEARCH PLANET", ConsoleColor.White, new Point(75, 1), visible: false);
            LabelPlanetSize = new Label("Size: ", ConsoleColor.White, new Point(75, 3), visible: false);
            ItemListPlanetSize = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(88, 3),
                1,
                visible: false
            );
            for (int i = 16; i < 130; i += 2) ItemListPlanetSize.AddItem("" + i);

            LabelOreName = new Label("Ore name: ", ConsoleColor.White, new Point(75, 5), visible: false);
            ItemListOreName = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(85, 5),
                1,
                visible: false
            );

            LabelOreChance = new Label("Chance: ", ConsoleColor.White, new Point(75, 6), visible: false);
            ItemListOreChance = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(83, 6),
                1,
                visible: false
            );
            for (double i = 0; i < 1000; i += 5) ItemListOreChance.AddItem("" + i / 10000);

            LabelPalkaPeredCostSearchPlanet = new Label("================", ConsoleColor.White, new Point(75, 7), visible: false);
            LabelCostSearchPlanet = new Label("Cost: 0 PL", ConsoleColor.White, new Point(75, 8), visible: false);
            LabelPressGtoSearchPlanet = new Label("Press G to search", ConsoleColor.White, new Point(75, 9), visible: false);

            // TabContainer
            TabContainer = new TabContainer();
            TabContainer.AddTab(); // TabIndex 0  Research
            TabContainer.AddSprite(0, LabelResearch);
            TabContainer.AddSprite(0, ItemListResearchList);
            TabContainer.AddSprite(0, LabelResearchCost);
            TabContainer.AddSprite(0, LabelEnterToResearch);
            TabContainer.AddTab(); // TabIndex 1  SearchPlanet
            TabContainer.AddSprite(1, LabelSearchPlanet);
            TabContainer.AddSprite(1, LabelPlanetSize);
            TabContainer.AddSprite(1, ItemListPlanetSize);
            TabContainer.AddSprite(1, LabelOreName);
            TabContainer.AddSprite(1, ItemListOreName);
            TabContainer.AddSprite(1, LabelOreChance);
            TabContainer.AddSprite(1, ItemListOreChance);
            TabContainer.AddSprite(1, LabelPalkaPeredCostSearchPlanet);
            TabContainer.AddSprite(1, LabelCostSearchPlanet);
            TabContainer.AddSprite(1, LabelPressGtoSearchPlanet);
            // ItemListContainer
            ItemListContainerSearchPlanet = new ItemListContainer(inactiveListSelectedItemColor:ConsoleColor.White);
            ItemListContainerSearchPlanet.AddItemList(ItemListPlanetSize);
            ItemListContainerSearchPlanet.AddItemList(ItemListOreName);
            ItemListContainerSearchPlanet.AddItemList(ItemListOreChance);

            // AddSprite
            AddSprite(LabelTokensInfo);
            AddSprite(LabelResearch);
            AddSprite(ItemListResearchList);
            AddSprite(LabelResearchCost);
            AddSprite(LabelEnterToResearch);
            
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
            if (_researchSystem.CloseResearch.Count > 0)
            {
                UpdateResearchList();
                UpdateResearchCost(ItemListResearchList.SelectedItem);
            }
            else
            {
                LabelResearchCost.Visible = false;
                LabelEnterToResearch.Visible = false;
                LabelResearch.Visible = false;
            }

            if (Visible)
            {
                TabContainer.SelectedTab = 0;
            }

            UpdateOreNameList(ResourceManager.Instance.PlanetPresets[0]);
            UpdateCostSearchPlanet();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (TabContainer.SelectedTab == 0)
                    {
                        ItemListResearchList.NextItem();
                        UpdateResearchCost(ItemListResearchList.SelectedItem);
                    }
                    else if (TabContainer.SelectedTab == 1)
                    {
                        ItemListContainerSearchPlanet.NextItemList();
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (TabContainer.SelectedTab == 0)
                    {
                        ItemListResearchList.PreviousItem();
                        UpdateResearchCost(ItemListResearchList.SelectedItem);
                    }
                    else if (TabContainer.SelectedTab == 1)
                    {
                        ItemListContainerSearchPlanet.PreviousItemList();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (TabContainer.SelectedTab == 1)
                    {
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
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (TabContainer.SelectedTab == 1)
                    {
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
                    }
                    break;
                case ConsoleKey.D1:
                    TabContainer.SelectedTab = 0;
                    break;
                case ConsoleKey.D2:
                    TabContainer.SelectedTab = 1;
                    break;
                case ConsoleKey.Enter:
                    if (_researchSystem.CloseResearch.Count > 0)
                    {
                        foreach (var token in _researchSystem.CloseResearch[ItemListResearchList.SelectedItem].ResearchCost)
                        {
                            if (_world.Tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                            {
                                _rootScene.MessageMessage.Show("not enough " + token.Key, ConsoleColor.DarkRed);
                            }
                        }
                        if (_researchSystem.CloseResearch[ItemListResearchList.SelectedItem].RequiredResearch != null)
                        {
                            if (!_researchSystem.OpenResearch.ContainsKey(_researchSystem.CloseResearch[ItemListResearchList.SelectedItem].RequiredResearch))
                            {
                                _rootScene.MessageMessage.Show("not studied " + _researchSystem.CloseResearch[ItemListResearchList.SelectedItem].RequiredResearch, ConsoleColor.DarkRed);
                            }
                        }
                        if (_world.StudyResearch(ItemListResearchList.SelectedItem))
                        {
                            UpdateResearchList();
                            _rootScene.SceneBuilding.UpdateBlockCategory();
                            _rootScene.SceneBuilding.UpdateBlockList(_rootScene.SceneBuilding.ItemListBlockCategory.SelectedItem);
                        }
                    }
                    if (_researchSystem.CloseResearch.Count > 0)
                    {
                        UpdateResearchCost(ItemListResearchList.SelectedItem);
                    }
                    else
                    {
                        LabelResearchCost.Visible = false;
                        LabelEnterToResearch.Visible = false;
                        LabelResearch.Visible = false;
                    }
                    break;
            }
        }

        public override void Tick()
        {
            UpdateTokensInfo();
        }

        private void UpdateTokensInfo()
        {
            LabelTokensInfo.Text = "TOKENS\n";
            foreach (var token in _world.Tokens)
            {
                LabelTokensInfo.Text += "  " + token.Key + ": " + token.Value + "\n";
            }
        }

        private void UpdateResearchList()
        {
            ItemListResearchList.ClearItems();
            foreach (var research in _world.ResearchSystem.CloseResearch)
            {
                ItemListResearchList.AddItem(research.Value.Name);
            }
        }

        private void UpdateResearchCost(string name)
        {
            LabelResearchCost.Text = "Cost\n";
            foreach (var token in _world.ResearchSystem.CloseResearch[name].ResearchCost)
            {
                LabelResearchCost.Text += token.Key + ": " + token.Value + "\n";
            }
            if (_world.ResearchSystem.CloseResearch[name].RequiredResearch != null)
            {
                LabelResearchCost.Text += "Required: " + _world.ResearchSystem.CloseResearch[name].RequiredResearch;
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
            ResourceManager resourceManager = ResourceManager.Instance;
            LabelCostSearchPlanet.Text = "cost: " + World.CalculateCostSearchPlanet(
                new PlanetPreset(
                    // Из BaseMode
                    resourceManager.PlanetPresets[0].Name,
                    int.Parse(ItemListPlanetSize.SelectedItem ?? "32"),
                    resourceManager.PlanetPresets[0].Dirt,
                    resourceManager.PlanetPresets[0].Type,
                    resourceManager.PlanetPresets[0].Ores.Select(ore => new OrePreset(ore.Name, _oreChance[ore.Name], ore.MinClusterSize, ore.MaxClusterSize)).ToList()
                )
            ) + " PL";
        }
    }
}
