using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace SerpentEngine;  
public class SoundPlayer : Component
{
    public SoundPlayer() : base(false)
    {

    }

    public virtual void PlaySound(Sound sound)
    {
        SoundEffect soundEffect;
        FileStream fileStream = new FileStream(sound.Path + ".wav", FileMode.Open);
        soundEffect = SoundEffect.FromStream(fileStream);
        soundEffect.Play();

    }
}
