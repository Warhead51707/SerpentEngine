using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class StateMachine : Component
{
    public GameObjectState CurrentState { get; private set; }
    public Dictionary<string, GameObjectState> States { get; private set; } = new Dictionary<string, GameObjectState>();

    public StateMachine() : base(false)
    {
    }

    public void AddState(GameObjectState state)
    {
        States.Add(state.Name, state);

        state.SetGameObject(GameObject);

        state.Initialize();
    }

    public GameObjectState CreateAndAddState(string name)
    {
        GameObjectState state = new GameObjectState(name);

        AddState(state);

        return state;
    }

    public void SetState(string name)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        try
        {
            CurrentState = States[name];
        }
        catch
        {
            Debug.WriteLine("State " + "'" + name + "' does not exist!");
        }

        CurrentState.Enter();
    }

    public override void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
