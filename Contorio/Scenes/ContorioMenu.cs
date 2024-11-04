using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using System.Drawing;

namespace Contorio.Scenes
{
    public class ContorioMenu
    {
        private Renderer renderer;
        private Scene scene;
        
        private bool _online;
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

        public ContorioMenu(Renderer renderer)
        {
            this.renderer = renderer;
            scene = new Scene();

            InitializeUIElements();
        }

        public void Run()
        {
            renderer.SetScene(scene);
            _online = true;

            while (_online)
            {
                HandleKeyPress();

                if (savesPath.Length == 0 && itemListMenu.SelectedItem == "LOAD GAME")
                {
                    itemListMenu.SelectedItemColor = ConsoleColor.DarkRed;
                }
                else
                {
                    itemListMenu.SelectedItemColor = ConsoleColor.DarkGreen;
                }

                renderer.Render();
            }
        }

        private void InitializeUIElements()
        {
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
            contorioSprite.Position = new Point(renderer.ScreenWidth / 2 - (contorioSprite.Width / 2), 0);

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

            scene.AddSprite(contorioSprite);
            scene.AddSprite(itemListMenu);
            scene.AddSprite(labelCountSave);
            scene.AddSprite(itemListSavesList);
        }

        //Input
        void HandleKeyPress()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                savesPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ctsave");

                if (itemListMenu.Visible)
                {
                    KeyPressMainMenu(keyInfo.Key);
                    return;
                }

                if (itemListSavesList.Visible)
                {
                    KeyPressLoadMenu(keyInfo.Key);
                }

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
                            _choice = null;
                            _online = false;
                            break;
                        case "NEW GAME":
                            _choice = "new";
                            _online = false;
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
                    _online = false;
                    break;
                case ConsoleKey.Escape:
                    setVisibleLoadMenu(false);
                    break;
            }
        }

        //UI
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
                itemListSavesList.Position = new Point((renderer.ScreenWidth / 2) - (itemListSavesList.Width / 2), 13);
                UpdatelabelCountSave();
            }
        }

        private void UpdatelabelCountSave()
        {
            labelCountSave.Text = (itemListSavesList.SelectedIndex + 1) + "/" + savesPath.Count();
            labelCountSave.Position = new Point((renderer.ScreenWidth / 2) - (itemListSavesList.Width / 2), 12);
        }
    }
}
