using System;
using System.Collections.Generic;

namespace SerpentEngine;
public class AnimationTree : Component
{
    public Dictionary<Predicate<bool>, Animation> Animations { get; private set; } = new Dictionary<Predicate<bool>, Animation>();
    public Animation CurrentAnimation { get; private set; }

    public AnimationTree() : base(true)
    {
    }

    public void AddAnimation(string path, bool condition)
    {
        Animation animation = new Animation(path);
        animation.SpriteSheet.Add(GameObject);

        Animations.Add(_ => condition, animation);
    }

    public override void Update()
    {
       foreach (KeyValuePair<Predicate<bool>, Animation> animation in Animations)
        {
            if (!animation.Key.Invoke(true)) continue;

            if (CurrentAnimation != null)
            {
                if (CurrentAnimation == animation.Value) continue;

                CurrentAnimation.Stop();
            }

            CurrentAnimation = animation.Value;

            CurrentAnimation.Play();
        }
    }

    public override void Draw()
    {
        if (CurrentAnimation == null) return;

        CurrentAnimation.SpriteSheet.Draw();
    }
}
