using Godot;
using System;

public class PlayerWalk : PlayerBaseStateMachine
{
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.SprAnimation("Walk");
    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        if (owner.IsDamaged && owner.DamagedTimer == 0)
        {
            stateMachine.TransitionToState(owner.playerHurt);
            return;
        }

        if (!owner.IsOnFloor())
        {
            stateMachine.TransitionToState(owner.playerAir);
            return;
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
        else if (owner.Velocity == new Vector2(0, 0))
        {
            stateMachine.TransitionToState(owner.playerIdle);
        }
    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {

    }
}
