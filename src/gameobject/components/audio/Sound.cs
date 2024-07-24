using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine { 
    public class Sound : Component
    {
        public string Path { get; set; } = "";
        public float Volume { get; set; } = 1f;

        public Song SoundEffect { get; set; }

        public Sound(string path) : base(false)
        {

        }



    }
}
