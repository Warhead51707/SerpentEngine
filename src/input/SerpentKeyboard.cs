using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace SerpentEngine;
public class SerpentKeyboard
{
    private KeyboardState newKeyboard, oldKeyboard;
    private List<SerpentKey> pressedKeys, previousPressedKeys = new List<SerpentKey>();

    public void Update()
    {
        newKeyboard = Keyboard.GetState();

        GetPressedKeys();

        oldKeyboard = newKeyboard;

        previousPressedKeys = new List<SerpentKey>();

        for (int i = 0; i < pressedKeys.Count; i++)
        {
            previousPressedKeys.Add(pressedKeys[i]);
        }
    }

    public bool GetKeyPress(string KEY)
    {
        for (int i = 0; i < pressedKeys.Count; i++)
        {
            if (pressedKeys[i].key == KEY)
            {
                return true;
            }
        }

        return false;
    }

    public void GetPressedKeys()
    {
        bool found = false;

        pressedKeys.Clear();

        for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
        {
            pressedKeys.Add(new SerpentKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
        }
    }

}