using Godot;
using System;

public class EnemyAttackA : EnemyBaseStateMachine
{

    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("AttackA");
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsStomped)
        {
            owner.IsStomped = false;
            stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }
        // apply damage based on frames and attack range
        if (owner.NdSprEnemy.Frame >= 3 && owner.NdSprEnemy.Frame <= 7 && owner.EnemyAttack() && !owner.NdObjPlayer.IsDamaged)
        {
            owner.NdObjPlayer.IsDamaged = true;
        }

        //GD.Print("Attack Frame = " + owner.NdSprEnemy.Frame);
        if (owner.IsAnimationOver)
        {
            stateMachine.TransitionToState(owner.enemyIdle);
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.IsAnimationOver = false;

    }

    

}
