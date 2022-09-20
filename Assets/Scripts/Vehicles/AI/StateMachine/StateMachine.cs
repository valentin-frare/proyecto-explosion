using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<IState> states;
    private int currentState = 0;

    public List<IState> States => states;

    public StateMachine(List<IState> states)
    {
        this.states = states;
    }

    public void Update() {
        try
        {
            states[currentState].Update();
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogError("Tried to update a not existent state. " + currentState.ToString());
        }
    }

    public void SetCurrentState(int index)
    {
        currentState = index;
    }

    public IState GetCurrentState()
    {
        return states[currentState];
    }
}