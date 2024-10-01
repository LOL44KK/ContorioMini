using Contorio.Engine;
using Contorio.Engine.Widgets;
using System.Diagnostics;
using System.Drawing;

//НА потом
//Доделать выживание(чтобы тратились рескрсы при стройке)
//
//
//

namespace Contorio
{
    public class Contorio
    {
        private Engine.Engine _engine;
        private Scene _worldScene;

        public Contorio()
        {
            _engine = new Engine.Engine(120, 30);
            _worldScene = new Scene();
            _engine.SetScene(_worldScene);
        }

        public void Run()
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            
            List<Research> tempResearcheList = new List<Research>();
            foreach (var item in resourceManager.Blocks)
            {
                tempResearcheList.Add(item.Value.Research);
            }
            ResearchSystem researchSystem = new ResearchSystem(tempResearcheList);

            World world = new World();
            WorldHandler worldHandler = new WorldHandler(world);
            Player player = new Player();

            bool researchMenu = false;
            bool TAB = false;

            bool buildingMode = true;
            bool categoryOrBlock = true;
            string selectBlock = "drill";
            string selectCategory = "logic";

            bool nameOrCount = true;

            int researchMenuSelectedItemList = 0; //1-4
            //1 Research
            //2 Planet size
            //3 ore
            //4 ore change
            Dictionary<string, int> oreChance = new Dictionary<string, int>();

            double messageTimeAccumulator = 0;

            //Map
            TileMap tileMap = new TileMap(
                66,
                30,
                resourceManager.TileSet,
                new Point(27, 1),
                cellPaddingRight: 2,
                cellPaddingBottom: 1
            );
            tileMap.addLayer(0);
            tileMap.addLayer(1);

            //Player
            Label labelPlayerCoord = new Label("X|Y", ConsoleColor.White, new Point(95, 0));
            Sprite blockPlayerCoord = new Sprite(resourceManager.TileSet.Tiles[0].Pixels, position: new Point(95, 1));
            Label labelPlayerResources = new Label("Resources", ConsoleColor.White, new Point(95, 15));

            //Planets info
            Label labelPlanetInfo = new Label("Planet", ConsoleColor.White, new Point(0, 0));
            Label labelPlanetsLabel = new Label("PLANETS", ConsoleColor.White, new Point(0, 20));
            ItemList itemListPlanetList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(0, 21),
                9
            );

            //Building
            Sprite blockPlayerSelectedBlock = new Sprite(resourceManager.TileSet.Tiles[3].Pixels, position: new Point(102, 1));
            ItemList itemListBlockCategory = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(95, 5),
                9
            );
            ItemList itemListBlockList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(105, 5),
                9
            );
            Label labelCostBuilding = new Label("COST", ConsoleColor.White, new Point(108, 0));

            //Research Menu
            //..Tokens
            Label labelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1), visible: false);

            //..Research
            Label labelResearch = new Label("Research", ConsoleColor.White, new Point(28, 15), visible: false);
            ItemList itemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(28, 16),
                12,
                visible:false
            );
            Label labelResearchCost = new Label("Cost", ConsoleColor.White, new Point(45, 15), visible:false);
            Label labelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(28, 28), visible: false);

            //..Search planet
            Label labelSearchPlanet = new Label("SEARCH PLANET", ConsoleColor.White, new Point(75, 1), visible: false);
            Label labelPlanetSize = new Label("Size: ", ConsoleColor.White, new Point(75, 3), visible: false);
            ItemList itemListPlanetSize = new ItemList(
                ConsoleColor.White,
                ConsoleColor.White,
                new Point(88, 3),
                1,
                visible:false
            );
            for (int i = 16; i < 130; i += 2)
            {
                itemListPlanetSize.AddItem("" + i);
            }
            Label labelOreName = new Label("Ore name: ", ConsoleColor.White, new Point(75, 5), visible: false);
            ItemList itemListOreName = new ItemList(
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
            Label labelOreChance = new Label("Chance: ", ConsoleColor.White, new Point(75, 6), visible: false);
            ItemList itemListOreChance = new ItemList(
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
            Label labelPalkaPeredCostSearchPlanet = new Label("================", ConsoleColor.White, new Point(75, 7), visible: false);
            Label labelCostSearchPlanet = new Label("Cost: 0 PL", ConsoleColor.White, new Point(75, 8), visible:false);
            Label labelPressGtoSearchPlanet = new Label("Press G to search", ConsoleColor.White, new Point(75, 9), visible: false);

            //TAB
            Label labelResourcesToPlayer = new Label("RESOURCES", ConsoleColor.White, new Point(27, 1), visible:false);
            ItemList itemListResourcesToPlayer = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(27, 2),
                15,
                visible: false
            );
            Label labelCountResource = new Label("Count: 0", ConsoleColor.White, new Point(45, 2), visible: false);
            ItemList itemListResourcesToPlayerCount = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(45, 3),
                1,
                visible: false
            );
            for (int i = 1; i < 10000; i *= 2)
            {
                itemListResourcesToPlayerCount.AddItem("" + i);
            }

            //F3
            Label labelFPS = new Label("FPS: 0", ConsoleColor.White, new Point(111, 0), visible: false);

            //Message
            Label labelMessage = new Label("MESSAGE", ConsoleColor.White, new Point(60, 29), visible: false);


            _worldScene.AddSprite(tileMap);

            _worldScene.AddSprite(labelPlayerCoord);
            _worldScene.AddSprite(blockPlayerCoord);
            _worldScene.AddSprite(labelPlayerResources);

            _worldScene.AddSprite(blockPlayerSelectedBlock);
            _worldScene.AddSprite(itemListBlockCategory);
            _worldScene.AddSprite(itemListBlockList);
            _worldScene.AddSprite(labelCostBuilding);

            _worldScene.AddSprite(labelPlanetInfo);
            _worldScene.AddSprite(labelPlanetsLabel);
            _worldScene.AddSprite(itemListPlanetList);

            _worldScene.AddSprite(labelTokensInfo);
            _worldScene.AddSprite(labelResearch);
            _worldScene.AddSprite(itemListResearchList);
            _worldScene.AddSprite(labelResearchCost);
            _worldScene.AddSprite(labelEnterToResearch);
            _worldScene.AddSprite(labelSearchPlanet);
            _worldScene.AddSprite(itemListPlanetSize);
            _worldScene.AddSprite(labelPlanetSize);
            _worldScene.AddSprite(labelOreName);
            _worldScene.AddSprite(itemListOreName);
            _worldScene.AddSprite(labelOreChance);
            _worldScene.AddSprite(itemListOreChance);
            _worldScene.AddSprite(labelCostSearchPlanet);
            _worldScene.AddSprite(labelPalkaPeredCostSearchPlanet);
            _worldScene.AddSprite(labelPressGtoSearchPlanet);

            _worldScene.AddSprite(labelResourcesToPlayer);
            _worldScene.AddSprite(itemListResourcesToPlayer);
            _worldScene.AddSprite(labelCountResource);
            _worldScene.AddSprite(itemListResourcesToPlayerCount);

            _worldScene.AddSprite(labelFPS);

            _worldScene.AddSprite(labelMessage);

            loadMap(tileMap, world.Plantes[player.Planet]);
            UpdatePlanetList(itemListPlanetList, world);
            UpdateBlockCategory(researchSystem, itemListBlockCategory);
            UpdateBlockList(researchSystem, itemListBlockList, itemListBlockCategory.SelectedItem);
            UpdateResearchList(researchSystem, itemListResearchList);
            UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
            player.Coord = new Point(15, 15);

            selectCategory = itemListBlockCategory.SelectedItem;
            selectBlock = itemListBlockList.SelectedItem;
            blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;

            UpdateCostBuilding(labelCostBuilding, selectBlock);


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
                labelPlayerResources.Visible = mode;
                labelCostBuilding.Visible = mode;
            }

            void setVisibleResearchMenu(bool visible)
            {
                SetBuildingMode(false);
                SetVisibleMap(!visible);

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

                if (researchSystem.CloseResearch.Count > 0)
                {
                    labelResearchCost.Visible = visible;
                    labelEnterToResearch.Visible = visible;
                    labelResearch.Visible = visible;
                }
            }

            void SetVisibleTABMenu(bool visible)
            {
                TAB = visible;
                SetBuildingMode(false);
                SetVisibleMap(!visible);

                labelResourcesToPlayer.Visible = visible;
                itemListResourcesToPlayer.Visible = visible;
                labelCountResource.Visible = visible;
                itemListResourcesToPlayerCount.Visible = visible;
            }

            Stopwatch sw = new Stopwatch();
            
            while (true)
            {
                sw.Restart();

                worldHandler.Tick();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.B:
                            SetBuildingMode(!buildingMode);
                            break;
                        case ConsoleKey.R:
                            setVisibleResearchMenu(!researchMenu);
                            break;
                        case ConsoleKey.Tab:
                            SetVisibleTABMenu(!TAB);
                            break;
                        case ConsoleKey.F3:
                            labelFPS.Visible = !labelFPS.Visible;
                            break;
                        case ConsoleKey.W:
                            player.Coord.Y -= 1;
                            break;
                        case ConsoleKey.S:
                            player.Coord.Y += 1;
                            break;
                        case ConsoleKey.A:
                            player.Coord.X -= 1;
                            break;
                        case ConsoleKey.D:
                            player.Coord.X += 1;
                            break;
                    }

                    if (buildingMode)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.Enter:
                                world.Plantes[player.Planet].SetBlock(player.Coord, resourceManager.Blocks[selectBlock]);
                                tileMap.SetCell(1, player.Coord, resourceManager.TileIds[selectBlock]);
                                break;
                            case ConsoleKey.E:
                                world.Plantes[player.Planet].RemoveBlock(player.Coord);
                                tileMap.RemoveCell(1, player.Coord);
                                break;
                            case ConsoleKey.DownArrow:
                                if (categoryOrBlock)
                                {
                                    itemListBlockCategory.NextItem();
                                    selectCategory = itemListBlockCategory.SelectedItem;
                                    UpdateBlockList(researchSystem, itemListBlockList, selectCategory);
                                }
                                else
                                {
                                    itemListBlockList.NextItem();
                                }
                                selectBlock = itemListBlockList.SelectedItem;
                                blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
                                UpdateCostBuilding(labelCostBuilding, selectBlock);
                                break;
                            case ConsoleKey.UpArrow:
                                if (categoryOrBlock)
                                {
                                    itemListBlockCategory.PreviousItem();
                                    selectCategory = itemListBlockCategory.SelectedItem;
                                    UpdateBlockList(researchSystem, itemListBlockList, selectCategory);
                                }
                                else
                                {
                                    itemListBlockList.PreviousItem();
                                }
                                selectBlock = itemListBlockList.SelectedItem;
                                blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
                                UpdateCostBuilding(labelCostBuilding, selectBlock);
                                break;
                            case ConsoleKey.LeftArrow:
                                categoryOrBlock = !categoryOrBlock;
                                if (categoryOrBlock)
                                {
                                    itemListBlockCategory.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListBlockList.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                itemListBlockCategory.SelectedItemColor = ConsoleColor.Blue;
                                itemListBlockList.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                            case ConsoleKey.RightArrow:
                                categoryOrBlock = !categoryOrBlock;
                                if (categoryOrBlock)
                                {
                                    itemListBlockCategory.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListBlockList.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                itemListBlockCategory.SelectedItemColor = ConsoleColor.Blue;
                                itemListBlockList.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                        }
                    }

                    if (!buildingMode && !researchMenu && !TAB)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                itemListPlanetList.NextItem();
                                player.Planet = itemListPlanetList.SelectedIndex;
                                player.Coord = new Point(15, 15);
                                loadMap(tileMap, world.Plantes[player.Planet]);
                                UpdatePlanetInfo(labelPlanetInfo, world.Plantes[player.Planet]);
                                break;
                            case ConsoleKey.UpArrow:
                                itemListPlanetList.PreviousItem();
                                player.Planet = itemListPlanetList.SelectedIndex;
                                player.Coord = new Point(15, 15);
                                loadMap(tileMap, world.Plantes[player.Planet]);
                                UpdatePlanetInfo(labelPlanetInfo, world.Plantes[player.Planet]);
                                break;
                        }
                    }

                    //TAB
                    if (TAB && !researchMenu && !buildingMode)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (nameOrCount)
                                {
                                    itemListResourcesToPlayer.NextItem();
                                    break;
                                }
                                itemListResourcesToPlayerCount.NextItem();
                                break;
                            case ConsoleKey.UpArrow:
                                if (nameOrCount)
                                {
                                    itemListResourcesToPlayer.PreviousItem();
                                    break;
                                }
                                itemListResourcesToPlayerCount.PreviousItem();
                                break;
                            case ConsoleKey.RightArrow:
                                nameOrCount = !nameOrCount;
                                if (nameOrCount)
                                {
                                    itemListResourcesToPlayer.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListResourcesToPlayerCount.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                itemListResourcesToPlayer.SelectedItemColor = ConsoleColor.Blue;
                                itemListResourcesToPlayerCount.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                            case ConsoleKey.LeftArrow:
                                nameOrCount = !nameOrCount;
                                if (nameOrCount)
                                {
                                    itemListResourcesToPlayer.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListResourcesToPlayerCount.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                itemListResourcesToPlayer.SelectedItemColor = ConsoleColor.Blue;
                                itemListResourcesToPlayerCount.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                        }
                    }

                    if (researchMenu && !TAB)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (researchMenuSelectedItemList == 0)
                                {
                                    itemListResearchList.NextItem();
                                    UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
                                }
                                if (researchMenuSelectedItemList == 1)
                                {
                                    itemListPlanetSize.NextItem();
                                }
                                if (researchMenuSelectedItemList == 2)
                                {
                                    itemListOreName.NextItem();
                                    itemListOreChance.SelectedItem = oreChance[itemListOreName.SelectedItem].ToString();
                                }
                                if (researchMenuSelectedItemList == 3)
                                {
                                    itemListOreChance.NextItem();
                                    oreChance[itemListOreName.SelectedItem] = int.Parse(itemListOreChance.SelectedItem);
                                }
                                break;
                            case ConsoleKey.UpArrow:
                                if (researchMenuSelectedItemList == 0)
                                {
                                    itemListResearchList.PreviousItem();
                                    UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
                                }
                                if (researchMenuSelectedItemList == 1)
                                {
                                    itemListPlanetSize.PreviousItem();
                                }
                                if (researchMenuSelectedItemList == 2)
                                {
                                    itemListOreName.PreviousItem();
                                    itemListOreChance.SelectedItem = oreChance[itemListOreName.SelectedItem].ToString();
                                }
                                if (researchMenuSelectedItemList == 3)
                                {
                                    itemListOreChance.PreviousItem();
                                    oreChance[itemListOreName.SelectedItem] = int.Parse(itemListOreChance.SelectedItem);
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                researchMenuSelectedItemList = (researchMenuSelectedItemList + 1) % 4;
                                if (researchMenuSelectedItemList == 0)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                }
                                if (researchMenuSelectedItemList == 1)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                }
                                if (researchMenuSelectedItemList == 2)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.DarkBlue;
                                }
                                if (researchMenuSelectedItemList == 3)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.DarkBlue;
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                researchMenuSelectedItemList = (researchMenuSelectedItemList - 1 + 4) % 4;
                                if (researchMenuSelectedItemList == 0)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                }
                                if (researchMenuSelectedItemList == 1)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                }
                                if (researchMenuSelectedItemList == 2)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.DarkBlue;
                                }
                                if (researchMenuSelectedItemList == 3)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                    itemListOreName.SelectedItemColor = ConsoleColor.White;
                                    itemListOreChance.SelectedItemColor = ConsoleColor.DarkBlue;
                                }
                                break;
                            case ConsoleKey.Enter:
                                if (researchSystem.CloseResearch.Count > 0)
                                {
                                    bool ok = true;
                                    foreach (var token in researchSystem.CloseResearch[itemListResearchList.SelectedItem].ResearchCost)
                                    {
                                        if (world.Tokens.GetValueOrDefault(token.Key, 0) < token.Value)
                                        {
                                            ok = false;
                                            ShowMessage(labelMessage, ref messageTimeAccumulator, "not enough " + token.Key, ConsoleColor.DarkRed);
                                        }
                                    }
                                    if (researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch != null)
                                    {
                                        if (!researchSystem.OpenResearch.ContainsKey(researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch))
                                        {
                                            ok = false;
                                            ShowMessage(labelMessage, ref messageTimeAccumulator, "not studied " + researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch, ConsoleColor.DarkRed);
                                        }
                                    }
                                    if (ok)
                                    {
                                        foreach (var token in researchSystem.CloseResearch[itemListResearchList.SelectedItem].ResearchCost)
                                        {
                                            world.Tokens[token.Key] -= token.Value;
                                        }
                                        researchSystem.UnlockResearch(itemListResearchList.SelectedItem);
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
                                int cost = CalculateCostSearchPlanet(int.Parse(itemListPlanetSize.SelectedItem), oreChance);
                                if (world.Tokens.GetValueOrDefault("PL", 0) >= cost)
                                {
                                    world.Plantes.Add(new Planet(int.Parse(itemListPlanetSize.SelectedItem), oreChance));
                                    UpdatePlanetList(itemListPlanetList, world);
                                    world.Tokens["PL"] -= cost;
                                }
                                else
                                {
                                    ShowMessage(labelMessage, ref messageTimeAccumulator, "not enough PL", ConsoleColor.DarkRed);
                                }
                                break;
                        }
                    }
                }

                tileMap.UpdatePixels(player.Coord);
                labelPlayerCoord.Text = player.Coord.X + "|" + player.Coord.Y;
                UpdatePlanetInfo(labelPlanetInfo, world.Plantes[player.Planet]);
                UpdatePlayerResourcesInfo(labelPlayerResources, player);
                
                if (world.Plantes[player.Planet].Ground.ContainsKey(player.Coord))
                {
                    if (world.Plantes[player.Planet].Blocks.ContainsKey(player.Coord))
                    {
                        blockPlayerCoord.Pixels = resourceManager.Blocks[world.Plantes[player.Planet].Blocks[player.Coord].Name].Sprite.Pixels;
                    }
                    else
                    {
                        blockPlayerCoord.Pixels = resourceManager.Grounds[world.Plantes[player.Planet].Ground[player.Coord].Name].Sprite.Pixels;
                    }
                }

                if (researchMenu)
                {
                    UpdateTokensInfo(world, labelTokensInfo);
                    labelCostSearchPlanet.Text = "cost: " + CalculateCostSearchPlanet(int.Parse(itemListPlanetSize.SelectedItem), oreChance) + " PL";
                }

                if (TAB)
                {
                    UpdateResourcesToPlayer(itemListResourcesToPlayer, world.Plantes[player.Planet]);
                    if (itemListResourcesToPlayer.Items.Count > 0)
                    {
                        labelCountResource.Text = "Count: " + world.Plantes[player.Planet].Resources[itemListResourcesToPlayer.SelectedItem];
                    }
                }

                if (labelMessage.Visible)
                {
                    messageTimeAccumulator += sw.Elapsed.TotalMilliseconds;
                    if (messageTimeAccumulator > 1000)
                    {
                        messageTimeAccumulator = 0;
                        labelMessage.Visible = false;
                    }
                }

                _engine.Render();

                labelFPS.Text = "FPS: " + ((int)(1000 / sw.Elapsed.TotalMilliseconds)).ToString();
            }
        }

        static void loadMap(TileMap tileMap, Planet planet)
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

        static void ShowMessage(Label message, ref double messageTimeAccumulator, string text, ConsoleColor color)
        {
            message.Text = text;
            message.TextColor = color;
            message.Position = new Point((120 / 2) - (text.Length / 2), 29);
            message.Visible = true;
            messageTimeAccumulator = 0;
        }

        static int CalculateCostSearchPlanet(int planetSize, Dictionary<string, int> oreChance)
        {
            int cost = 0;
            foreach (var ore in oreChance)
            {
                cost += ore.Value * planetSize;
            }
            return cost;
        }

        static void UpdatePlanetInfo(Label planetInfo, Planet planet)
        {
            string text = $"PLANET\n" +
                          $" Name: {planet.Name}\n" +
                          $" Energy: {planet.Energy}\n" +
                          $" Resources\n";
            foreach (var resource in planet.Resources)
            {
                text += "  "+ resource.Key + ": " + resource.Value + "\n";
            }
            planetInfo.Text = text;
        }

        static void UpdatePlanetList(ItemList planetList, World world)
        {
            planetList.ClearItems();
            foreach (var planet in world.Plantes)
            {
                planetList.AddItem(planet.Name);
            }
        }

        static void UpdateBlockCategory(ResearchSystem researchSystem, ItemList blockCategory)
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

        static void UpdateBlockList(ResearchSystem researchSystem, ItemList blockList, string category)
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

        static void UpdateResearchList(ResearchSystem researchSystem, ItemList researchList)
        {
            researchList.ClearItems();
            foreach (var research in researchSystem.CloseResearch)
            {
                researchList.AddItem(research.Value.Name);
            }
        }

        static void UpdateResearchCost(ResearchSystem researchSystem, string name, Label researchCost)
        {
            researchCost.Text = "Cost\n";
            foreach(var token in researchSystem.CloseResearch[name].ResearchCost)
            {
                researchCost.Text += token.Key + ": " + token.Value + "\n";
            }
            if (researchSystem.CloseResearch[name].RequiredResearch != null)
            {
                researchCost.Text += "Required: " + researchSystem.CloseResearch[name].RequiredResearch;
            }
        }

        static void UpdateTokensInfo(World world, Label tokensInfo)
        {
            tokensInfo.Text = "TOKENS\n";
            foreach (var token in world.Tokens)
            {
                tokensInfo.Text += "  " + token.Key + ": " + token.Value + "\n";
            }
        }

        static void UpdatePlayerResourcesInfo(Label playerResources, Player player)
        {
            playerResources.Text = "RESOURCES\n";
            foreach( var resource in player.Resources)
            {
                playerResources.Text += resource.Key + ": " + resource.Value + "\n";
            }
        }

        static void UpdateCostBuilding(Label costBuilding, string name)
        {
            Block block = ResourceManager.Instance.Blocks[name];

            costBuilding.Text = "COST\n";
            foreach (var resource in block.Cost)
            {
                costBuilding.Text += resource.Key + ": " + resource.Value + "\n";
            }
        }

        static void UpdateResourcesToPlayer(ItemList resourcesToPlayer, Planet planet)
        {
            string? selectedItem = null;
            if (resourcesToPlayer.Items.Count > 0)
            {
                selectedItem = resourcesToPlayer.SelectedItem;
            }
            resourcesToPlayer.ClearItems();

            foreach (var resource in planet.Resources)
            {
                resourcesToPlayer.AddItem(resource.Key);
            }

            if (selectedItem != null)
            {
                resourcesToPlayer.SelectedItem = selectedItem;
            }
        }
    }
}
