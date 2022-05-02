using Godot;
using System;

public class EnemyChase : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Wander");
        owner.Speed = new Vector2(owner.ChaseSpeed.x, owner.ChaseSpeed.y);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsStomped || owner.IsDamaged)
        {
            owner.IsStomped = false;
            owner.IsDamaged = false;
            if (owner.Health - owner.Battled(owner.NdObjPlayer.CurDmg, owner.NdObjPlayer.IsPhysical) <= 0)
                stateMachine.TransitionToState(owner.enemyDeath);
            else
                stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }

        // follow player till detection is false and maintain chase if player jumps out of detection
        if (owner.PlayerDetected() || owner.NdObjPlayer.IsInAir)
        {
            float dirX;
            dirX = (owner.NdObjPlayer.Position.x > owner.Position.x) ? 1 : -1;
            owner.Direction = new Vector2(dirX, 0);

            // if enemy in range they will attack player
            if (owner.EnemyAttack())
            {
                stateMachine.TransitionToState(owner.enemyAttackA);
            }
        }
        else
        {
            stateMachine.TransitionToState(owner.enemyReturn);
        }

        owner.BaseMovementControl();
        owner.EnemyTurnIdle();

    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        
    }
}
