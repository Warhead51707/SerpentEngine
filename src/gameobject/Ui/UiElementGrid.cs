using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class UiElementGrid : Component
{
    public Vector2 Position { get; set; } = Vector2.Zero;

    public float GridSize { get; set; } = 1;

    public List<GameObject> UiElements = new List<GameObject>();

    public Vector2 Size { get; set; } = Vector2.Zero;

    public UiElementGrid(Vector2 size, float gridSize) : base(true)
    {
        Size = size;
        GridSize = gridSize;
    }

    public override void Update()
    {
        foreach(GameObject ui in UiElements)
        {
            ui.Update();
        }
    }

    public override void Draw()
    {
        foreach(GameObject ui in UiElements)
        {
            ui.Draw();
        }
    }

    public void AddUiElement(GameObject ui)
    {
        ui.Position = Position + ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;
        UiElements.Add(ui);
        ui.Initialize();
    }

    public void AddUiElementGroup(UiElementGroup uiGroup)
    {
        uiGroup.Parent.Position = Position + ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;

        DebugGui.Log(uiGroup.Parent.Position + " " + ConvertNumberToGridCoordinates(UiElements.Count)  + " " + GridSize);

        UiElements.Add(uiGroup.Parent);

        uiGroup.Parent.Initialize();


        foreach (KeyValuePair<GameObject, Vector2> uiEntry in uiGroup.Children)
        {
            GameObject ui = uiEntry.Key;
            ui.Position = Position + uiEntry.Value + ConvertNumberToGridCoordinates(UiElements.Count) * GridSize;
            UiElements.Add(ui);
            ui.Initialize();
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
