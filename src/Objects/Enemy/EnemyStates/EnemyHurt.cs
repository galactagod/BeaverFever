using Godot;
using System;

public class EnemyHurt : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Hurt");
        owner.ChangeHealth(owner.Health - owner.NdObjPlayer.CurDmg);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsAnimationOver)
        {
            owner.IsStomped = false;
            stateMachine.TransitionToState(owner.enemyIdle);
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.IsAnimationOver = false;
    }
}
