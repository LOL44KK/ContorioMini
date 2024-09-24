﻿using Contorio.Engine;
using Contorio.Engine.Widgets;
using System.Diagnostics;
using System.Drawing;

//НА ЗАВТРА
//Planet generator
//planet size 16-128
//resources spawn change
//price token this planet

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

            bool TAB = false;

            bool buildingMode = true;
            bool categoryOrBlock = true;
            string selectBlock = "drill";
            string selectCategory = "logic";

            bool researchOrSearchPlanet = true;

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
            Label labelPlayerCoord = new Label("X|Y", ConsoleColor.White, new Point(95, 0));
            Sprite blockPlayerCoord = new Sprite(resourceManager.TileSet.Tiles[0].Pixels, position: new Point(95, 1));

            //Planets info
            Label labelPlanetInfo = new Label(CreatePlanetInfo(world.Plantes[player.Planet]), ConsoleColor.White, new Point(0, 0));
            Label labelPlanetsLabel = new Label("PLANETS", ConsoleColor.White, new Point(0, 20));
            ItemList itemListPlanetList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(0, 21),
                9
            );

            //Building
            Sprite blockPlayerSelectedBlock = new Sprite(resourceManager.TileSet.Tiles[3].Pixels, position: new Point(102, 1));
            ItemList ItemListBlockCategory = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(95, 5),
                10
            );
            ItemList ItemListBlockList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.Blue,
                new Point(105, 5),
                10
            );

            //TAB
            //..Tokens
            Label labelTokensInfo = new Label("Tokens", ConsoleColor.White, new Point(28, 1), visible: false);

            //..Research
            Label labelResearch = new Label("Research", ConsoleColor.White, new Point(28, 15), visible: false);
            ItemList itemListResearchList = new ItemList(
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                new Point(28, 16),
                13,
                visible:false
            );
            Label labelResearchCost = new Label("Cost", ConsoleColor.White, new Point(45, 15), visible:false);
            Label labelEnterToResearch = new Label("Enter to research", ConsoleColor.White, new Point(28, 29), visible: false);

            //..Search planet
            Label labelSearchPlanet = new Label("Search planet", ConsoleColor.White, new Point(75, 1), visible: false);
            Label labelPlanetSize = new Label("Planet size: ", ConsoleColor.White, new Point(75, 3), visible: false);
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
            foreach (var ore in resourceManager.Grounds)
            {
                if (ore.Value.Type == GroundType.ORE)
                {
                    itemListOreName.AddItem(ore.Key);
                }
            }
            Label labelOreChance = new Label("chance: ", ConsoleColor.White, new Point(75, 6), visible: false);


            //F3
            Label labelFPS = new Label("FPS: 0", ConsoleColor.White, new Point(111, 0), visible: false);

            _worldScene.AddSprite(tileMap);
            _worldScene.AddSprite(labelPlayerCoord);
            _worldScene.AddSprite(blockPlayerCoord);

            _worldScene.AddSprite(blockPlayerSelectedBlock);
            _worldScene.AddSprite(ItemListBlockCategory);
            _worldScene.AddSprite(ItemListBlockList);

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

            _worldScene.AddSprite(labelFPS);

            loadMap(tileMap, world.Plantes[player.Planet]);
            UpdatePlanetList(itemListPlanetList, world);
            UpdateBlockCategory(researchSystem, ItemListBlockCategory);
            UpdateBlockList(researchSystem, ItemListBlockList, ItemListBlockCategory.SelectedItem);
            UpdateResearchList(researchSystem, itemListResearchList);
            UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
            player.Coord = new Point(15, 15);

            selectCategory = ItemListBlockCategory.SelectedItem;
            selectBlock = ItemListBlockList.SelectedItem;
            blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;

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
                            buildingMode = !buildingMode;
                            blockPlayerSelectedBlock.Visible = buildingMode;
                            ItemListBlockList.Visible = buildingMode;
                            ItemListBlockCategory.Visible = buildingMode;
                            break;
                        case ConsoleKey.Tab:
                            TAB = !TAB;
                            itemListResearchList.Visible = TAB;
                            labelTokensInfo.Visible = TAB;
                            labelSearchPlanet.Visible = TAB;
                            itemListPlanetSize.Visible = TAB;
                            labelPlanetSize.Visible = TAB;
                            labelOreName.Visible = TAB;
                            itemListOreName.Visible = TAB;
                            labelOreChance.Visible = TAB;

                            if (researchSystem.CloseResearch.Count > 0)
                            {
                                labelResearchCost.Visible = TAB;
                                labelEnterToResearch.Visible = TAB;
                                labelResearch.Visible = TAB;
                            }

                            tileMap.Visible = !TAB;
                            blockPlayerCoord.Visible = !TAB;
                            labelPlayerCoord.Visible = !TAB;

                            buildingMode = false;
                            blockPlayerSelectedBlock.Visible = false;
                            ItemListBlockList.Visible = false;
                            ItemListBlockCategory.Visible = false;
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
                                    ItemListBlockCategory.NextItem();
                                    selectCategory = ItemListBlockCategory.SelectedItem;
                                    UpdateBlockList(researchSystem, ItemListBlockList, selectCategory);
                                }
                                else
                                {
                                    ItemListBlockList.NextItem();
                                }
                                selectBlock = ItemListBlockList.SelectedItem;
                                blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
                                break;
                            case ConsoleKey.UpArrow:
                                if (categoryOrBlock)
                                {
                                    ItemListBlockCategory.PreviousItem();
                                    selectCategory = ItemListBlockCategory.SelectedItem;
                                    UpdateBlockList(researchSystem, ItemListBlockList, selectCategory);
                                }
                                else
                                {
                                    ItemListBlockList.PreviousItem();
                                }
                                selectBlock = ItemListBlockList.SelectedItem;
                                blockPlayerSelectedBlock.Pixels = resourceManager.Blocks[selectBlock].Sprite.Pixels;
                                break;
                            case ConsoleKey.LeftArrow:
                                categoryOrBlock = !categoryOrBlock;
                                if (categoryOrBlock)
                                {
                                    ItemListBlockCategory.SelectedItemColor = ConsoleColor.DarkBlue;
                                    ItemListBlockList.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                ItemListBlockCategory.SelectedItemColor = ConsoleColor.Blue;
                                ItemListBlockList.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                            case ConsoleKey.RightArrow:
                                categoryOrBlock = !categoryOrBlock;
                                if (categoryOrBlock)
                                {
                                    ItemListBlockCategory.SelectedItemColor = ConsoleColor.DarkBlue;
                                    ItemListBlockList.SelectedItemColor = ConsoleColor.Blue;
                                    break;
                                }
                                ItemListBlockCategory.SelectedItemColor = ConsoleColor.Blue;
                                ItemListBlockList.SelectedItemColor = ConsoleColor.DarkBlue;
                                break;
                        }
                    }

                    if (!buildingMode && !TAB)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                itemListPlanetList.NextItem();
                                player.Planet = itemListPlanetList.SelectedIndex;
                                player.Coord = new Point(15, 15);
                                loadMap(tileMap, world.Plantes[player.Planet]);
                                labelPlanetInfo.Text = CreatePlanetInfo(world.Plantes[player.Planet]);
                                break;
                            case ConsoleKey.UpArrow:
                                itemListPlanetList.PreviousItem();
                                player.Planet = itemListPlanetList.SelectedIndex;
                                player.Coord = new Point(15, 15);
                                loadMap(tileMap, world.Plantes[player.Planet]);
                                labelPlanetInfo.Text = CreatePlanetInfo(world.Plantes[player.Planet]);
                                break;
                        }
                    }

                    if (TAB)
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (researchOrSearchPlanet)
                                {
                                    itemListResearchList.NextItem();
                                    UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
                                }
                                else
                                {
                                    itemListPlanetSize.NextItem();
                                }
                                break;
                            case ConsoleKey.UpArrow:
                                if (researchOrSearchPlanet)
                                {
                                    itemListResearchList.PreviousItem();
                                    UpdateResearchCost(researchSystem, itemListResearchList.SelectedItem, labelResearchCost);
                                }
                                else
                                {
                                    itemListPlanetSize.PreviousItem();
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                researchOrSearchPlanet = !researchOrSearchPlanet;
                                if (researchOrSearchPlanet)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                }
                                else
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.DarkBlue;
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                researchOrSearchPlanet = !researchOrSearchPlanet;
                                if (researchOrSearchPlanet)
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.DarkBlue;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.White;
                                }
                                else
                                {
                                    itemListResearchList.SelectedItemColor = ConsoleColor.Red;
                                    itemListPlanetSize.SelectedItemColor = ConsoleColor.DarkBlue;
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
                                        }
                                    }
                                    if (researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch != null)
                                    {
                                        if (!researchSystem.OpenResearch.ContainsKey(researchSystem.CloseResearch[itemListResearchList.SelectedItem].RequiredResearch))
                                        {
                                            ok = false;
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
                                        UpdateBlockCategory(researchSystem, ItemListBlockCategory);
                                        UpdateBlockList(researchSystem, ItemListBlockList, selectCategory);
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
                        }
                    }
                }

                tileMap.UpdatePixels(player.Coord);
                labelPlayerCoord.Text = player.Coord.X + "|" + player.Coord.Y;
                labelPlanetInfo.Text = CreatePlanetInfo(world.Plantes[player.Planet]);
                
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

                if (TAB)
                {
                    UpdateTokensInfo(world, labelTokensInfo);
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

        static string CreatePlanetInfo(Planet planet)
        {
            string text = $"PLANET\n" +
                          $" Name: {planet.Name}\n" +
                          $" Energy: {planet.Energy}\n" +
                          $" Resources\n";
            foreach (var resource in planet.Resources)
            {
                text += "  "+ resource.Key + ": " + resource.Value + "\n";
            }
            return text;
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
            tokensInfo.Text = "Tokens\n";
            foreach (var token in world.Tokens)
            {
                tokensInfo.Text += "  " + token.Key + ": " + token.Value + "\n";
            }
        }
    }
}
