using System;
using System.Collections.Generic;
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

    public void SetState(string name)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = States[name];

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
