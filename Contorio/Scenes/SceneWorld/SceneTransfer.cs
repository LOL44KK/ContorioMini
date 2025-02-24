﻿using System.Drawing;

using CharEngine;
using CharEngine.Containers;
using CharEngine.Widgets;
using Contorio.Core;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneTransfer : Scene
    {
        private World _world;
        private Player _player;

        public Label LabelPlanetResourcesToPlayer;
        public ItemList ItemListPlanetResourcesToPlayer;
        public Label LabelPlayerResourcesToPlanet;
        public ItemList ItemListPlayerResourcesToPlanet;
        public Label LabelCountResource;
        public Label LabelTransfer;
        public ItemList ItemListResourcesToPlayerCount;
        public Label LabelPreesEToTransferPlayer;
        public Label LabelPreesFToTransferPlanet;

        public ItemListContainer ItemListContainer;

        public SceneTransfer(World world)
        {
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            LabelPlanetResourcesToPlayer = new Label("PLANET RESOURCES", ConsoleColor.White, new Point(27, 1));
            ItemListPlanetResourcesToPlayer = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(27, 2),
                12
            );
            LabelPlayerResourcesToPlanet = new Label("PLAYER RESOURCES", ConsoleColor.White, new Point(93, 1), alignment:Alignment.Right);
            ItemListPlayerResourcesToPlanet = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point(93, 2),
                12,
                alignment: Alignment.Right,
                textAlignment: TextAlignment.Right
            );
            LabelCountResource = new Label("Count: 0", ConsoleColor.White, new Point(120 / 2, 1), alignment: Alignment.Center);
            LabelTransfer = new Label("transfer: ", ConsoleColor.White, new Point(120 / 2, 2), alignment:Alignment.Center);
            ItemListResourcesToPlayerCount = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                new Point((120 / 2 ) + (LabelTransfer.Width / 2) + 2, 2),
                1,
                alignment:Alignment.Center
            );
            for (int i = 1; i < 10000; i *= 2) ItemListResourcesToPlayerCount.AddItem("" + i);

            LabelPreesEToTransferPlayer = new Label("Prees E to transfer to player", ConsoleColor.White, new Point(120 / 2, 3), alignment:Alignment.Center);
            LabelPreesFToTransferPlanet = new Label("Prees F to transfer to planet", ConsoleColor.White, new Point(120 / 2, 4), alignment: Alignment.Center);

            // ItemListContainer
            ItemListContainer = new ItemListContainer();
            ItemListContainer.AddItemList(ItemListPlanetResourcesToPlayer);
            ItemListContainer.AddItemList(ItemListResourcesToPlayerCount);
            ItemListContainer.AddItemList(ItemListPlayerResourcesToPlanet);

            // AddSprte
            AddSprite(LabelPlanetResourcesToPlayer);
            AddSprite(ItemListPlanetResourcesToPlayer);
            AddSprite(LabelPlayerResourcesToPlanet);
            AddSprite(ItemListPlayerResourcesToPlanet);
            AddSprite(LabelCountResource);
            AddSprite(LabelTransfer);
            AddSprite(ItemListResourcesToPlayerCount);
            AddSprite(LabelPreesEToTransferPlayer);
            AddSprite(LabelPreesFToTransferPlanet);
        }

        public override void Ready()
        {
            ItemListContainer.SelectedItemList = 1;
        }

        public override void Tick()
        {
            UpdatePlanetResourcesToPlayer();
            UpdatePlayerResourcesToPlanet();
            UpdateCountTransferResources();
        }

        public override void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    ItemListContainer.PreviousItemList();
                    break;
                case ConsoleKey.RightArrow:
                    ItemListContainer.NextItemList();
                    break;
                case ConsoleKey.UpArrow:
                    ItemListContainer.PreviousItem();
                    break;
                case ConsoleKey.DownArrow:
                    ItemListContainer.NextItem();
                    break;
                case ConsoleKey.E:
                    if (ItemListPlanetResourcesToPlayer.Items.Count == 0) break;

                    int countplanet = int.Parse(ItemListResourcesToPlayerCount.SelectedItem);
                    if (_world.Planets[_player.Planet].Resources[ItemListPlanetResourcesToPlayer.SelectedItem] >= countplanet)
                    {
                        _world.Planets[_player.Planet].Resources[ItemListPlanetResourcesToPlayer.SelectedItem] -= countplanet;
                        _player.Resources[ItemListPlanetResourcesToPlayer.SelectedItem] = _player.Resources.GetValueOrDefault(ItemListPlanetResourcesToPlayer.SelectedItem, 0) + countplanet;
                    }
                    break;
                case ConsoleKey.F:
                    if (ItemListPlayerResourcesToPlanet.Items.Count == 0) break;

                    int countplayer = int.Parse(ItemListResourcesToPlayerCount.SelectedItem);
                    if (_player.Resources[ItemListPlayerResourcesToPlanet.SelectedItem] >= countplayer)
                    {
                        _player.Resources[ItemListPlayerResourcesToPlanet.SelectedItem] -= countplayer;
                        _world.Planets[_player.Planet].Resources[ItemListPlayerResourcesToPlanet.SelectedItem] = _world.Planets[_player.Planet].Resources.GetValueOrDefault(ItemListPlayerResourcesToPlanet.SelectedItem, 0) + countplayer;
                    }
                    break;
            }
        }

        private void UpdatePlanetResourcesToPlayer()
        {
            if (ItemListPlanetResourcesToPlayer.Items.Count > 0)
            {
                LabelPreesEToTransferPlayer.Visible = true;
            }
            else
            {
                LabelPreesEToTransferPlayer.Visible = false;
            }

            string? selectedItem = null;
            if (ItemListPlanetResourcesToPlayer.Items.Count > 0)
            {
                selectedItem = ItemListPlanetResourcesToPlayer.SelectedItem;
            }
            ItemListPlanetResourcesToPlayer.ClearItems();

            foreach (var resource in _world.Planets[_player.Planet].Resources)
            {
                ItemListPlanetResourcesToPlayer.AddItem(resource.Key);
            }

            if (selectedItem != null)
            {
                ItemListPlanetResourcesToPlayer.SelectedItem = selectedItem;
            }
        }

        private void UpdatePlayerResourcesToPlanet()
        {
            if (ItemListPlayerResourcesToPlanet.Items.Count > 0)
            {
                LabelPreesFToTransferPlanet.Visible = true;
            }
            else
            {
                LabelPreesFToTransferPlanet.Visible = false;
            }

            string? selectedItem = null;
            if (ItemListPlayerResourcesToPlanet.Items.Count > 0)
            {
                selectedItem = ItemListPlayerResourcesToPlanet.SelectedItem;
            }
            ItemListPlayerResourcesToPlanet.ClearItems();
            foreach (var resource in _player.Resources)
            {
                ItemListPlayerResourcesToPlanet.AddItem(resource.Key);
            }
            if (selectedItem != null)
            {
                ItemListPlayerResourcesToPlanet.SelectedItem = selectedItem;
            }
        }

        private void UpdateCountTransferResources()
        {
            LabelCountResource.Text = _world.Planets[_player.Planet].Resources.GetValueOrDefault(ItemListPlanetResourcesToPlayer.SelectedItem ?? "", 0) + "|" + _player.Resources.GetValueOrDefault(ItemListPlayerResourcesToPlanet.SelectedItem ?? "", 0);
        }
    }
}
