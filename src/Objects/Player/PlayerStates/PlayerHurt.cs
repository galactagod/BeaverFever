using Godot;
using System;

public class PlayerHurt : PlayerBaseStateMachine
{
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.SprAnimation("Hurt");
        owner.NdPlayerStats.ChangeHealth(owner.NdPlayerStats.Health - 3);

    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        //owner.BaseMovementControl();

        if (owner.IsAnimationOver)
        {
            owner.IsDamaged = false;
            stateMachine.TransitionToState(owner.playerIdle);

        }
    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.IsAnimationOver = false;
        owner.IsDamaged = false;
    }
}
