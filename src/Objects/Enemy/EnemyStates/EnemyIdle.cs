using Godot;
using System;

public class EnemyIdle : EnemyBaseStateMachine
{
    private int timer = 0;

    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.Velocity = new Vector2(0, 0);
        owner.SprAnimation("Idle");
        timer = 0;
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        //owner.BaseMovementControl();
        timer++;
        if (timer % 30 == 0)
        {
            owner.BaseMovementControl();

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

            if (owner.EnemyAttack())
            {
                stateMachine.TransitionToState(owner.enemyAttackA);
            }
            else
            {
                stateMachine.TransitionToState(owner.enemyReturn);
            }
            
        }      
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        
    }
}
