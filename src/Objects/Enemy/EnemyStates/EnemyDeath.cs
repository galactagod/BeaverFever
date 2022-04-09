using Godot;
using System;

public class EnemyDeath : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Death");
        owner.ChangeHealth(owner.Health - owner.NdObjPlayer.CurDmg);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsAnimationOver)
        {
            owner.IsStomped = false;

            // remove the node
            owner.QueueFree();
        }
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
    
    }
}
