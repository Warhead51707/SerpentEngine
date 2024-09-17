using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine
{
    public class UiElementGrid
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public float GridSize { get; set; } = 1;

        public Dictionary<Vector2, GameObject> UiElements = new Dictionary<Vector2, GameObject>();

        public Vector2 Size { get; set; } = Vector2.Zero;

        public UiElementGrid(Vector2 size, float gridSize)
        {
            Size = size;
            GridSize = gridSize;
        }

        public void AddUiElement(GameObject ui)
        {
            ui.Position = Position += ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;
            UiElements.Add(ConvertNumberToGridCoordinates(UiElements.Count), ui);
        }

        public void AddUiElementGroup(UiElementGroup uiGroup)
        {
            uiGroup.Parent.Position = Position += ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;
            DebugGui.Log(uiGroup.Parent.Position + "");
            UiElements.Add(ConvertNumberToGridCoordinates(UiElements.Count), uiGroup.Parent);

            foreach(KeyValuePair<GameObject, Vector2> uiEntry in uiGroup.Children)
            {
                GameObject ui = uiEntry.Key;
                ui.Position = Position += ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;
                UiElements.Add(ConvertNumberToGridCoordinates(UiElements.Count), ui);
            }
        }

        public Vector2 ConvertNumberToGridCoordinates(int number)
        {
            Vector2 coordinates = new Vector2();

            for(int i = 0; i < number; i++)
            {
                coordinates.X++;
                if(coordinates.X == Size.X)
                {
                    coordinates.Y++;
                    coordinates.X = 0;
                }
            }

            return coordinates;
        }
    }
}
