using Godot;
using System;

public class PlayerStateMachineManager : IPlayerStateMachine
{
    protected ObjPlayer owner;

    protected PlayerBaseStateMachine currentState;

    public PlayerStateMachineManager(ObjPlayer newOwner, PlayerBaseStateMachine newCurrentState)
    {
        owner = newOwner;
        TransitionToState(newCurrentState);
    }

    public void Update()
    {
        currentState.OnStateUpdate(this, owner);
    }

    public void TransitionToState(PlayerBaseStateMachine state)
    {
        if (currentState != null && state != null)
        {
            currentState.OnStateExit(this, owner);
            currentState = state;
            if(state is PlayerHurt)
            {
                Console.WriteLine("hahahahahahah easy tec");
            }
            currentState.OnStateEnter(this, owner);
        }
        else if (currentState == null && state != null)
        {
            currentState = state;
            state.OnStateEnter(this, owner);
        }
    }
}
