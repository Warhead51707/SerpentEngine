using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class GameObjectState
{
    public string Name { get; private set; }
    public GameObject GameObject { get; private set; }
    public StateMachine StateMachine { get; private set; }

    public GameObjectState(string name)
    {
        Name = name;
    }

    public void SetGameObject(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public virtual void Initialize()
    {
        StateMachine = GameObject.GetComponent<StateMachine>();
    }

    public virtual void Update()
    {
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}
