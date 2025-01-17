using System.Drawing;

using CharEngine;
using CharEngine.Widgets;
using Contorio.Core;
using Contorio.Core.Types;
using Contorio.Core.Interfaces;

namespace Contorio.Scenes.SceneWorld
{
    public class SceneDebugUI : Scene
    {
        private Engine _engine;

        private World _world;
        private Player _player;

        public Label LabelFPS;
        public Label LabelBlockInfo;

        public SceneDebugUI(Engine engine, World world)
        {
            _engine = engine;

            _world = world;
            _player = world.Player;

            // InitializeWidgets
            LabelFPS = new Label(
                "FPS: ",
                ConsoleColor.White,
                new Point(120, 0),
                alignment: Alignment.Right,
                textAlignment: TextAlignment.Right
            );

            LabelBlockInfo = new Label(
                "BLOCK INFO",
                ConsoleColor.White,
                new Point(120, 1),
                alignment: Alignment.Right,
                textAlignment: TextAlignment.Right
            );

            // AddSprite
            AddSprite(LabelFPS);
            AddSprite(LabelBlockInfo);
        }

        public override void Tick()
        {
            LabelFPS.Text = "FPS: " + _engine.FPS;
            UpdateBlockInfo();
        }

        private void UpdateBlockInfo()
        {
            LabelBlockInfo.Text = "BLOCK INFO\n";
            if (_world.Planets[_player.Planet].Blocks.TryGetValue(_player.Coord, out BlockState? blockState))
            {
                LabelBlockInfo.Text += $"name: {blockState.Name}\n\n";
                
                if (blockState is IConnectToEnergyPoint iConnectToEnergyPoint)
                {
                    LabelBlockInfo.Text += $"energy point: {iConnectToEnergyPoint.EnergyPoint}\n";
                }
                if (blockState is IConnectToDroneStation iConnectToDroneStation)
                {
                    LabelBlockInfo.Text += $"drone station: {iConnectToDroneStation.DroneStation}\n";
                }
            }
        }
    }
}
