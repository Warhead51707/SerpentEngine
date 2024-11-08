using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public class Sound
{
    public string Name { get; private set; } = "";
    public string Path { get; private set; } = "";
    public float Volume { get; set; } = 0.5f;

    public Sound(string path, string name)
    {
        Path = path;
        Name = name;
    }




}
