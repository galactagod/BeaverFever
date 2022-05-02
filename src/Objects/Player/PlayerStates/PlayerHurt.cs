using Godot;
using System;

public class PlayerHurt : PlayerBaseStateMachine
{
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.SprAnimation("Hurt");
        owner.Battled();
        //owner.NdPlayerStats.ChangeHealth(-3);
    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        if (owner.IsAnimationOver)
        {
            stateMachine.TransitionToState(owner.playerIdle);
        }
    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.IsAnimationOver = false;
    }
}
