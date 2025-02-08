using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld.SceneTokensMenu
{
    public class SceneResearch : Scene
    {
        private SceneWorld _rootScene;

        private ResourceManager _resourceManager;

        private World _world;
        private ResearchSystem _researchSystem;

        // Research
        public Label LabelResearch;
        public ItemList ItemListResearchList;
        public Label LabelResearchCost;
        public Label LabelEnterToResearch;

        public SceneResearch(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;

            _resourceManager = ResourceManager.Instance;

            _world = world;
            _researchSystem = world.ResearchSystem;

            // InitializeWidgets
            LabelResearch = new Label("Research", ConsoleColor.White, new Point(60, 3));
            ItemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(60, 4),
                20,
                visible: false
            );
            LabelResearchCost = new Label("Cost", ConsoleColor.White, new Point(78, 3));
            LabelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(60, 28));

            // AddSprite
            AddSprite(LabelResearch);
            AddSprite(ItemListResearchList);
            AddSprite(LabelResearchCost);
            AddSprite(LabelEnterToResearch);
        }

        public override void Ready()
        {
            if (_researchSystem.CountCloseResearchs > 0)
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
                    if (_researchSystem.CountCloseResearchs > 0)
                    {
                        UnlockResearch(ItemListResearchList.SelectedItem);
                    }
                    break;
            }
        }

        public void UnlockResearch(string researchName)
        {
            Research research;
            if (_resourceManager.Researches.TryGetValue(researchName, out research))
            {
                foreach (var token in research.ResearchCost)
                {
                    if (_world.Tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                    {
                        _rootScene.MessageMessage.Show("not enough " + token.Key, ConsoleColor.DarkRed);
                    }
                }
                if (research.RequiredResearch != null)
                {
                    if (!_resourceManager.Researches.ContainsKey(research.RequiredResearch))
                    {
                        _rootScene.MessageMessage.Show("not studied " + research.RequiredResearch, ConsoleColor.DarkRed);
                    }
                }

                if (_world.StudyResearch(researchName))
                {
                    UpdateResearchList();
                    _rootScene.SceneBuildingUI.Ready();
                }
            }

            if (_researchSystem.CountCloseResearchs > 0)
            {
                UpdateResearchCost(ItemListResearchList.SelectedItem);
            }
            else
            {
                LabelResearchCost.Visible = false;
                LabelEnterToResearch.Visible = false;
                LabelResearch.Visible = false;
            }
        }

        private void UpdateResearchList()
        {
            ItemListResearchList.ClearItems();
            foreach (var research in _world.ResearchSystem.Researchs.Where(res => res.Value == false))
            {
                ItemListResearchList.AddItem(research.Key);
            }
        }

        private void UpdateResearchCost(string name)
        {
            LabelResearchCost.Text = "Cost\n";
            foreach (var token in _resourceManager.Researches[name].ResearchCost)
            {
                LabelResearchCost.Text += token.Key + ": " + token.Value + "\n";
            }
            if (_resourceManager.Researches[name].RequiredResearch != null)
            {
                LabelResearchCost.Text += "Required: " + _resourceManager.Researches[name].RequiredResearch;
            }
        }
    }
}
