using Godot;
using System;

public class PlayerIdle : PlayerBaseStateMachine
{
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        //owner.Velocity = new Vector2(0, 0);
        owner.SprAnimation("Idle");
    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        if (owner.IsDamaged && owner.DamagedTimer == 0)
        {
            stateMachine.TransitionToState(owner.playerHurt);
        }
        else if (!owner.IsOnFloor())
        {
            stateMachine.TransitionToState(owner.playerAir); 
        }

        owner.BaseMovementControl();

        if (Input.IsActionJustPressed("ui_up"))
        {
            stateMachine.TransitionToState(owner.playerAir);
        }
        else if (owner.ActivateSkill())
        {
            stateMachine.TransitionToState(owner.playerSkill);
        }
        else if (owner.Velocity.x != 0)
        {
            stateMachine.TransitionToState(owner.playerWalk);
        }

        

    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {

    }
}
