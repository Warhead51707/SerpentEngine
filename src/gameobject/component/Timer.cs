

using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace SerpentEngine;

public delegate void TimeoutEvent();

public class Timer : Component
{
    public event TimeoutEvent OnTimeout;

    public int WaitTime { get; set; } = 0;

    public int Time { get; set; } = 0;

    public bool Autostart { get; set; } = false;


    private System.Timers.Timer timer = new System.Timers.Timer();
    public Timer(int waitTime) : base(false)
    {
        WaitTime = waitTime;
        timer.Enabled = false;

    }

    public void Start(int waitTime)
    {
        WaitTime = waitTime;
        Start();
    }

    public void Start()
    {

        Time = WaitTime;

        timer = new System.Timers.Timer(WaitTime * 1000);

        timer.Elapsed += End;

        timer.Enabled = true;

    }

    public void End(object sender, ElapsedEventArgs e)
    {
        OnTimeout.Invoke();

        if (Autostart)
        {
            timer.Enabled = false;
            Start();
        }
        else
        {
            timer.Enabled = false;
        }
    }
}
