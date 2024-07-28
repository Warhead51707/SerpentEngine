using MonoGame.ImGuiNet;
using System.Collections.Generic;

namespace SerpentEngine;
public class ImGuiManager
{
    public ImGuiRenderer Renderer { get; private set; }
    public List<ImGuiDrawable> Drawables { get; private set; } = new List<ImGuiDrawable>();

    public ImGuiManager()
    {
        
    }

    public void Initialize()
    {
        Renderer = new ImGuiRenderer(SerpentGame.Instance);

        Renderer.RebuildFontAtlas();
    }

    public void Draw()
    {
        Renderer.BeginLayout(SerpentGame.GameTime);

        foreach (ImGuiDrawable drawable in Drawables)
        {
            if (!drawable.Visible) continue;

            drawable.Draw();
        }

        Renderer.EndLayout();
    }

    public void AddGuiDrawable(ImGuiDrawable drawable)
    {
        Drawables.Add(drawable);
    }

    public void RemoveGuiDrawable(ImGuiDrawable drawable)
    {
        Drawables.Remove(drawable);
    }
}
