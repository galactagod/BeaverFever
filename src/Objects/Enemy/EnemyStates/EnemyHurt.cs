using Godot;
using System;

public class EnemyHurt : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("HurtA");
        owner.BattledDamage(owner.NdObjPlayer.CurDmg, owner.NdObjPlayer.IsPhysical);
        GD.Print(owner.EnemyType + " Hurt State----------------------------------------");
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsAnimationOver)
        {
            owner.IsStomped = false;
            owner.IsDamaged = false;
            stateMachine.TransitionToState(owner.enemyIdle);
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.IsAnimationOver = false;
    }
}
