using Contorio.CharGraphics.Widgets;
using Contorio.CharGraphics;
using System.Diagnostics;
using System.Drawing;


namespace Contorio.Scenes
{
    public class ContorioWorld
    {
        private Renderer renderer;
        private Scene scene;
        private bool _online;

        private ResourceManager resourceManager = ResourceManager.Instance;
        private World world;
        private Player player;
        private WorldHandler worldHandler;
        private ResearchSystem researchSystem;

        // UI
        // Map UI
        private TileMap tileMap;

        // Player UI
        private Label labelPlayerCoord;
        private Sprite blockPlayerCoord;
        private Label labelPlayerResources;

        // Planet info UI
        private Label labelPlanetInfo;
        private Label labelPlanetsLabel;
        private ItemList itemListPlanetList;

        // Building UI
        private Sprite blockPlayerSelectedBlock;
        private ItemList itemListBlockCategory;
        private ItemList itemListBlockList;
        private Label labelCostBuilding;

        // Research menu UI
        private Label labelTokensInfo;
        private Label labelResearch;
        private ItemList itemListResearchList;
        private Label labelResearchCost;
        private Label labelEnterToResearch;

        // Planet search UI
        private Label labelSearchPlanet;
        private Label labelPlanetSize;
        private ItemList itemListPlanetSize;
        private Label labelOreName;
        private ItemList itemListOreName;
        private Label labelOreChance;
        private ItemList itemListOreChance;
        private Label labelPalkaPeredCostSearchPlanet;
        private Label labelCostSearchPlanet;
        private Label labelPressGtoSearchPlanet;

        // Resource transfer UI (TAB menu)
        private Label labelResourcesToPlayer;
        private ItemList itemListPlanetResourcesToPlayer;
        private Label labelPlayerResourcesToPlanet;
        private ItemList itemListPlayerResourcesToPlanet;
        private Label labelCountResource;
        private Label labelTransfer;
        private ItemList itemListResourcesToPlayerCount;
        private Label labelPreesEToTransferPlayer;
        private Label labelPreesFToTransferPlanet;

        //Block transferBeacon UI
        private Label labelTransferBeacon;
        private ItemList itemListTransferBeaconMenuResourcesList;
        private ItemList itemListTransferBeaconMenuCount;
        private Label LabelListTransferBeaconMenuPreesEnterToBack;

        // F3 UI
        private Label labelFPS;

        // Message UI
        private Message messageMessage;


        // State variables
        private bool researchMenu = false;
        private bool TABmenu = false;
        private bool buildingMode = true;
        private bool categoryOrBlock = true;
        private string selectBlock = "drill";
        private string selectCategory = "logic";
        private int researchMenuSelectedItemList = 0;
        private int TABMenuSelectedItemList = 0;
        private Dictionary<string, int> oreChance = new Dictionary<string, int>();


        public ContorioWorld(Renderer renderer)
        {
            this.renderer = renderer;

            scene = new Scene();

            InitializeUIElements();
        }

        public void Run(World world)
        {
            renderer.SetScene(scene);

            this.world = world;
            researchSystem = world.ResearchSystem;
            worldHandler = new WorldHandler(world);
            player = world.Player;

            SetupInitialState();

            Stopwatch sw = Stopwatch.StartNew();

            double ms = 0;
            _online = true;
            while (_online)
            {
                sw.Restart();

                //World
                worldHandler.Tick();

                //Key Input
                HandleKeyPress();

                //UI
                UpdatePlanetInfo(labelPlanetInfo, world.Planets[player.Planet]);
                UpdatePlayerResourcesInfo(labelPlayerResources, player);


                if (tileMap.Visible)
                {
                    tileMap.UpdatePixels(player.Coord);
                    labelPlayerCoord.Text = player.Coord.X + "|" + player.Coord.Y;
                    UpdateSpritePlayerCoordBlock();
                }

                if (researchMenu)
                {
                    UpdateTokensInfo(world, labelTokensInfo);
                    labelCostSearchPlanet.Text = "cost: " + World.CalculateCostSearchPlanet(int.Parse(itemListPlanetSize.SelectedItem), oreChance) + " PL";
                }

                if (TABmenu)
                {
                    UpdatePlanetResourcesToPlayer();
                    UpdatePlayerResourcesToPlanet();
                    UpdateCountTransferResources();
                }

                //Message
                messageMessage.Tick();

                labelFPS.Text = "FPS: " + ((int)(1000 / ms)).ToString();

                //Graphics
                renderer.Render();

                ms = sw.Elapsed.TotalMilliseconds;
            }
        }

        private void InitializeUIElements()
        {
            tileMap = new TileMap(
                66,
                30,
                resourceManager.TileSet,
                new Point(27, 1),
                cellPaddingRight: 2,
                cellPaddingBottom: 1
            );
            tileMap.addLayer(0);
            tileMap.addLayer(1);

            labelPlayerCoord = new Label("X|Y", ConsoleColor.White, new Point(95, 0));
            blockPlayerCoord = new Sprite(resourceManager.TileSet.Tiles[0].Pixels, position: new Point(95, 1));
            labelPlayerResources = new Label("Resources", ConsoleColor.White, new Point(95, 15));

            labelPlanetInfo = new Label("Planet", ConsoleColor.White, new Point(0, 0));
            labelPlanetsLabel = new Label("PLANETS", ConsoleColor.White, new Point(0, 20));
            itemListPlanetList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(0, 21),
                9
            );

            blockPlayerSelectedBlock = new Sprite(resourceManager.TileSet.Tiles[3].Pixels, position: new Point(102, 1));
            itemListBlockCategory = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(95, 5),
                9
            );
            itemListBlockList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(105, 5),
                9
            );
            labelCostBuilding = new Label("COST", ConsoleColor.White, new Point(108, 0));

            labelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1), visible: false);
            labelResearch = new Label("Research", ConsoleColor.White, new Point(28, 15), visible: false);
            itemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(28, 16),
                12,
                visible: false
            );
            labelResearchCost = new Label("Cost", ConsoleColor.White, new Point(45, 15), visible: false);
            labelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(28, 28), visible: false);

            labelSearchPlanet = new Label("SEARCH PLANET", ConsoleColor.White, new Point(75, 1), visible: false);
            labelPlanetSize = new Label("Size: ", ConsoleColor.White, new Point(75, 3), visible: false);
            itemListPlanetSize = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(88, 3),
                1,
                visible: false
            );
            for (int i = 16; i < 130; i += 2)
            {
                itemListPlanetSize.AddItem("" + i);
            }

            labelOreName = new Label("Ore name: ", ConsoleColor.White, new Point(75, 5), visible: false);
            itemListOreName = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(85, 5),
                1,
                visible: false
            );
            oreChance.Clear();
            foreach (var ore in resourceManager.Grounds)
            {
                if (ore.Value.Type == GroundType.ORE)
                {
                    itemListOreName.AddItem(ore.Key);
                    oreChance.Add(ore.Key, 5);
                }
            }

            labelOreChance = new Label("Chance: ", ConsoleColor.White, new Point(75, 6), visible: false);
            itemListOreChance = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(83, 6),
                1,
                visible: false
            );
            for (int i = 5; i < 100; i++)
            {
                itemListOreChance.AddItem("" + i);
            }

            labelPalkaPeredCostSearchPlanet = new Label("================", ConsoleColor.White, new Point(75, 7), visible: false);
            labelCostSearchPlanet = new Label("Cost: 0 PL", ConsoleColor.White, new Point(75, 8), visible: false);
            labelPressGtoSearchPlanet = new Label("Press G to search", ConsoleColor.White, new Point(75, 9), visible: false);

            labelResourcesToPlayer = new Label("RESOURCES", ConsoleColor.White, new Point(27, 1), visible: false);
            itemListPlanetResourcesToPlayer = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(27, 2),
                12,
                visible: false
            );
            labelPlayerResourcesToPlanet = new Label("RESOURCES", ConsoleColor.White, new Point(93 - "RESOURCES".Length, 1), visible: false);
            itemListPlayerResourcesToPlanet = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(84, 2),
                12,
                visible: false
            );
            labelCountResource = new Label("Count: 0", ConsoleColor.White, new Point((renderer.ScreenWidth / 2) - ("Count: 0".Length / 2), 1), visible: false);
            labelTransfer = new Label("transfer: ", ConsoleColor.White, new Point((renderer.ScreenWidth / 2) - ("transfer: ".Length / 2), 2), visible: false);
            itemListResourcesToPlayerCount = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(((renderer.ScreenWidth / 2) - (1 / 2)) + "transfer: ".Length / 2, 2),
                1,
                visible: false
            );
            for (int i = 1; i < 10000; i *= 2)
            {
                itemListResourcesToPlayerCount.AddItem("" + i);
            }
            labelPreesEToTransferPlayer = new Label("Prees E to transfer to player", ConsoleColor.White, new Point((renderer.ScreenWidth / 2) - ("Prees E to transfer to player".Length / 2), 3), visible: false);
            labelPreesFToTransferPlanet = new Label("Prees F to transfer to planet", ConsoleColor.White, new Point((renderer.ScreenWidth / 2) - ("Prees F to transfer to planet".Length / 2), 4), visible: false);

            labelTransferBeacon = new Label("Transfer Beacon MENU", ConsoleColor.White, new Point(60, 10), visible: false);
            labelTransferBeacon.Position = new Point((renderer.ScreenWidth / 2) - (labelTransferBeacon.Width / 2), labelTransferBeacon.Position.Y);
            itemListTransferBeaconMenuResourcesList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(60, 11),
                1,
                visible: false
            );
            itemListTransferBeaconMenuCount = new ItemList(ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(60, 12),
                1,
                visible: false
            );

            labelFPS = new Label("FPS: 0", ConsoleColor.White, new Point(111, 0), visible: false);

            messageMessage = new Message(renderer.ScreenWidth / 2, 29);

            scene.AddSprite(tileMap);

            scene.AddSprite(labelPlayerCoord);
            scene.AddSprite(blockPlayerCoord);
            scene.AddSprite(labelPlayerResources);

            scene.AddSprite(blockPlayerSelectedBlock);
            scene.AddSprite(itemListBlockCategory);
            scene.AddSprite(itemListBlockList);
            scene.AddSprite(labelCostBuilding);

            scene.AddSprite(labelPlanetInfo);
            scene.AddSprite(labelPlanetsLabel);
            scene.AddSprite(itemListPlanetList);

            scene.AddSprite(labelTokensInfo);
            scene.AddSprite(labelResearch);
            scene.AddSprite(itemListResearchList);
            scene.AddSprite(labelResearchCost);
            scene.AddSprite(labelEnterToResearch);
            scene.AddSprite(labelSearchPlanet);
            scene.AddSprite(itemListPlanetSize);
            scene.AddSprite(labelPlanetSize);
            scene.AddSprite(labelOreName);
            scene.AddSprite(itemListOreName);
            scene.AddSprite(labelOreChance);
            scene.AddSprite(itemListOreChance);
            scene.AddSprite(labelCostSearchPlanet);
            scene.AddSprite(labelPalkaPeredCostSearchPlanet);
            scene.AddSprite(labelPressGtoSearchPlanet);

            scene.AddSprite(labelResourcesToPlayer);
            scene.AddSprite(itemListPlanetResourcesToPlayer);
            scene.AddSprite(labelPlayerResourcesToPlanet);
            scene.AddSprite(itemListPlayerResourcesToPlanet);
            scene.AddSprite(labelCountResource);
            scene.AddSprite(labelTransfer);
            scene.AddSprite(itemListResourcesToPlayerCount);
            scene.AddSprite(labelPreesEToTransferPlayer);
            scene.AddSprite(labelPreesFToTransferPlanet);

            scene.AddSprite(labelTransferBeacon);
            scene.AddSprite(itemListTransferBeaconMenuResourcesList);
            scene.AddSprite(itemListTransferBeaconMenuCount);

            scene.AddSprite(labelFPS);

            scene.AddSprite(messageMessage);
        }

        private void SetupInitialState()
        {
            loadMap(tileMap, world.Planets[player.Planet]);
            UpdatePlanetList(itemListPlanetList, world);
            UpdateBlockCategory(researchSystem, itemListBlockCategory);
            UpdateBlockList(researchSystem, itemListBlockList, itemListBlockCategory.SelectedItem);
            UpdateResearchList(researchSystem, itemListResearchList);
            UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);

            selectCategory = itemListBlockCategory.SelectedItem;
            selectBlock = itemListBlockList.SelectedItem;
            blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
            itemListPlanetList.SelectedIndex = player.Planet;

            UpdateCostBuilding(labelCostBuilding, selectBlock);
        }

        //Input
        void HandleKeyPress()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                //Main
                switch (keyInfo.Key)
                {
                    case ConsoleKey.B:
                        if (!TABmenu && !researchMenu)
                        {
                            SetBuildingMode(!buildingMode);
                        }
                        break;
                    case ConsoleKey.R:
                        SetVisibleTABMenu(false);
                        SetBuildingMode(false);
                        SetVisibleTransferBeaconMenu(false);
                        SetVisibleMap(researchMenu);
                        setVisibleResearchMenu(!researchMenu);
                        break;
                    case ConsoleKey.Tab:
                        setVisibleResearchMenu(false);
                        SetBuildingMode(false);
                        SetVisibleTransferBeaconMenu(false);
                        SetVisibleMap(TABmenu);
                        SetVisibleTABMenu(!TABmenu);
                        break;
                    case ConsoleKey.Enter:
                        if (tileMap.Visible && !buildingMode)
                        {
                            SetVisibleTABMenu(false);
                            SetBuildingMode(false);
                            setVisibleResearchMenu(false);
                            SetVisibleMap(labelTransferBeacon.Visible);
                            SetVisibleTransferBeaconMenu(!labelTransferBeacon.Visible);

                            UpdateItemListTransferBeaconMenuResourcesList();
                        }
                        break;
                    case ConsoleKey.F3:
                        labelFPS.Visible = !labelFPS.Visible;
                        break;
                    case ConsoleKey.G:
                        player.GodMode = !player.GodMode;
                        break;
                    case ConsoleKey.H:
                        SaveManager.SaveWorld($"{world.Planets[0].Name}.ctsave", world);
                        messageMessage.Show("Successfully saved", ConsoleColor.DarkGreen);
                        break;
                    case ConsoleKey.Escape:
                        SaveManager.SaveWorld($"{world.Planets[0].Name}.ctsave", world);
                        _online = false;
                        break;
                }

                if (tileMap.Visible)
                {
                    HandleKeyPressMap(keyInfo);
                }

                if (!buildingMode && !researchMenu && !TABmenu)
                {
                    HandleKeyPressSwitchPlanet(keyInfo);
                }

                if (tileMap.Visible && buildingMode)
                {
                    HandleKeyPressBuilding(keyInfo);
                }

                if (researchMenu && !TABmenu)
                {
                    HandleKeyPressResearchMenu(keyInfo);
                }

                if (TABmenu && !researchMenu && !buildingMode)
                {
                    HandleKeyPressTABMenu(keyInfo);
                }

                if (labelTransferBeacon.Visible)
                {

                }
            }
        }


        void HandleKeyPressTransferBeaconMenu(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {

            }
        }

        void HandleKeyPressMap(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    player.Move(0, -1);
                    break;
                case ConsoleKey.S:
                    player.Move(0, 1);
                    break;
                case ConsoleKey.A:
                    player.Move(-1, 0);
                    break;
                case ConsoleKey.D:
                    player.Move(1, 0);
                    break;
            }
        }

        void HandleKeyPressSwitchPlanet(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    itemListPlanetList.NextItem();
                    player.Planet = itemListPlanetList.SelectedIndex;
                    player.Coord = new Point(world.Planets[player.Planet].Size / 2, world.Planets[player.Planet].Size / 2);
                    loadMap(tileMap, world.Planets[player.Planet]);
                    UpdatePlanetInfo(labelPlanetInfo, world.Planets[player.Planet]);
                    break;
                case ConsoleKey.UpArrow:
                    itemListPlanetList.PreviousItem();
                    player.Planet = itemListPlanetList.SelectedIndex;
                    player.Coord = new Point(world.Planets[player.Planet].Size / 2, world.Planets[player.Planet].Size / 2);
                    loadMap(tileMap, world.Planets[player.Planet]);
                    UpdatePlanetInfo(labelPlanetInfo, world.Planets[player.Planet]);
                    break;
            }
        }

        void HandleKeyPressBuilding(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    Block block = resourceManager.Blocks[selectBlock];
                    if (!world.Planets[player.Planet].Ground.ContainsKey(player.Coord))
                    {
                        messageMessage.Show("No building here", ConsoleColor.DarkRed);
                        break;
                    }
                    
                    foreach (var resource in block.Cost)
                    {
                        if (player.Resources.GetValueOrDefault(resource.Key, 0) < resource.Value)
                        {
                            messageMessage.Show("not enough " + resource.Key, ConsoleColor.DarkRed);
                            break;
                        }
                    }

                    if (player.BuildBlock(player.Coord, block, world.Planets[player.Planet]))
                    {
                        tileMap.SetCell(1, player.Coord, resourceManager.TileIds[selectBlock]);
                    }
                    break;
                case ConsoleKey.E:
                    player.DestroyBlock(player.Coord, world.Planets[player.Planet]);
                    tileMap.RemoveCell(1, player.Coord);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    bool isDown = keyInfo.Key == ConsoleKey.DownArrow;

                    if (categoryOrBlock)
                    {
                        if (isDown) itemListBlockCategory.NextItem(); else itemListBlockCategory.PreviousItem();
                        selectCategory = itemListBlockCategory.SelectedItem;
                        UpdateBlockList(researchSystem, itemListBlockList, selectCategory);
                    }
                    else
                    {
                        if (isDown) itemListBlockList.NextItem(); else itemListBlockList.PreviousItem();
                    }

                    selectBlock = itemListBlockList.SelectedItem;
                    blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
                    UpdateCostBuilding(labelCostBuilding, selectBlock);
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    categoryOrBlock = !categoryOrBlock;
                    if (categoryOrBlock)
                    {
                        itemListBlockCategory.SelectedItemColor = ConsoleColor.DarkBlue;
                        itemListBlockList.SelectedItemColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        itemListBlockCategory.SelectedItemColor = ConsoleColor.Blue;
                        itemListBlockList.SelectedItemColor = ConsoleColor.DarkBlue;
                    }
                    break;
            }
        }

        void HandleKeyPressTABMenu(ConsoleKeyInfo keyInfo)
        {
            void UpdateSelectedItemList()
            {
                itemListPlanetResourcesToPlayer.SelectedItemColor = (TABMenuSelectedItemList == 0) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                itemListResourcesToPlayerCount.SelectedItemColor = (TABMenuSelectedItemList == 1) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                itemListPlayerResourcesToPlanet.SelectedItemColor = (TABMenuSelectedItemList == 2) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
            }

            void HandleNavigation(bool next)
            {
                if (TABMenuSelectedItemList == 0) { if (next) itemListPlanetResourcesToPlayer.NextItem(); else itemListPlanetResourcesToPlayer.PreviousItem(); }
                if (TABMenuSelectedItemList == 1) { if (next) itemListResourcesToPlayerCount.NextItem(); else itemListResourcesToPlayerCount.PreviousItem(); }
                if (TABMenuSelectedItemList == 2) { if (next) itemListPlayerResourcesToPlanet.NextItem(); else itemListPlayerResourcesToPlanet.PreviousItem(); }
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    HandleNavigation(true);
                    break;
                case ConsoleKey.UpArrow:
                    HandleNavigation(false);
                    break;
                case ConsoleKey.RightArrow:
                    TABMenuSelectedItemList = (TABMenuSelectedItemList + 1) % 3;
                    UpdateSelectedItemList();
                    break;
                case ConsoleKey.LeftArrow:
                    TABMenuSelectedItemList = (TABMenuSelectedItemList - 1 + 3) % 3;
                    UpdateSelectedItemList();
                    break;
                case ConsoleKey.E:
                    if (itemListPlanetResourcesToPlayer.Items.Count == 0)
                    {
                        break;
                    }

                    int countplanet = int.Parse(itemListResourcesToPlayerCount.SelectedItem);
                    if (world.Planets[player.Planet].Resources[itemListPlanetResourcesToPlayer.SelectedItem] >= countplanet)
                    {
                        world.Planets[player.Planet].Resources[itemListPlanetResourcesToPlayer.SelectedItem] -= countplanet;
                        player.Resources[itemListPlanetResourcesToPlayer.SelectedItem] = player.Resources.GetValueOrDefault(itemListPlanetResourcesToPlayer.SelectedItem, 0) + countplanet;
                    }
                    break;
                case ConsoleKey.F:
                    if (itemListPlayerResourcesToPlanet.Items.Count == 0)
                    {
                        break;
                    }

                    int countplayer = int.Parse(itemListResourcesToPlayerCount.SelectedItem);
                    if (player.Resources[itemListPlayerResourcesToPlanet.SelectedItem] >= countplayer)
                    {
                        player.Resources[itemListPlayerResourcesToPlanet.SelectedItem] -= countplayer;
                        world.Planets[player.Planet].Resources[itemListPlayerResourcesToPlanet.SelectedItem] = world.Planets[player.Planet].Resources.GetValueOrDefault(itemListPlayerResourcesToPlanet.SelectedItem, 0) + countplayer;
                    }
                    break;
            }
        }

        void HandleKeyPressResearchMenu(ConsoleKeyInfo keyInfo)
        {
            void HandleNavigation(int selectedItemList, bool next)
            {
                if (selectedItemList == 0) { if (next) itemListResearchList.NextItem(); else itemListResearchList.PreviousItem(); UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost); }
                if (selectedItemList == 1) { if (next) itemListPlanetSize.NextItem(); else itemListPlanetSize.PreviousItem(); }
                if (selectedItemList == 2) { if (next) itemListOreName.NextItem(); else itemListOreName.PreviousItem(); itemListOreChance.SelectedItem = oreChance[itemListOreName.SelectedItem].ToString(); }
                if (selectedItemList == 3) { if (next) itemListOreChance.NextItem(); else itemListOreChance.PreviousItem(); oreChance[itemListOreName.SelectedItem] = int.Parse(itemListOreChance.SelectedItem); }
            }

            void UpdateSelectedItemList()
            {
                itemListResearchList.SelectedItemColor = (researchMenuSelectedItemList == 0) ? ConsoleColor.DarkBlue : ConsoleColor.Red;
                itemListPlanetSize.SelectedItemColor = (researchMenuSelectedItemList == 1) ? ConsoleColor.DarkBlue : ConsoleColor.White;
                itemListOreName.SelectedItemColor = (researchMenuSelectedItemList == 2) ? ConsoleColor.DarkBlue : ConsoleColor.White;
                itemListOreChance.SelectedItemColor = (researchMenuSelectedItemList == 3) ? ConsoleColor.DarkBlue : ConsoleColor.White;
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    HandleNavigation(researchMenuSelectedItemList, next: true);
                    break;
                case ConsoleKey.UpArrow:
                    HandleNavigation(researchMenuSelectedItemList, next: false);
                    break;
                case ConsoleKey.RightArrow:
                    researchMenuSelectedItemList = (researchMenuSelectedItemList + 1) % 4;
                    UpdateSelectedItemList();
                    break;
                case ConsoleKey.LeftArrow:
                    researchMenuSelectedItemList = (researchMenuSelectedItemList - 1 + 4) % 4;
                    UpdateSelectedItemList();
                    break;
                case ConsoleKey.Enter:
                    if (researchSystem.CloseResearch.Count > 0)
                    {
                        foreach (var token in researchSystem.CloseResearch[itemListResearchList.SelectedItem].ResearchCost)
                        {
                            if (world.Tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                            {
                                messageMessage.Show("not enough " + token.Key, ConsoleColor.DarkRed);
                            }
                        }
                        if (researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch != null)
                        {
                            if (!researchSystem.OpenResearch.ContainsKey(researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch))
                            {
                                messageMessage.Show("not studied " + researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch, ConsoleColor.DarkRed);
                            }
                        }
                        if (world.StudyResearch(itemListResearchList.SelectedItem))
                        {
                            UpdateResearchList(researchSystem, itemListResearchList);
                            UpdateBlockCategory(researchSystem, itemListBlockCategory);
                            UpdateBlockList(researchSystem, itemListBlockList, selectCategory);
                        }
                    }
                    if (researchSystem.CloseResearch.Count > 0)
                    {
                        UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
                    }
                    else
                    {
                        labelResearchCost.Text = "";
                        labelEnterToResearch.Visible = false;
                        labelResearch.Visible = false;
                    }
                    break;
                case ConsoleKey.G:
                    if (world.SearchPlanet(int.Parse(itemListPlanetSize.SelectedItem), oreChance))
                    {
                        UpdatePlanetList(itemListPlanetList, world);
                    }
                    else
                    {
                        messageMessage.Show("not enough PL", ConsoleColor.DarkRed);
                    }
                    break;
            }
        }

        //UI
        void SetVisibleMap(bool visible)
        {
            tileMap.Visible = visible;
            blockPlayerCoord.Visible = visible;
            labelPlayerCoord.Visible = visible;
        }

        void SetBuildingMode(bool mode)
        {
            buildingMode = mode;
            blockPlayerSelectedBlock.Visible = mode;
            itemListBlockList.Visible = mode;
            itemListBlockCategory.Visible = mode;
            labelCostBuilding.Visible = mode;

            if (tileMap.Visible)
            {
                labelPlayerResources.Visible = true;
            }
        }

        void setVisibleResearchMenu(bool visible)
        {
            researchMenu = visible;
            itemListResearchList.Visible = visible;
            labelTokensInfo.Visible = visible;
            labelSearchPlanet.Visible = visible;
            itemListPlanetSize.Visible = visible;
            labelPlanetSize.Visible = visible;
            labelOreName.Visible = visible;
            itemListOreName.Visible = visible;
            labelOreChance.Visible = visible;
            itemListOreChance.Visible = visible;
            labelCostSearchPlanet.Visible = visible;
            labelPalkaPeredCostSearchPlanet.Visible = visible;
            labelPressGtoSearchPlanet.Visible = visible;

            labelPlayerResources.Visible = !visible;

            if (researchSystem.CloseResearch.Count > 0)
            {
                labelResearchCost.Visible = visible;
                labelEnterToResearch.Visible = visible;
                labelResearch.Visible = visible;
            }
        }

        void SetVisibleTABMenu(bool visible)
        {
            TABmenu = visible;

            labelResourcesToPlayer.Visible = visible;
            itemListPlanetResourcesToPlayer.Visible = visible;
            labelPlayerResourcesToPlanet.Visible = visible;
            itemListPlayerResourcesToPlanet.Visible = visible;
            labelCountResource.Visible = visible;
            labelTransfer.Visible = visible;
            itemListResourcesToPlayerCount.Visible = visible;
            labelPreesEToTransferPlayer.Visible = visible;
            labelPreesFToTransferPlanet.Visible = visible;

            labelPlayerResources.Visible = !visible;
        }

        void SetVisibleTransferBeaconMenu(bool visible)
        {
            labelTransferBeacon.Visible = visible;
            itemListTransferBeaconMenuResourcesList.Visible = visible;
            itemListTransferBeaconMenuCount.Visible = visible;
        }

        private void UpdatePlanetResourcesToPlayer()
        {
            if (itemListPlanetResourcesToPlayer.Items.Count > 0)
            {
                itemListPlanetResourcesToPlayer.Visible = true;
            }
            else
            {
                labelPreesEToTransferPlayer.Visible = false;
            }

            string? selectedItem = null;
            if (itemListPlanetResourcesToPlayer.Items.Count > 0)
            {
                selectedItem = itemListPlanetResourcesToPlayer.SelectedItem;
            }
            itemListPlanetResourcesToPlayer.ClearItems();

            foreach (var resource in world.Planets[player.Planet].Resources)
            {
                itemListPlanetResourcesToPlayer.AddItem(resource.Key);
            }

            if (selectedItem != null)
            {
                itemListPlanetResourcesToPlayer.SelectedItem = selectedItem;
            }
        }

        private void UpdatePlayerResourcesToPlanet()
        {
            if (itemListPlayerResourcesToPlanet.Items.Count > 0)
            {
                labelPreesFToTransferPlanet.Visible = true;
            }
            else
            {
                labelPreesFToTransferPlanet.Visible = false;
            }

            string? selectedItem = null;
            if (itemListPlayerResourcesToPlanet.Items.Count > 0)
            {
                selectedItem = itemListPlayerResourcesToPlanet.SelectedItem;
            }
            itemListPlayerResourcesToPlanet.ClearItems();
            foreach (var resource in player.Resources)
            {
                itemListPlayerResourcesToPlanet.AddItem(resource.Key);
            }
            if (selectedItem != null)
            {
                itemListPlayerResourcesToPlanet.SelectedItem = selectedItem;
            }
        }

        private void UpdateCountTransferResources()
        {
            labelCountResource.Text = world.Planets[player.Planet].Resources.GetValueOrDefault(itemListPlanetResourcesToPlayer.SelectedItem ?? "", 0) + "|" + player.Resources.GetValueOrDefault(itemListPlayerResourcesToPlanet.SelectedItem ?? "", 0);
            labelCountResource.Position = new Point((renderer.ScreenWidth / 2) - (labelCountResource.Width / 2), labelCountResource.Position.Y);
        }

        private void UpdateSpritePlayerCoordBlock()
        {
            if (world.Planets[player.Planet].Ground.ContainsKey(player.Coord))
            {
                if (world.Planets[player.Planet].Blocks.ContainsKey(player.Coord))
                {
                    blockPlayerCoord.Pixels = resourceManager.Blocks[world.Planets[player.Planet].Blocks[player.Coord].Name].Sprite.Pixels;
                }
                else
                {
                    blockPlayerCoord.Pixels = resourceManager.Grounds[world.Planets[player.Planet].Ground[player.Coord].Name].Sprite.Pixels;
                }
            }
            else
            {
                blockPlayerCoord.Pixels = new Pixel[0, 0];

            }
        }

        private void UpdateItemListTransferBeaconMenuResourcesList()
        {
            itemListTransferBeaconMenuResourcesList.ClearItems();
            foreach (var resource in world.Planets[player.Planet].Resources)
            {
                itemListTransferBeaconMenuResourcesList.AddItem(resource.Key);
            }
            itemListTransferBeaconMenuResourcesList.SelectedItem = ((TransferBeaconState)world.Planets[player.Planet].Blocks[player.Coord]).Resource;
        }

        //old method
        private static void loadMap(TileMap tileMap, Planet planet)
        {
            ResourceManager resourceManager = ResourceManager.Instance;

            tileMap.Clear();
            foreach (KeyValuePair<Point, GroundState> pair in planet.Ground)
            {
                tileMap.SetCell(0, pair.Key, resourceManager.TileIds[pair.Value.Name]);
            }
            foreach (KeyValuePair<Point, BlockState> pair in planet.Blocks)
            {
                tileMap.SetCell(1, pair.Key, resourceManager.TileIds[pair.Value.Name]);
            }
        }

        private static void UpdatePlanetInfo(Label planetInfo, Planet planet)
        {
            string text = $"PLANET\n" +
                          $" Name: {planet.Name}\n" +
                          $" Energy: {planet.Energy}\n" +
                          $" Resources\n";
            foreach (var resource in planet.Resources)
            {
                if (resource.Value != 0)
                {
                    text += "  " + resource.Key + ": " + resource.Value + "\n";
                }
            }
            planetInfo.Text = text;
        }

        private static void UpdatePlanetList(ItemList planetList, World world)
        {
            planetList.ClearItems();
            foreach (var planet in world.Planets)
            {
                planetList.AddItem(planet.Name);
            }
        }

        private static void UpdateBlockCategory(ResearchSystem researchSystem, ItemList blockCategory)
        {
            blockCategory.ClearItems();
            SortedSet<string> categoryes = new SortedSet<string>();
            foreach (var research in researchSystem.OpenResearch)
            {
                categoryes.Add(research.Value.Category);
            }
            foreach (var category in categoryes)
            {
                blockCategory.AddItem(category);
            }
        }

        private static void UpdateBlockList(ResearchSystem researchSystem, ItemList blockList, string category)
        {
            blockList.ClearItems();
            foreach (var research in researchSystem.OpenResearch)
            {
                if (research.Value.Category == category)
                {
                    blockList.AddItem(research.Value.Name);
                }
            }
        }

        private static void UpdateResearchList(ResearchSystem researchSystem, ItemList researchList)
        {
            researchList.ClearItems();
            foreach (var research in researchSystem.CloseResearch)
            {
                researchList.AddItem(research.Value.Name);
            }
        }

        private static void UpdateResearchCost(ResearchSystem researchSystem, string name, Label researchCost)
        {
            researchCost.Text = "Cost\n";
            foreach (var token in researchSystem.CloseResearch[name].ResearchCost)
            {
                researchCost.Text += token.Key + ": " + token.Value + "\n";
            }
            if (researchSystem.CloseResearch[name].RequiredResearch != null)
            {
                researchCost.Text += "Required: " + researchSystem.CloseResearch[name].RequiredResearch;
            }
        }

        private static void UpdateTokensInfo(World world, Label tokensInfo)
        {
            tokensInfo.Text = "TOKENS\n";
            foreach (var token in world.Tokens)
            {
                tokensInfo.Text += "  " + token.Key + ": " + token.Value + "\n";
            }
        }

        private static void UpdatePlayerResourcesInfo(Label playerResources, Player player)
        {
            playerResources.Text = "RESOURCES\n";
            foreach (var resource in player.Resources)
            {
                playerResources.Text += resource.Key + ": " + resource.Value + "\n";
            }
        }

        private static void UpdateCostBuilding(Label costBuilding, string name)
        {
            Block block = ResourceManager.Instance.Blocks[name];

            costBuilding.Text = "COST\n";
            foreach (var resource in block.Cost)
            {
                costBuilding.Text += resource.Key + ": " + resource.Value + "\n";
            }
        }
    }
}
