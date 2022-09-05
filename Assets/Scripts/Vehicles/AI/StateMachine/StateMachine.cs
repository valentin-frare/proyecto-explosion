using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<IState> states;
    private int currentState = 0;

    public StateMachine(List<IState> states)
    {
        this.states = states;
    }

    public void Update() {
        states[currentState].Update();
    }

    public void SetCurrentState(int index)
    {
        currentState = index;
    }
}