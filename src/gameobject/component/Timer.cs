namespace SerpentEngine;

public delegate void TimerFinishEvent();

public class Timer
{
    public event TimerFinishEvent OnTimeout;

    public float WaitTime { get; set; } = 0;
    public bool Enabled { get; set; } = false;
    public bool Loop { get; set; } = false;
    private float time { get; set; } = 0;

    public Timer(float waitTime)
    {
        WaitTime = waitTime;
    }

    public void Update()
    {
        if (!Enabled) return;

        time += SerpentGame.DeltaTime;

        if (time >= WaitTime)
        {
            Finish();
        }
    }

    public void Finish()
    {
        Enabled = false;

        time = 0;

        if (Loop)
        {
            Enabled = true;
        }

        InvokeTimeout();

    }

    public void InvokeTimeout()
    {
        OnTimeout.Invoke();
    }
}
