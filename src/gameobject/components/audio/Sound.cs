using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine { 
    public class Sound : Component
    {
        public string Path { get; set; } = "";
        public float Volume { get; set; } = 1f;

        public bool isPlaying { get; set; } = false;

        public SoundEffect SoundEffect { get; set; }

        public Sound(string path) : base(false)
        {
            FileStream fileStream = new FileStream(path + ".wav", FileMode.Open);
            SoundEffect = SoundEffect.FromStream(fileStream);

        }


        public virtual void Play()
        {
            SoundEffect.Play();
            isPlaying = true;
        }



    }
}
