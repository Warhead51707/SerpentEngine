

using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace SerpentEngine;

public delegate void TimeoutEvent();

public class Timer : Component
{
    public event TimeoutEvent OnTimeout;

    public float WaitTime { get; set; } = 0;

    public float Time { get; set; } = 0;

    public bool Autostart { get; set; } = false;

    public Timer(float waitTime) : base(false)
    {
        WaitTime = waitTime;
    }

    public virtual void Start(float waitTime)
    {
        WaitTime = waitTime;
        Enabled = true;
    }


    public override void Update()
    {
        if (!Enabled) return;

        Time += SerpentGame.DeltaTime;


        if(Time >= WaitTime)
        {
            End();
        }

        base.Update();
    }

    public virtual void End()
    {
        InvokeTimeout();
        Enabled = false;
        Time = 0;

        if (Autostart)
        {
            Start(WaitTime);
        }
        
    }

    public void InvokeTimeout()
    {
        OnTimeout.Invoke();
    }
}
