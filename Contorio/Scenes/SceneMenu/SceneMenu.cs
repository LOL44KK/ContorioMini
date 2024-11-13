using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.CharEngine.Containers;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneMenu
{
    public class SceneMenu : Scene
    {
        Engine _engine;
        private string? _choice;

        public string? Choice
        {
            get { return _choice; }
            set { _choice = value; }
        }

        public Sprite SpriteContorio;
        public ItemList ItemListMenu;
        public Label LabelVersion;

        public Container ContainerMain;

        public SceneLoadGame SceneLoadGame;
        public SceneNewGame SceneNewGame;

        public SceneMenu(Engine engine)
        {
            _engine = engine;

            // InitializeComponent
            SceneLoadGame = new SceneLoadGame(_engine, this);
            SceneNewGame = new SceneNewGame(_engine, this);

            // InitializeWidgets
            SpriteContorio = new Sprite(
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
            SpriteContorio.Position = new Point(_engine.Renderer.ScreenWidth / 2 - SpriteContorio.Width / 2, 0);
            ItemListMenu = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkGreen,
                new Point(55, 13),
                10
            );
            ItemListMenu.AddItem("NEW GAME");
            ItemListMenu.AddItem("LOAD GAME");
            ItemListMenu.AddItem("QUIT");
            LabelVersion = new Label(
                ResourceManager.ContorioMiniVersion,
                ConsoleColor.White,
                new Point(0, 29)
            );

            // ContainerMain
            ContainerMain = new Container();
            ContainerMain.AddSprite(ItemListMenu);

            // IncludeScene
            IncludeScene(SceneLoadGame);
            IncludeScene(SceneNewGame);

            // AddSprite
            AddSprite(SpriteContorio);
            AddSprite(ItemListMenu);
            AddSprite(LabelVersion);

            // Костя:)
            OnInput += Inputt;
        }

        public override void Ready()
        {
            ContainerMain.Visible = true;
            SceneLoadGame.Enable = false;
            SceneNewGame.Enable = false;
        }

        public override void Tick()
        {
            UpdateColorLoadGameButton();
        }

        public void Inputt(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (ContainerMain.Visible)
                    {
                        ItemListMenu.PreviousItem();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (ContainerMain.Visible)
                    {
                        ItemListMenu.NextItem();
                    }
                    break;
                case ConsoleKey.Enter:
                    if (ContainerMain.Visible)
                    {
                        switch (ItemListMenu.SelectedItem)
                        {
                            case "QUIT":
                                _choice = null;
                                _engine.Quit();
                                break;
                            case "NEW GAME":
                                ContainerMain.Visible = false;
                                SceneNewGame.Enable = true;
                                break;
                            case "LOAD GAME":
                                if (SceneLoadGame.SavesPath.Length > 0)
                                {
                                    ContainerMain.Visible = false;
                                    SceneLoadGame.Enable = true;
                                }
                                break;
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    if (SceneLoadGame.Enable || SceneNewGame.Enable)
                    {
                        ContainerMain.Visible = true;
                        SceneLoadGame.Enable = false;
                        SceneNewGame.Enable = false;
                    }
                    break;
            }
        }

        private void UpdateColorLoadGameButton()
        {
            if (SceneLoadGame.SavesPath.Length == 0 && ItemListMenu.SelectedItem == "LOAD GAME")
            {
                ItemListMenu.SelectedItemColor = ConsoleColor.DarkRed;
            }
            else
            {
                ItemListMenu.SelectedItemColor = ConsoleColor.DarkGreen;
            }
        }
    }
}
