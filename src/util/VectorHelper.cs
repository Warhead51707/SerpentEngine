using Microsoft.Xna.Framework;
using System;

namespace SerpentEngine;
public static class VectorHelper
{
    public static Vector2 Snap(Vector2 value, float grid)
    {
        //Snaps a coordinate to the closest designated grid square. 
        return Vector2.Floor(value / grid) * grid;
    }

    public static float GetDistance(Vector2 pos, Vector2 target)
    {
        //Returns the distance between two coordinates.
        return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
    }

    public static Vector2 ToCoordinates(int num, Vector2 dims)
    {

        //Transforms a number into coordinates

        int x = 0;
        int y = 0;

        for (int i = 0; i < num; i++)
        {
            x++;
            if (x >= dims.X)
            {
                y++;
                x = 0;
            }


        }
        return new Vector2(x, y);
    }

    public static int ToNumber(Vector2 cords, Vector2 dims)
    {
        //Transforms a coordinate into a number


        int x = 0;
        int y = 0;

        int index = 0;

        for (int i = 0; i < dims.X * dims.Y; i++)
        {


            if (new Vector2(x, y) == cords)
            {
                return index;
            }
            x++;
            index++;
            if (x >= dims.X)
            {
                y++;
                x = 0;
            }



        }
        return -1;
    }


}
