using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;
using System.Drawing;

namespace Contorio.Scenes.SceneWorld
{
    public class ContainerResearch : Container
    {
        private SceneWorld _rootScene;

        private World _world;
        private ResearchSystem _researchSystem;

        public Label LabelTokensInfo;
        public Label LabelResearch;
        public ItemList ItemListResearchList;
        public Label LabelResearchCost;
        public Label LabelEnterToResearch;

        public ContainerResearch(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;
            _researchSystem = world.ResearchSystem;

            // InitializeWidgets
            LabelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1), visible: false);
            LabelResearch = new Label("Research", ConsoleColor.White, new Point(28, 15), visible: false);
            ItemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(28, 16),
                12,
                visible: false
            );
            LabelResearchCost = new Label("Cost", ConsoleColor.White, new Point(45, 15), visible: false);
            LabelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(28, 28), visible: false);

            // AddSprite
            AddSprite(LabelTokensInfo);
            AddSprite(LabelResearch);
            AddSprite(ItemListResearchList);
            AddSprite(LabelResearchCost);
            AddSprite(LabelEnterToResearch);
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
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListResearchList.NextItem();
                    UpdateResearchCost(ItemListResearchList.SelectedItem);
                    break;
                case ConsoleKey.UpArrow:
                    ItemListResearchList.PreviousItem();
                    UpdateResearchCost(ItemListResearchList.SelectedItem);
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
                            _rootScene.ContainerBuilding.UpdateBlockCategory();
                            _rootScene.ContainerBuilding.UpdateBlockList(_rootScene.ContainerBuilding.ItemListBlockCategory.SelectedItem);
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
    }
}
