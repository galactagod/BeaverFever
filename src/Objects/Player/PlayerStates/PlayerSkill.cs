using Godot;
using System;

public class PlayerSkill : PlayerBaseStateMachine
{
    
    public override void OnStateEnter(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.BaseMovementControl();

        // Call skill instance
        Type skillType = Type.GetType(owner.CurSkill);
        SkillMove ndSkillMove = (SkillMove)Activator.CreateInstance(skillType, owner, null, "player");
        owner.AddChild(ndSkillMove);
        
        GD.Print("Skill State");

        if (!owner.IsOnFloor())
        {
            owner.IsInAir = true;
        }

        // Activate current skill animation
        owner.AttackAnimation(owner.CurSkill, "start");
    }

    public override void OnStateUpdate(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        if (owner.IsDamaged && owner.DamagedTimer == 0)
        {
            stateMachine.TransitionToState(owner.playerHurt);
        }
        else if (!owner.IsOnFloor() && !owner.UseSkill)
        {
            stateMachine.TransitionToState(owner.playerAir);
        }
        else if (owner.IsOnFloor() && !owner.UseSkill)
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
        // transition an air animation to a floor if they touch floor while falling
        else if (owner.IsOnFloor() && owner.IsInAir == true)
        {
            owner.AttackAnimation(owner.CurSkill, "end");
        }

        owner.BaseMovementControl();
    }

    public override void OnStateExit(IPlayerStateMachine stateMachine, ObjPlayer owner)
    {
        owner.IsInAir = false;
        owner.IsAnimationOver = false;
        owner.CurSkill = "";
        owner.UseSkill = false;
    }
}
