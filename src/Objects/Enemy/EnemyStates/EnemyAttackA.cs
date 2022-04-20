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
            if (owner.Health - owner.NdObjPlayer.CurDmg <= 0)
                stateMachine.TransitionToState(owner.enemyDeath);
            else
                stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }

        //GD.Print("Enemy Dir X = " + owner.Direction.x + " Y = " + owner.Direction.y);
        // apply damage based on frames and attack range
        if (owner.NdSprEnemy.Frame >= owner.AtkFrm[0] && owner.NdSprEnemy.Frame <= owner.AtkFrm[1] && owner.EnemyAttack() && !owner.NdObjPlayer.IsDamaged)
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
