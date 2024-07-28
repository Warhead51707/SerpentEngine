using ImGuiNET;

namespace SerpentEngine;
public class DebugGui : ImGuiDrawable
{
    private bool showGeneralWindow = false;
    private bool showGameObjectsWindow = false;

    public override void Draw()
    {
       MainMenuBar();

       if (showGeneralWindow)
       {
           GeneralWindow();
       }

       if (showGameObjectsWindow)
       {
           GameObjectsWindow();
       }
    }

    public void MainMenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Game"))
            {
               if (ImGui.MenuItem("General"))
                {
                    showGeneralWindow = true;
                }

               ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Scene"))
            {
                if (ImGui.MenuItem("GameObjects"))
                {
                    showGameObjectsWindow = true;
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }

    public void GeneralWindow()
    {
        ImGui.Begin("General", ref showGeneralWindow);

        ImGui.Text("FPS: " + SerpentGame.FPS);
        ImGui.Text("Current Scene: " + SceneManager.CurrentScene.Name);

        ImGui.End();
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
