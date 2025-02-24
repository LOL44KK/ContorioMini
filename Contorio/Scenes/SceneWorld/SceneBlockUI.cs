﻿using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using CharEngine.Containers;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Scenes.SceneWorld
{
    // При добавление нового блока разделить на под сцены
    // SceneTransferBeacon
    // SceneNewBlock
    // и так далее...
    public class SceneBlockUI : Scene
    {
        private World _world;
        private Player _player;

        // TransferBeacon
        public Label LabelTransferBeaconMenu;
        public Label LabelTransferBeaconMenuPlanet;
        public ItemList ItemListTransferBeaconMenuPlanetList;
        public Label LabelTransferBeaconMenuResource;
        public ItemList ItemListTransferBeaconMenuResourcesList;
        public Label LabelTransferBeaconMenuCount;
        public ItemList ItemListTransferBeaconMenuCount;
        public Label LabelTransferBeaconMenuPreesEnterToBack;

        public Container ContainerTransferBeacon;
        public ItemListContainer ItemListContainerTransferBeacon;

        public SceneBlockUI(World world)
        {
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            LabelTransferBeaconMenu = new Label("Transfer Beacon MENU", ConsoleColor.White, new Point(60, 7));
            LabelTransferBeaconMenu.Position = new Point((120 / 2) - (LabelTransferBeaconMenu.Width / 2), LabelTransferBeaconMenu.Position.Y);
            LabelTransferBeaconMenuPlanet = new Label("planet: ", ConsoleColor.White, new Point(50, 8));
            ItemListTransferBeaconMenuPlanetList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(60, 8),
                1
            );
            LabelTransferBeaconMenuResource = new Label("resource: ", ConsoleColor.White, new Point(50, 9));
            ItemListTransferBeaconMenuResourcesList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(60, 9),
                1
            );
            LabelTransferBeaconMenuCount = new Label("count: ", ConsoleColor.White, new Point(50, 10));
            ItemListTransferBeaconMenuCount = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(60, 10),
                1
            );
            LabelTransferBeaconMenuPreesEnterToBack = new Label("press Enter to back", ConsoleColor.White, new Point(60, 11));
            LabelTransferBeaconMenuPreesEnterToBack.Position = new Point((120 / 2) - (LabelTransferBeaconMenuPreesEnterToBack.Width / 2), LabelTransferBeaconMenuPreesEnterToBack.Position.Y);
            
            // ContainerTransferBeacon
            ContainerTransferBeacon = new Container();
            ContainerTransferBeacon.AddSprite(LabelTransferBeaconMenu);
            ContainerTransferBeacon.AddSprite(LabelTransferBeaconMenuPlanet);
            ContainerTransferBeacon.AddSprite(ItemListTransferBeaconMenuPlanetList);
            ContainerTransferBeacon.AddSprite(LabelTransferBeaconMenuResource);
            ContainerTransferBeacon.AddSprite(ItemListTransferBeaconMenuResourcesList);
            ContainerTransferBeacon.AddSprite(LabelTransferBeaconMenuCount);
            ContainerTransferBeacon.AddSprite(ItemListTransferBeaconMenuCount);
            ContainerTransferBeacon.AddSprite(LabelTransferBeaconMenuPreesEnterToBack);

            // ItemListContainerTransferBeacon
            ItemListContainerTransferBeacon = new ItemListContainer();
            ItemListContainerTransferBeacon.AddItemList(ItemListTransferBeaconMenuPlanetList);
            ItemListContainerTransferBeacon.AddItemList(ItemListTransferBeaconMenuResourcesList);
            ItemListContainerTransferBeacon.AddItemList(ItemListTransferBeaconMenuCount);

            AddSprite(LabelTransferBeaconMenu);
            AddSprite(LabelTransferBeaconMenuPlanet);
            AddSprite(ItemListTransferBeaconMenuPlanetList);
            AddSprite(LabelTransferBeaconMenuResource);
            AddSprite(ItemListTransferBeaconMenuResourcesList);
            AddSprite(LabelTransferBeaconMenuCount);
            AddSprite(ItemListTransferBeaconMenuCount);
            AddSprite(LabelTransferBeaconMenuPreesEnterToBack);
        }

        public override void Ready()
        {
            ContainerTransferBeacon.Visible = false;
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (ContainerTransferBeacon.Visible)
                    {
                        ItemListContainerTransferBeacon.PreviousItemList();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (ContainerTransferBeacon.Visible)
                    {
                        ItemListContainerTransferBeacon.NextItemList();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (ContainerTransferBeacon.Visible)
                    {
                        ItemListContainerTransferBeacon.PreviousItem();
                        SaveDataTransferBeaconState();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (ContainerTransferBeacon.Visible)
                    {
                        ItemListContainerTransferBeacon.NextItem();
                        SaveDataTransferBeaconState();
                    }
                    break;
            }
        }

        public bool OpenUI(Point coord)
        {
            BlockState? blockState;
            if (_world.Planets[_player.Planet].Blocks.TryGetValue(coord, out blockState))
            {
                BlockType type = ResourceManager.Instance.Blocks[blockState.Name].Type;
                switch (type)
                {
                    case BlockType.TRANSFER_BEACON:
                        Enable = true;
                        ContainerTransferBeacon.Visible = true;
                        UpdateItemListTransferBeaconMenuResourcesList();
                        UpdateItemListTransferBeaconMenuCount();
                        UpdateItemListTransferBeaconMenuPlanetList();
                        return true;
                }
            }
            return false;
        }

        private void SaveDataTransferBeaconState()
        {
            TransferBeaconState transferBeaconState = (TransferBeaconState)_world.Planets[_player.Planet].Blocks[_player.Coord];
            transferBeaconState.Planet = ItemListTransferBeaconMenuPlanetList.SelectedIndex;
            transferBeaconState.Resource = ItemListTransferBeaconMenuResourcesList.SelectedItem;
            transferBeaconState.Count = int.Parse(ItemListTransferBeaconMenuCount.SelectedItem);
        }

        private void UpdateItemListTransferBeaconMenuResourcesList()
        {
            ItemListTransferBeaconMenuResourcesList.ClearItems();
            foreach (var resource in _world.Planets[_player.Planet].Resources)
            {
                ItemListTransferBeaconMenuResourcesList.AddItem(resource.Key);
            }
            ItemListTransferBeaconMenuResourcesList.SelectedItem = ((TransferBeaconState)_world.Planets[_player.Planet].Blocks[_player.Coord]).Resource;
        }

        private void UpdateItemListTransferBeaconMenuCount()
        {
            ItemListTransferBeaconMenuCount.ClearItems();
            ItemListTransferBeaconMenuCount.AddItem("" + 0);
            for (int i = 2; i < ((TransferBeacon)ResourceManager.Instance.Blocks[_world.Planets[_player.Planet].Blocks[_player.Coord].Name]).MaxTransferableCount; i *= 2)
            {
                ItemListTransferBeaconMenuCount.AddItem("" + i);
            }
            ItemListTransferBeaconMenuCount.SelectedItem = "" + ((TransferBeaconState)_world.Planets[_player.Planet].Blocks[_player.Coord]).Count;
        }

        private void UpdateItemListTransferBeaconMenuPlanetList()
        {
            ItemListTransferBeaconMenuPlanetList.ClearItems();
            foreach (var planet in _world.Planets)
            {
                ItemListTransferBeaconMenuPlanetList.AddItem(planet.Name);
            }
            ItemListTransferBeaconMenuPlanetList.SelectedIndex = ((TransferBeaconState)_world.Planets[_player.Planet].Blocks[_player.Coord]).Planet;
        }
    }
}
