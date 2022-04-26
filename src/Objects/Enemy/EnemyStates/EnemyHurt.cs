using Godot;
using System;

public class EnemyHurt : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("HurtA");
        owner.ChangeHealth(-owner.NdObjPlayer.CurDmg);
        GD.Print(owner.EnemyType + " Hurt State----------------------------------------");
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
