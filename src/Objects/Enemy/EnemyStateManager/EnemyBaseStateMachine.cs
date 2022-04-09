using Godot;
using System;

public abstract class EnemyBaseStateMachine
{
    public abstract void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner);
    public abstract void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner);
    public abstract void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner);
}
