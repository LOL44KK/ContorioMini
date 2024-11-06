using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using System.Drawing;

namespace Contorio.Scenes
{
    public class SceneMenu : Scene
    {
        Engine _engine;
        private string? _choice;
        
        public string? Choice 
        { 
            get { return _choice; }
        }

        // UI
        private Sprite contorioSprite;
        private ItemList itemListMenu;
        private Label labelCountSave;
        private ItemList itemListSavesList;

        // State variables
        private string[] savesPath = { };

        public SceneMenu(Engine engine)
        {
            _engine = engine;

            // InitializeWidgets
            contorioSprite = new Sprite(
                pixels: new Pixel[6, 70]
                {
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                }
            );
            contorioSprite.Position = new Point(_engine.Renderer.ScreenWidth / 2 - (contorioSprite.Width / 2), 0);

            itemListMenu = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkGreen,
                new Point(55, 13),
                10
            );
            itemListMenu.AddItem("NEW GAME");
            itemListMenu.AddItem("LOAD GAME");
            itemListMenu.AddItem("QUIT");

            labelCountSave = new Label("0/0", ConsoleColor.White, new Point(55, 12), visible: false);

            itemListSavesList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(55, 13),
                10,
                visible: false
            );

            AddSprite(contorioSprite);
            AddSprite(itemListMenu);
            AddSprite(labelCountSave);
            AddSprite(itemListSavesList);
        }

        public override void Tick()
        {
            if (savesPath.Length == 0 && itemListMenu.SelectedItem == "LOAD GAME")
            {
                itemListMenu.SelectedItemColor = ConsoleColor.DarkRed;
            }
            else
            {
                itemListMenu.SelectedItemColor = ConsoleColor.DarkGreen;
            }
        }

        public override void Input(ConsoleKey key)
        {
            savesPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ctsave");
            if (itemListMenu.Visible)
            {
                KeyPressMainMenu(key);
                return;
            }
            if (itemListSavesList.Visible)
            {
                KeyPressLoadMenu(key);
            }
        }

        private void KeyPressMainMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    itemListMenu.PreviousItem();
                    break;
                case ConsoleKey.DownArrow:
                    itemListMenu.NextItem();
                    break;
                case ConsoleKey.Enter:
                    switch (itemListMenu.SelectedItem)
                    {
                        case "QUIT":
                            _engine.Quit();
                            break;
                        case "NEW GAME":
                            _choice = "new";
                            _engine.Quit();
                            break;
                        case "LOAD GAME":
                            if (savesPath.Length > 0)
                            {
                                setVisibleLoadMenu(true);
                            }
                            break;
                    }
                    break;
            }
        }

        private void KeyPressLoadMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    itemListSavesList.PreviousItem();
                    UpdatelabelCountSave();
                    break;
                case ConsoleKey.DownArrow:
                    itemListSavesList.NextItem();
                    UpdatelabelCountSave();
                    break;
                case ConsoleKey.Enter:
                    _choice = itemListSavesList.SelectedItem;
                    _engine.Quit();
                    break;
                case ConsoleKey.Escape:
                    setVisibleLoadMenu(false);
                    break;
            }
        }

        private void setVisibleLoadMenu(bool visible)
        {
            itemListMenu.Visible = !visible;
            labelCountSave.Visible = visible;
            itemListSavesList.Visible = visible;

            if (visible)
            {
                itemListMenu.Visible = false;
                itemListSavesList.Visible = true;
                labelCountSave.Visible = true;

                itemListSavesList.ClearItems();
                foreach (string file in savesPath)
                {
                    itemListSavesList.AddItem(Path.GetFileName(file));
                }
                itemListSavesList.Position = new Point((_engine.Renderer.ScreenWidth / 2) - (itemListSavesList.Width / 2), 13);
                UpdatelabelCountSave();
            }
        }

        private void UpdatelabelCountSave()
        {
            labelCountSave.Text = (itemListSavesList.SelectedIndex + 1) + "/" + savesPath.Count();
            labelCountSave.Position = new Point((_engine.Renderer.ScreenWidth / 2) - (itemListSavesList.Width / 2), 12);
        }
    }
}
