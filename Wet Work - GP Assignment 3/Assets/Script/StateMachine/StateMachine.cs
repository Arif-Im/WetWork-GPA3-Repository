using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    //StateMachine will not be responsible for changing states. Rather, the states themselves will handle state changes.
    //use protected to allow subclasses to make modifications to State
    protected State State;

    public void SetState(State passedState)
    {
        State = passedState;
        if(State != null)
            StartCoroutine(State.Start());
    }

    public void SetNewState(State newState)
    {
        this.State = newState;
    }

    public State GetNewState()
    {
        return this.State;
    }
}
