using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using System.Drawing;

namespace Contorio.Scenes.SceneMenu
{
    public class SceneLoadGame : Scene
    {
        private Engine _engine;
        private SceneMenu _sceneMenu;

        public Label LabelCountSave;
        public ItemList ItemListSavesList;

        public string[] SavesPath;

        public SceneLoadGame(Engine engine, SceneMenu sceneMenu)
        {
            _engine = engine;
            _sceneMenu = sceneMenu;

            SavesPath = new string[0];

            // InitializeWidgets
            LabelCountSave = new Label("0/0", ConsoleColor.White, new Point(55, 12));
            ItemListSavesList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(55, 13),
                10
            );

            // AddSprite
            AddSprite(LabelCountSave);
            AddSprite(ItemListSavesList);
        }

        public override void Ready()
        {
            SavesPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ctsave");
            UpdateItemListSavesList();
            UpdateLabelCountSave();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    ItemListSavesList.NextItem();
                    UpdateLabelCountSave();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListSavesList.PreviousItem();
                    UpdateLabelCountSave();
                    break;
                case ConsoleKey.Enter:
                    _sceneMenu.Choice = ItemListSavesList.SelectedItem;
                    _engine.Quit();
                    break;
            }
        }

        private void UpdateLabelCountSave()
        {
            LabelCountSave.Text = ItemListSavesList.SelectedIndex + 1 + "/" + SavesPath.Count();
            LabelCountSave.Position = new Point(_engine.Renderer.ScreenWidth / 2 - ItemListSavesList.Width / 2, 12);
        }

        private void UpdateItemListSavesList()
        {
            ItemListSavesList.ClearItems();
            foreach (string file in SavesPath)
            {
                ItemListSavesList.AddItem(Path.GetFileName(file));
            }
            ItemListSavesList.Position = new Point(_engine.Renderer.ScreenWidth / 2 - ItemListSavesList.Width / 2, 13);
        }
    }
}
