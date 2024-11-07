using System.Drawing;

using Contorio.CharEngine;
using Contorio.CharEngine.Widgets;
using Contorio.Core;

namespace Contorio.Scenes.SceneWorld
{
    public class ScenePlanetInfo : Scene
    {
        private World _world;
        private Player _player;

        public Label LabelPlanetInfo;
        public Label LabelPlanetsLabel;
        public ItemList ItemListPlanetList;

        public ScenePlanetInfo(World world)
        {
            _world = world;
            _player = world.Player;

            // InitializeWidgets
            LabelPlanetInfo = new Label("Planet", ConsoleColor.White, new Point(0, 0));
            LabelPlanetsLabel = new Label("PLANETS", ConsoleColor.White, new Point(0, 20));
            ItemListPlanetList = new ItemList(
                ConsoleColor.White,
                ConsoleColor.DarkBlue,
                new Point(0, 21),
                9
            );

            // AddSprite
            AddSprite(LabelPlanetInfo);
            AddSprite(LabelPlanetsLabel);
            AddSprite(ItemListPlanetList);

            //
            UpdatePlanetList();
        }

        public override void Tick()
        {
            UpdatePlanetInfo(_world.Planets[_player.Planet]);
            UpdatePlanetList();
        }

        public void UpdatePlanetInfo(Planet planet)
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
            LabelPlanetInfo.Text = text;
        }

        public void UpdatePlanetList()
        {
            string planetSelected = ItemListPlanetList.SelectedItem;
            ItemListPlanetList.ClearItems();
            foreach (var planet in _world.Planets)
            {
                ItemListPlanetList.AddItem(planet.Name);
            }
            ItemListPlanetList.SelectedItem = planetSelected;
        }
    }
}
