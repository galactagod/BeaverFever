using Godot;
using System;

public class EnemyDeath : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Death");
        owner.ChangeHealth(-owner.NdObjPlayer.CurDmg);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsAnimationOver)
        {
            owner.IsStomped = false;

            // give player exp and remove the node
            owner.NdPlayerStats.ChangeExp(owner.Exp);
            owner.QueueFree();
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
    
    }
}
