using Godot;
using System;

public class EnemyWander : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Wander");
        owner.Speed = new Vector2(100, 400);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.WanderLogic(owner.Direction);
        owner.BaseMovementControl();
        owner.EnemyTurnIdle();

        if (owner.IsStomped)
        {
            owner.IsStomped = false;
            stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }

        // if player detected go to chase state
        if (owner.PlayerDetected())
        {
            stateMachine.TransitionToState(owner.enemyChase);
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        
    }
}
