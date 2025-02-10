using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using CharEngine.Containers;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneBuildingUI : Scene
    {
        private SceneWorld _rootScene;
        private World _world;
        private Player _player;

        public Sprite SpriteBlockPlayerSelectedBlock;
        public ItemList ItemListBlockCategory;
        public ItemList ItemListBlockList;
        public Label LabelCostBuilding;
        public Label LabelPlayerResources;
        
        public ItemListContainer ItemListContainer;

        public string? SelectedBlockToBuild;

        public SceneBuildingUI(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            SpriteBlockPlayerSelectedBlock = new Sprite(ResourceManager.Instance.TileSet.Tiles[3].Pixels, position: new Point(102, 1));
            ItemListBlockCategory = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(95, 5),
                9
            );
            ItemListBlockList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(105, 5),
                9
            );
            LabelCostBuilding = new Label("COST", ConsoleColor.White, new Point(108, 0));
            LabelPlayerResources = new Label("RESOURCES", ConsoleColor.White, new Point(95, 15));

            ItemListContainer = new ItemListContainer();
            ItemListContainer.AddItemList(ItemListBlockList);
            ItemListContainer.AddItemList(ItemListBlockCategory);

            // AddSprite
            AddSprite(SpriteBlockPlayerSelectedBlock);
            AddSprite(ItemListBlockCategory);
            AddSprite(ItemListBlockList);
            AddSprite(LabelCostBuilding);
            AddSprite(LabelPlayerResources);

            UpdateBlockCategory();
            UpdateBlockList(ItemListBlockCategory.SelectedItem);
        }

        public override void Ready()
        {
            if (ResourceManager.Instance.Blocks.Count == 0)
            {
                return;
            }

            SelectedBlockToBuild = ItemListBlockList.SelectedItem;
            UpdateSpriteBlockPlayerSelectedBlock(SelectedBlockToBuild);
            UpdateCostBuilding(SelectedBlockToBuild);
        }

        public override void Tick()
        {
            UpdatePlayerResources();
        }

        public override void Input(ConsoleKey key)
        {
            if (ResourceManager.Instance.Blocks.Count == 0)
            {
                return;
            }

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    ItemListContainer.PreviousItem();
                    HandleNavigation();
                    break;
                case ConsoleKey.DownArrow:
                    ItemListContainer.NextItem();
                    HandleNavigation();
                    break;
                case ConsoleKey.LeftArrow:
                    ItemListContainer.NextItemList();
                    HandleNavigation();
                    break;
                case ConsoleKey.RightArrow:
                    ItemListContainer.PreviousItemList();
                    HandleNavigation();
                    break;
            }
        }

        private void HandleNavigation()
        {
            if (ItemListContainer.IsSelectedItemList(ItemListBlockCategory))
            {
                UpdateBlockList(ItemListBlockCategory.SelectedItem);
            }

            SelectedBlockToBuild = ItemListBlockList.SelectedItem;
            UpdateSpriteBlockPlayerSelectedBlock(SelectedBlockToBuild);
            UpdateCostBuilding(SelectedBlockToBuild);
        }

        public void UpdateBlockCategory()
        {
            SortedSet<string> categoryes = new SortedSet<string>();
            foreach (var researchName in _world.ResearchSystem.Researchs)
            {
                if (researchName.Value)
                {
                    categoryes.Add(ResourceManager.Instance.Researches[researchName.Key].Category);
                }
            }

            ItemListBlockCategory.ClearItems();
            foreach (var category in categoryes)
            {
                ItemListBlockCategory.AddItem(category);
            }
        }

        public void UpdateBlockList(string category)
        {
            ItemListBlockList.ClearItems();
            foreach (var researchName in _world.ResearchSystem.Researchs)
            {
                if (researchName.Value)
                {
                    Research research = ResourceManager.Instance.Researches[researchName.Key];
                    if (research.Category == category)
                    {
                        ItemListBlockList.AddItem(research.Name);
                    }
                }
            }
        }

        public void UpdateCostBuilding(string name)
        {
            Block block = ResourceManager.Instance.Blocks[name];

            LabelCostBuilding.Text = "COST\n";
            foreach (var resource in block.Cost)
            {
                LabelCostBuilding.Text += resource.Key + ": " + resource.Value + "\n";
            }
        }

        public void UpdateSpriteBlockPlayerSelectedBlock(string blockName)
        {
            SpriteBlockPlayerSelectedBlock.Pixels = ResourceManager.Instance.Blocks[blockName].PixelCanvas.Pixels;
        }

        public void UpdatePlayerResources()
        {
            LabelPlayerResources.Text = "RESOURCES\n";
            foreach (var resource in _player.Resources)
            {
                LabelPlayerResources.Text += $"{resource.Key}: {resource.Value}\n";
            }
        }
    }
}
