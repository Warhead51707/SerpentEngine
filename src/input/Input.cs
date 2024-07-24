

namespace SerpentEngine;

public static class Input
{
    public static SerpentMouse Mouse { get; private set; }
    public static SerpentKeyboard Keyboard { get; private set; }

    public static void Initialize()
    {
        Keyboard = new SerpentKeyboard();
        Mouse = new SerpentMouse();
    }

    public static void Update()
    {
        Keyboard.Update();
        Mouse.Update();
    }
}
