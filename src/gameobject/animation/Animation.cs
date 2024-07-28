using Microsoft.Xna.Framework;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Animation
{
    public string Path { get; private set; }
    public SpriteSheet SpriteSheet { get; private set; }
    public Vector2 FrameSize { get; private set; }
    public float FrameLength { get; private set; }
    public bool Loop { get; private set; }
    public bool Playing { get; private set; } = false;

    public Animation(string path)
    {
        Path = path;

        string jsonFileContents = File.ReadAllText(path + ".json");
        AnimationModel animationModel = JsonSerializer.Deserialize<AnimationModel>(jsonFileContents);

        SpriteSheet = new SpriteSheet(animationModel.SpriteSheet, new Vector2(animationModel.FrameSize.Width, animationModel.FrameSize.Height));
        FrameSize = new Vector2(animationModel.FrameSize.Width, animationModel.FrameSize.Height);
        FrameLength = animationModel.FrameLength;
        Loop = animationModel.Loop;
    }

    public void Play()
    {
        if (Playing) return;

        Playing = true;

        Task.Run(async () =>
        {
            await Task.Delay(50);

            while(Playing)
            {
                for (int y = 0; y < SpriteSheet.Size.Y; y++)
                {
                    if (!Playing) break;

                    SpriteSheet.ChangeCoordinates(new Vector2(0, y));

                    await Task.Delay((int)(FrameLength * 1000));  
                }

                if (!Loop) break;
            }
        });
    }

    public void Stop()
    {
        Playing = false;
    }
}
