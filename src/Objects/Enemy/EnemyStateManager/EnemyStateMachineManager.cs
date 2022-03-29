using Godot;
using System;

public class EnemyStateMachineManager : IEnemyStateMachine
{
    protected EnemyMovementAct owner;

    protected EnemyBaseStateMachine currentState;

    public EnemyStateMachineManager(EnemyMovementAct newOwner, EnemyBaseStateMachine newCurrentState)
    {
        owner = newOwner;
        TransitionToState(newCurrentState);
    }

    public void Update()
    {
        currentState.OnStateUpdate(this, owner);
    }

    public void TransitionToState(EnemyBaseStateMachine state)
    {
        if (currentState != null && state != null)
        {
            currentState.OnStateExit(this, owner);
            currentState = state;
            currentState.OnStateEnter(this, owner);
        }
        else if (currentState == null && state != null)
        {
            currentState = state;
            state.OnStateEnter(this, owner);
        }
    }
}
