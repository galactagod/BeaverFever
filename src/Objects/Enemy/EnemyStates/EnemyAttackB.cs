using Godot;
using System;

public class EnemyAttackB : EnemyBaseStateMachine
{
    public override void OnStateEnter(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        //owner.BaseMovementControl();

        // Call skill instance
        Type skillType = Type.GetType(owner.CurSkill);
        SkillMove ndSkillMove = (SkillMove)Activator.CreateInstance(skillType, owner.NdObjPlayer, owner, "enemy");
        owner.GetParent().AddChild(ndSkillMove);
        //owner.AddChild(ndSkillMove);

        owner.SprAnimation("AttackB");
    }

    public override void OnStateUpdate(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        if (owner.IsStomped || owner.IsDamaged)
        {
            GD.Print("Spider Dmg");
            owner.IsStomped = false;
            owner.IsDamaged = false;
            if (owner.Health - owner.Battled(owner.NdObjPlayer.CurDmg, owner.NdObjPlayer.IsPhysical) <= 0)
                stateMachine.TransitionToState(owner.enemyDeath);
            else
                stateMachine.TransitionToState(owner.enemyHurt);
            return;
        }
        else if (!owner.UseSkill)
        {
            GD.Print("wander state");
            /*
            if (owner.EnemyType == "Crow")
            {
                stateMachine.TransitionToState(owner.enemyReturn);
            }
            else
            */
            {
                stateMachine.TransitionToState(owner.enemyWander);
            }

        }
        
        // player will be damaged from the instantiated skill from state entrance
        // the attacker and damage will be set up when skill hits the player
        // owner.NdObjPlayer.IsDamaged = true;
        // owner.NdObjPlayer.Attacker = owner;
        // set UseSkill = false; on skill after freeing it
    }

    public override void OnStateExit(IEnemyStateMachine stateMachine, EnemyMovementAct owner)
    {
        owner.CurSkill = "";
        owner.UseSkill = false;
        owner.IsAnimationOver = false;
    }
}
