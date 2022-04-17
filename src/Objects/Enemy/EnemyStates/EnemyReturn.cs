using Godot;
using System;

public class EnemyReturn : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.SprAnimation("Wander");
        owner.Speed = new Vector2(200, 400);
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        bool isReturned = owner.EnemyReturn();
        owner.BaseMovementControl();
        owner.EnemyTurnIdle();

        if (owner.IsStomped)
        {
            owner.IsStomped = false;
            if (owner.Health - owner.NdObjPlayer.CurDmg <= 0)
                stateMachine.TransitionToState(owner.enemyDeath);
            else
                stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }

        if (isReturned)
        {
            stateMachine.TransitionToState(owner.enemyWander);
        }
        else if (owner.PlayerDetected())
        {
            stateMachine.TransitionToState(owner.enemyChase);
        }


    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
       
    }
}

