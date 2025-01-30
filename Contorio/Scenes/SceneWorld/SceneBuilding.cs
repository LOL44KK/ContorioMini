using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneBuilding : Scene
    {
        private SceneWorld _rootScene;
        private World _world;
        private Player _player;

        public Sprite SpriteBlockPlayerSelectedBlock;
        public ItemList ItemListBlockCategory;
        public ItemList ItemListBlockList;
        public Label LabelCostBuilding;
        public Label LabelPlayerResources;


        private string _selectedBlock;
        private int _selectedItemList;

        public SceneBuilding(SceneWorld rootScene, World world)
        {
            _rootScene = rootScene;
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            SpriteBlockPlayerSelectedBlock = new Sprite(ResourceManager.Instance.TileSet.Tiles[3].Pixels, position: new Point(102, 1));
            ItemListBlockCategory = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(95, 5),
                9
            );
            ItemListBlockList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(105, 5),
                9
            );
            LabelCostBuilding = new Label("COST", ConsoleColor.White, new Point(108, 0));
            LabelPlayerResources = new Label("RESOURCES", ConsoleColor.White, new Point(95, 15));
            

            // AddSprite
            AddSprite(SpriteBlockPlayerSelectedBlock);
            AddSprite(ItemListBlockCategory);
            AddSprite(ItemListBlockList);
            AddSprite(LabelCostBuilding);
            AddSprite(LabelPlayerResources);

            //
            UpdateBlockCategory();
            UpdateBlockList(ItemListBlockCategory.SelectedItem);
            _selectedBlock = ItemListBlockList.SelectedItem;
        }

        public override void Ready()
        {
            _selectedBlock = ItemListBlockList.SelectedItem;
            UpdateSpriteBlockPlayerSelectedBlock(_selectedBlock);
            UpdateCostBuilding(_selectedBlock);
        }

        public override void Tick()
        {
            UpdatePlayerResources();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    Block block = ResourceManager.Instance.Blocks[_selectedBlock];
                    if (!_world.Planets[_player.Planet].Ground.ContainsKey(_player.Coord))
                    {
                        _rootScene.MessageMessage.Show("No building here", ConsoleColor.DarkRed);
                        break;
                    }

                    foreach (var resource in block.Cost)
                    {
                        if (_player.Resources.GetValueOrDefault(resource.Key, 0) < resource.Value)
                        {
                            _rootScene.MessageMessage.Show("not enough " + resource.Key, ConsoleColor.DarkRed);
                            break;
                        }
                    }

                    if (_player.BuildBlock(_player.Coord, block, _world.Planets[_player.Planet]))
                    {
                        _rootScene.SceneTileMap.Map.SetCell(1, _player.Coord, ResourceManager.Instance.TileIds[_selectedBlock]);
                    }
                    break;
                case ConsoleKey.E:
                    _player.DestroyBlock(_player.Coord, _world.Planets[_player.Planet]);
                    _rootScene.SceneTileMap.Map.RemoveCell(SceneTileMap.LayerBlock, _player.Coord);
                    break;
                case ConsoleKey.DownArrow:
                    HandleNavigation(true);
                    break;
                case ConsoleKey.UpArrow:
                    HandleNavigation(false);
                    break;
                case ConsoleKey.LeftArrow:
                    _selectedItemList = (_selectedItemList + 1) % 2;
                    UpdateSelectedItemList();
                    break;
                case ConsoleKey.RightArrow:
                    _selectedItemList = (_selectedItemList - 1 + 2) % 2;
                    UpdateSelectedItemList();
                    break;
            }
        }

        private void UpdateSelectedItemList()
        {
            ItemListBlockCategory.SelectedItemColor = (_selectedItemList == 0) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
            ItemListBlockList.SelectedItemColor = (_selectedItemList == 1) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
        }

        private void HandleNavigation(bool next)
        {
            if (_selectedItemList == 0)
            {
                if (next) ItemListBlockCategory.NextItem(); else ItemListBlockCategory.PreviousItem();
                UpdateBlockList(ItemListBlockCategory.SelectedItem);
            }
            if (_selectedItemList == 1)
            {
                if (next) ItemListBlockList.NextItem(); else ItemListBlockList.PreviousItem();
            }
            _selectedBlock = ItemListBlockList.SelectedItem;
            UpdateSpriteBlockPlayerSelectedBlock(_selectedBlock);
            UpdateCostBuilding(_selectedBlock);
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
