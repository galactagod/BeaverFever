using Godot;
using System;

public class PlayerAir : PlayerBaseStateMachine
{
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.IsInAir = true;

        if (owner.Velocity.y > 0)
        {
            owner.SprAnimation("Fall");
        }
        else if (owner.Velocity.y < 0 || owner.StompJump)
        {
            owner.SprAnimation("Jump");
            owner.StompJump = false;
        }
    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.BaseMovementControl();
        if (owner.IsDamaged && owner.DamagedTimer == 0)
        {
            stateMachine.TransitionToState(owner.playerHurt);
            return;
        }

        // iterate the jump animation if we stay in this stomp state
        if (owner.StompJump)
        {
            owner.SprAnimation("Jump");
            owner.StompJump = false;
        }
        

        if (owner.IsOnFloor())
        {
            if (owner.Velocity == new Vector2(0, 0))
            {
                stateMachine.TransitionToState(owner.playerIdle);
            }
            else
            {
                stateMachine.TransitionToState(owner.playerWalk);
            }
        }
    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.IsInAir = false;
        owner.IsAnimationOver = false;
    }
}
