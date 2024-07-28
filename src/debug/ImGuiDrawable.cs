namespace SerpentEngine;
public abstract class ImGuiDrawable
{
    public bool Visible { get; set; } = true;
    public abstract void Draw();
}
