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
    public string Path { get; private set; } = "";
    public float Volume { get; private set; } = 1f;
    public bool IsPlaying { get; private set; } = false;
    public SoundEffect SoundEffect { get; private set; }
    public Sound(string path)
    {
        FileStream fileStream = new FileStream(path + ".wav", FileMode.Open);
        SoundEffect = SoundEffect.FromStream(fileStream);

    }

    public void ChangeVolume(float volume)
    {
        Volume = volume;
        SoundEffect.MasterVolume = Volume;
    }


    public virtual void Play()
    {
        SoundEffect.Play();
        IsPlaying = true;
    }
}
