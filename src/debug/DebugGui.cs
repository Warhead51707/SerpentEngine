using ImGuiNET;
using System.Collections.Generic;

namespace SerpentEngine;
public class DebugGui : ImGuiDrawable
{
    private static List<string> logs = new List<string>();
    private bool showGeneralWindow = false;
    private bool showConsoleWindow = false;
    private bool showGameObjectsWindow = false;

    public static void Log(string message)
    {
        logs.Add(message);
    }

    public override void Draw()
    {
       MainMenuBar();

       if (showConsoleWindow)
       {
           ConsoleWindow();
       }

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

               if (ImGui.MenuItem("Console"))
                {
                    showConsoleWindow = true;
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

    public void ConsoleWindow()
    {
        ImGui.Begin("Console", ref showConsoleWindow);

        foreach (string log in logs)
        {
            ImGui.Text(log);
        }

        ImGui.End();
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

        List<GameObject> gameObjects = SceneManager.CurrentScene.GetGameObjects();

        ImGui.Text("GameObjects: " + gameObjects.Count);

        ImGui.SeparatorText("GameObjects");

        int index = 0;

        foreach (GameObject gameObject in gameObjects)
        {
            if (ImGui.CollapsingHeader(gameObject.ToString() + " (" + index + ")" ))
            {
                ImGui.SeparatorText("Properties");

                ImGui.Text("Position: " + gameObject.Position);
                ImGui.Text("Layer: " + gameObject.Layer);

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

                    if (component is AnimationTree animationTree)
                    {
                        ImGui.SeparatorText("AnimationTree");

                        ImGui.Text("Total Animations: " + animationTree.Animations.Count);
                        ImGui.Text("Current Animation: " + animationTree.CurrentAnimation.Path);
                    }

                    if (component is StateMachine stateMachine)
                    {
                        ImGui.SeparatorText("StateMachine");

                        ImGui.Text("Total States: " + stateMachine.States.Count);
                        ImGui.Text("Current State: " + stateMachine.CurrentState.Name);
                    }

                    if (component is Sprite sprite)
                    {
                        ImGui.SeparatorText("Sprite");

                        ImGui.Text("Path: " + sprite.Path);
                        ImGui.Text("Size: " + sprite.Size);
                        ImGui.Text("Scale: " + sprite.Scale);
                        ImGui.Text("Coordinates: " + sprite.Coordinates);
                        ImGui.Text("Rotation: " + sprite.Rotation);
                    }

                    if (component is SpriteSheet spriteSheet)
                    {
                        ImGui.SeparatorText("SpriteSheet");

                        ImGui.Text("Size: " + spriteSheet.Size);
                        ImGui.Text("TileSize: " + spriteSheet.TileSize);
                        ImGui.Text("Path: " + spriteSheet.CurrentSprite.Path);
                        ImGui.Text("Coordinates: " + spriteSheet.CurrentSprite.Coordinates);
                    }
                }

                ImGui.SeparatorText("Actions");

                if (ImGui.Button("Remove"))
                {
                    gameObject.Remove();

                    break;
                }
            }

            index++;
        }

        ImGui.End();
    }
}
