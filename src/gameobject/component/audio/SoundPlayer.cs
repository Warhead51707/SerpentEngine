using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;

namespace SerpentEngine;  
public class SoundPlayer : Component
{
    private Dictionary<Sound, SoundEffect> soundEffectsCache = new Dictionary<Sound, SoundEffect>();

    public SoundPlayer() : base(false)
    {

    }

    public void AddSound(Sound sound)
    {
        using (FileStream fileStream = new FileStream(sound.Path + ".wav", FileMode.Open))
        {
            soundEffectsCache[sound] = SoundEffect.FromStream(fileStream);
        }
    }

    public void PlaySound(string soundName)
    {
        foreach (Sound sound in soundEffectsCache.Keys)
        {
            if (sound.Name == soundName)
            {
                soundEffectsCache[sound].Play();
                return;
            }
        }
    }
}
