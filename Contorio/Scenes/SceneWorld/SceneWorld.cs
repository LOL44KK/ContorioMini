﻿using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneWorld : Scene
    {
        private Engine _engine;

        private World _world;
        private Player _player;
        private WorldHandler _worldHandler;

        public SceneTileMap SceneTileMap;
        public ScenePlanetInfo ScenePlanetInfo;
        public SceneBuilding SceneBuilding;
        public SceneBuildingUI SceneBuildingUI;
        public SceneTransfer SceneTransfer;
        public SceneBlockUI SceneBlockUI;
        public SceneTokensMenu.SceneTokensMenu SceneTokensMenu;
        public SceneDebugUI SceneDebugUI;

        public Message MessageMessage;

        public SceneWorld(Engine engine, World world)
        {
            _engine = engine;

            _world = world;
            _player = _world.Player;
            _worldHandler = new WorldHandler(_world);

            // InitializeWidgets
            MessageMessage = new Message(new Point(60, 29));

            // InitializeScene
            SceneTileMap = new SceneTileMap(_world);
            ScenePlanetInfo = new ScenePlanetInfo(_world);
            SceneBuilding = new SceneBuilding(this, _world);
            SceneBuildingUI = new SceneBuildingUI(this, _world);
            SceneTransfer = new SceneTransfer(_world);
            SceneBlockUI = new SceneBlockUI(_world);
            SceneTokensMenu = new SceneTokensMenu.SceneTokensMenu(this, _world);
            SceneDebugUI = new SceneDebugUI(_engine, _world);

            // AddSprite
            AddSprite(MessageMessage);

            // IncludeScene
            IncludeScene(SceneTileMap);
            IncludeScene(ScenePlanetInfo);
            IncludeScene(SceneBuilding);
            IncludeScene(SceneBuildingUI);
            IncludeScene(SceneTokensMenu);
            IncludeScene(SceneTransfer);
            IncludeScene(SceneBlockUI);
            IncludeScene(SceneDebugUI);

            // OnTick
            OnTick += _worldHandler.Tick;
        }

        public override void Ready()
        {
            ScenePlanetInfo.Enable = true;
            SceneTileMap.Enable = true;

            SceneBuilding.Enable = false;
            SceneBuildingUI.Enable = false;
            SceneTokensMenu.Enable = false;
            SceneTransfer.Enable = false;
            SceneBlockUI.Enable = false;
            SceneDebugUI.Enable = false;
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    if (SceneTileMap.Enable)
                    {
                        _engine.Quit();
                        break;
                    }

                    SceneTileMap.Enable = true;
                    ScenePlanetInfo.Enable = true;

                    SetBuildingMode(false);
                    SceneTokensMenu.Enable = false;
                    SceneTransfer.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.R:
                    SceneTokensMenu.Enable = !SceneTokensMenu.Enable;
                    
                    SceneTileMap.Enable  = !SceneTokensMenu.Enable;
                    SetBuildingMode(false);
                    SceneTransfer.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.T:
                    SceneTransfer.Enable = !SceneTransfer.Enable;

                    SceneTileMap.Enable = !SceneTransfer.Enable;
                    SetBuildingMode(false);
                    SceneTokensMenu.Enable = false;
                    SceneBlockUI.Enable  = false;
                    break;

                case ConsoleKey.B:
                    if (SceneTileMap.Enable)
                    {
                        SetBuildingMode(!SceneBuilding.Enable);
                        SceneDebugUI.Enable = false;
                    }
                    break;

                case ConsoleKey.F3:
                    SceneDebugUI.Enable = !SceneDebugUI.Enable;
                    SetBuildingMode(false);
                    break;
                
                case ConsoleKey.Enter:
                    if (SceneBlockUI.Enable)
                    {
                        SceneBlockUI.Enable = false;

                        SceneTileMap.Enable = true;
                    }
                    else if (SceneTileMap.Enable && !SceneBuilding.Enable && SceneBlockUI.OpenUI(_player.Coord))
                    {
                        SceneTileMap.Enable  = false;
                        SceneTransfer.Enable = false;
                        SetBuildingMode(false);
                        SceneTokensMenu.Enable = false;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (SceneTileMap.Enable && !SceneBuilding.Enable)
                    {
                        ScenePlanetInfo.ItemListPlanetList.NextItem();
                        SwitchPlanet(ScenePlanetInfo.ItemListPlanetList.SelectedIndex);
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (SceneTileMap.Enable && !SceneBuilding.Enable)
                    {
                        ScenePlanetInfo.ItemListPlanetList.PreviousItem();
                        SwitchPlanet(ScenePlanetInfo.ItemListPlanetList.SelectedIndex);
                    }
                    break;

                case ConsoleKey.H:
                    SaveWorld();
                    MessageMessage.Show("Successfully saved", ConsoleColor.DarkGreen);
                    break;

                case ConsoleKey.G:
                    _player.GodMode = !_player.GodMode;
                    break;
            }
        }

        private void SwitchPlanet(int planetIndex)
        {
            _player.Planet = planetIndex;
            SceneTileMap.LoadMap(_world.Planets[planetIndex]);
            _player.Coord = new Point(_world.Planets[planetIndex].Size / 2, _world.Planets[planetIndex].Size / 2);
        }

        private void SaveWorld()
        {
            SaveManager.SaveWorld(_world.Planets[0].Name + ".ctsave", _world);
        }

        private void SetBuildingMode(bool mode)
        {
            SceneBuilding.Enable = mode;
            SceneBuildingUI.Enable = mode;
        }
    }
}
