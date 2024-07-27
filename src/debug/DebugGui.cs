using ImGuiNET;
using SerpentEngine;

namespace SerpentEngine;
public class DebugGui : ImGuiDrawable
{
    private bool showGameObjectsWindow = false;

    public override void Draw()
    {
       MainMenuBar();

       if (showGameObjectsWindow)
       {
           GameObjectsWindow();
       }
    }

    public void MainMenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Scene"))
            {
                if (ImGui.MenuItem("GameObjects"))
                {
                    showGameObjectsWindow = true;
                }
            }
        }
    }

    public void GameObjectsWindow()
    {
        ImGui.Begin("GameObjects", ref showGameObjectsWindow);

        ImGui.Text("GameObjects: " + SceneManager.CurrentScene.GameObjects.Count);

        ImGui.SeparatorText("GameObjects");

        foreach (GameObject gameObject in SceneManager.CurrentScene.GameObjects)
        {
            if (ImGui.CollapsingHeader(gameObject.ToString()))
            {
                ImGui.SeparatorText("Properties");

                ImGui.Text("Position: " + gameObject.Position);

                ImGui.SeparatorText("Components");

                foreach (Component component in gameObject.Components.Components)
                {
                    ImGui.Text(component.ToString());
                }

                foreach (Component component in gameObject.Components.Components)
                {
                    if (component is TileGrid tilegrid)
                    {
                        ImGui.SeparatorText("TileGrid");

                        ImGui.Text("Visible Tiles: " + tilegrid.VisibleTiles);
                    }
                }

                ImGui.SeparatorText("Actions");

                if (ImGui.Button("Remove"))
                {
                    SceneManager.CurrentScene.Remove(gameObject);

                    break;
                }
            }
        }
    }
}
