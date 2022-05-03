using Godot;
using System;

public class EnemyWander : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Wander");
        owner.Speed = new Vector2(owner.OrigSpeed.x, owner.OrigSpeed.y);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.WanderLogic(owner.Direction);
        owner.BaseMovementControl();
        owner.EnemyTurnIdle();

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

        // if player detected go to chase state
        if (owner.PlayerDetected())
        {
            stateMachine.TransitionToState(owner.enemyChase);
        }
        // Do range attack on player till detection is false and maintain attack if player jumps out of detection
        else if (owner.EnemyRangeAttack() && owner.EnemyType == "Spider")
        {
            float dirX;
            dirX = (owner.NdObjPlayer.Position.x > owner.Position.x) ? 1 : -1;
            // turn towards direction of player
            owner.Direction = new Vector2(dirX, 0);
            stateMachine.TransitionToState(owner.enemyAttackB);
        }
        

    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        
    }
}
