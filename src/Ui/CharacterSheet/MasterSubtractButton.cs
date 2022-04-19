using Godot;
using System;


public class MasterSubtractButton : TextureButton
{
    [Export]
    String Type;
    [Signal]
    public delegate void statPointsSubtract(string type);
    private LevelControl levelControl;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = GetNode<LevelControl>("/root/LevelControl");
        ActionMode = ActionModeEnum.Press;
        var mainSheet = GetNode(levelControl.rootPath + "CharacterSheet");
        mainSheet.Connect("attackStatPointsEmptied", this, "disableAttack");
        mainSheet.Connect("attackStatPointsFilled", this, "enableAttack");
        mainSheet.Connect("defenseStatPointsEmptied", this, "disableDefense");
        mainSheet.Connect("defenseStatPointsFilled", this, "enableDefense");
        mainSheet.Connect("specialAttackStatPointsEmptied", this, "disableSpecialAttack");
        mainSheet.Connect("specialAttackStatPointsFilled", this, "enableSpecialAttack");
        mainSheet.Connect("specialDefenseStatPointsEmptied", this, "disableSpecialDefense");
        mainSheet.Connect("specialDefenseStatPointsFilled", this, "enableSpecialDefense");
        mainSheet.Connect("healthStatPointsEmptied", this, "disableHealth");
        mainSheet.Connect("healthStatPointsFilled", this, "enableHealth");
        mainSheet.Connect("staminaStatPointsEmptied", this, "disableStamina");
        mainSheet.Connect("staminaStatPointsFilled", this, "enableStamina");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //public override void _Process(float delta)
    //{
        
    //}
    public override void _Pressed()
    {
        EmitSignal("statPointsSubtract", Type);
    }

    #region Enables and Disables
    public void disableAttack()
    {
        if(Type == "Attack")
        {
            Disabled = true;
        }
    }

    public void enableAttack()
    {
        if(Type == "Attack")
        {
            Disabled = false;
        }
    }

    public void disableDefense()
    {
        if (Type == "Defense")
        {
            Disabled = true;
        }
    }

    public void enableDefense()
    {
        if (Type == "Defense")
        {
            Disabled = false;
        }
    }

    public void disableSpecialAttack()
    {
        if (Type == "SpecialAttack")
        {
            Disabled = true;
        }
    }

    public void enableSpecialAttack()
    {
        if(Type == "SpecialAttack")
        {
            Disabled = false;
        }
    }

    public void disableSpecialDefense()
    {
        if (Type == "SpecialDefense")
        {
            Disabled = true;
        }
    }

    public void enableSpecialDefense()
    {
        if (Type == "SpecialDefense")
        {
            Disabled = false;
        }
    }

    public void disableHealth()
    {
        if(Type == "Health")
        {
            Disabled=true;
        }
    }

    public void enableHealth()
    {
        if (Type == "Health")
        {
            Disabled = false;
        }
    }
    public void disableStamina()
    {
        if (Type == "Stamina")
        {
            Disabled = true;
        }
    }

    public void enableStamina()
    {
        if (Type == "Stamina")
        {
            Disabled = false;
        }
    }
    #endregion

}
