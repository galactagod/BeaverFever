using Godot;
using System;
using System.Collections.Generic;

public class MainStatPage : Control
{
    #region Variables
    private PlayerData playerData;
    // Stats themselves
    private int attack = 0;
    private int defense = 0;
    private int specialAttack = 0;
    private int specialDefense = 0;
    private int stamina = 0;
    private int health = 0;

    // Stats points
    private int totalStatPoints = 0;
    private int attackStatPoints = 0;
    private int defenseStatPoints = 0;
    private int specialAttackStatPoints = 0;
    private int specialDefenseStatPoints = 0;
    private int staminaStatPoints = 0;
    private int healthStatPoints = 0;
    #endregion

    #region Signals
    // Signals for when there are 0 (and not 0) total stat points to be added
    [Signal]
    public delegate void statPointsEmptied();
    [Signal]
    public delegate void statPointsFilled();

    // Signals for when there are 0 (and not 0) of each stat type
    [Signal]
    public delegate void attackStatPointsEmptied();
    [Signal]
    public delegate void attackStatPointsFilled();
    [Signal]
    public delegate void defenseStatPointsEmptied();
    [Signal]
    public delegate void defenseStatPointsFilled();
    [Signal]
    public delegate void specialAttackStatPointsEmptied();
    [Signal]
    public delegate void specialAttackStatPointsFilled();
    [Signal]
    public delegate void specialDefenseStatPointsEmptied();
    [Signal]
    public delegate void specialDefenseStatPointsFilled();
    [Signal]
    public delegate void staminaStatPointsEmptied();
    [Signal]
    public delegate void staminaStatPointsFilled();
    [Signal]
    public delegate void healthStatPointsEmptied();
    [Signal]
    public delegate void healthStatPointsFilled();
    #endregion

    #region Godot Overrides
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Add and Subtract Buttons
        var attackSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground/SubtractAttackButton");
        attackSubtractButton.Connect("statPointsSubtract", this, "subtractStats");
        
        var attackAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground/AddAttackButton");
        attackAddButton.Connect("statPointsAdd", this, "addStats");

        var defenseSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground2/SubtractDefenseButton");
        defenseSubtractButton.Connect("statPointsSubtract", this, "subtractStats");

        var defenseAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground2/AddDefenseButton");
        defenseAddButton.Connect("statPointsAdd", this, "addStats");

        var specialAttackSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground3/SubtractSpecialAttackButton");
        specialAttackSubtractButton.Connect("statPointsSubtract", this, "subtractStats");

        var specialAttackAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground3/AddSpecialAttackButton");
        specialAttackAddButton.Connect("statPointsAdd", this, "addStats");

        var specialDefenseSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground4/SubtractSpecialDefenseButton");
        specialDefenseSubtractButton.Connect("statPointsSubtract", this, "subtractStats");

        var specialDefenseAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground4/AddSpecialDefenseButton");
        specialDefenseAddButton.Connect("statPointsAdd", this, "addStats");

        var healthSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground5/SubtractHealthPoints");
        healthSubtractButton.Connect("statPointsSubtract", this, "subtractStats");

        var healthAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground5/AddHealthPoints");
        healthAddButton.Connect("statPointsAdd", this, "addStats");


        var staminaSubtractButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground6/SubtractStaminaPoints");
        staminaSubtractButton.Connect("statPointsSubtract", this, "subtractStats");

        var staminaAddButton = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground6/AddStaminaPoints");
        staminaAddButton.Connect("statPointsAdd", this, "addStats");






        // Save Button
        var saveButton = GetNode("Background/VBoxContainer/Buttons/SaveButton");
        saveButton.Connect("saveClicked", this, "savePoints");

        playerData = GetNode<PlayerData>("/root/PlayerData");

        attack = playerData.PlayerAttack;
        defense = playerData.PlayerDefense;
        specialAttack = playerData.PlayerSpAttack;
        specialDefense = playerData.PlayerSpDefense;
        health = playerData.PlayerHealth;
        stamina = playerData.PlayerStamina;
        totalStatPoints = playerData.PlayerTotalPoints;

        // Stat Labels

        initializingLabels();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Disabling buttons adding stat points if the amount that the player has (before saving) equals 0
        if(totalStatPoints == 0)
        {
            emitStatsEmptied();
        }
        else
        {
            emitStatsFilled();
        }

        if(attackStatPoints == 0)
        {
            EmitSignal("attackStatPointsEmptied");
        }
        else
        {
            EmitSignal("attackStatPointsFilled");
        }

        if(defenseStatPoints == 0)
        {
            EmitSignal("defenseStatPointsEmptied");
        }
        else
        {
            EmitSignal("defenseStatPointsFilled");
        }
        if(specialAttackStatPoints == 0)
        {
            EmitSignal("specialAttackStatPointsEmptied");
        }
        else
        {
            EmitSignal("specialAttackStatPointsFilled");
        }
        if(specialDefenseStatPoints == 0)
        {
            EmitSignal("specialDefenseStatPointsEmptied");
        }
        else
        {
            EmitSignal("specialDefenseStatPointsFilled");
        }
        if(healthStatPoints == 0)
        {
            EmitSignal("healthStatPointsEmptied");
        }
        else
        {
            EmitSignal("healthStatPointsFilled");
        }
        if(staminaStatPoints == 0)
        {
            EmitSignal("staminaStatPointsEmptied");
        }
        else
        {
            EmitSignal("staminaStatPointsFilled");
        }
            
    }
    #endregion

    #region Emit Statements
    public void emitStatsEmptied()
    {
        EmitSignal("statPointsEmptied");
    }

    public void emitStatsFilled()
    {
        EmitSignal("statPointsFilled");
    }
    #endregion

    #region Adding Stats
    public void addStats(string type)
    {
        if(type == "Attack")
        {
            attackStatPoints++;
            Node attackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground/Label");
            attackPointsLabel.Set("text", "AttackPoints: " + attackStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node attackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Attack");
            attackLabel.Set("text", "Attack: " + attack + " -> " + (attack + attackStatPoints));
        }
        else if (type == "Defense")
        {
            defenseStatPoints++;
            Node defensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground2/Label");
            defensePointsLabel.Set("text", "DefensePoints: " + defenseStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node defenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Defense");
            defenseLabel.Set("text", "Defense: " + defense + " -> " + (defense + defenseStatPoints));
        }
        else if (type == "SpecialAttack")
        {
            specialAttackStatPoints++;
            Node specialAttackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground3/Label");
            specialAttackPointsLabel.Set("text", "SpecialAttackPoints: " + specialAttackStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node specialAttackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpAttack");
            specialAttackLabel.Set("text", "Special Attack: " + specialAttack + " -> " + (specialAttack + specialAttackStatPoints));
        }
        else if (type == "SpecialDefense")
        {
            specialDefenseStatPoints++;
            Node specialDefensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground4/Label");
            specialDefensePointsLabel.Set("text", "Special Defense Points: " + specialDefenseStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node specialDefenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpDefense");
            specialDefenseLabel.Set("text", "Special Defense: " + specialDefense + " -> " + (specialDefense + specialDefenseStatPoints));
        }
        else if (type == "Health")
        {
            healthStatPoints++;
            Node healthPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground5/Label");
            healthPointsLabel.Set("text", "HealthPoints: " + healthStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node healthLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Health");
            healthLabel.Set("text", "Health: " + health + " -> " + (health + healthStatPoints));
        }
        else if (type == "Stamina")
        {
            staminaStatPoints++;
            Node staminaPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground6/Label");
            staminaPointsLabel.Set("text", "StaminaPoints: " + staminaStatPoints);
            totalStatPoints--;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node staminaLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Stamina");
            staminaLabel.Set("text", "Stamina: " + stamina + " -> " + (stamina + staminaStatPoints));
        }
    }
    #endregion

    #region Subtracting Stats
    public void subtractStats(string type)
    {
        if (type == "Attack")
        {
            attackStatPoints--;
            Node attackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground/Label");
            attackPointsLabel.Set("text", "AttackPoints: " + attackStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node attackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Attack");
            attackLabel.Set("text", "Attack: " + attack + " -> " + (attack + attackStatPoints));
        }
        else if (type == "Defense")
        {
            defenseStatPoints--;
            Node defensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground2/Label");
            defensePointsLabel.Set("text", "DefensePoints: " + defenseStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node defenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Defense");
            defenseLabel.Set("text", "Defense: " + defense + " -> " + (defense + defenseStatPoints));
        }
        else if (type == "SpecialAttack")
        {
            specialAttackStatPoints--;
            Node specialAttackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground3/Label");
            specialAttackPointsLabel.Set("text", "SpecialAttackPoints: " + specialAttackStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node specialAttackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpAttack");
            specialAttackLabel.Set("text", "Special Attack: " + specialAttack + " -> " + (specialAttack + specialAttackStatPoints));
        }
        else if (type == "SpecialDefense")
        {
            specialDefenseStatPoints--;
            Node specialDefensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground4/Label");
            specialDefensePointsLabel.Set("text", "Special Defense Points: " + specialDefenseStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node specialDefenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpDefense");
            specialDefenseLabel.Set("text", "Special Defense: " + specialDefense + " -> " + (specialDefense + specialDefenseStatPoints));
        }
        else if (type == "Health")
        {
            healthStatPoints--;
            Node healthPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground5/Label");
            healthPointsLabel.Set("text", "HealthPoints: " + healthStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node healthLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Health");
            healthLabel.Set("text", "Health: " + health + " -> " + (health + healthStatPoints));
        }
        else if (type == "Stamina")
        {
            staminaStatPoints--;
            Node staminaPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground6/Label");
            staminaPointsLabel.Set("text", "StaminaPoints: " + staminaStatPoints);
            totalStatPoints++;
            Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
            statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);
            Node staminaLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Stamina");
            staminaLabel.Set("text", "Stamina: " + stamina + " -> " + (stamina + staminaStatPoints));
        }
    }
    #endregion

    #region Saving Stats
    public void savePoints()
    {
        attack += attackStatPoints;
        defense += defenseStatPoints;
        specialAttack += specialAttackStatPoints;
        specialDefense += specialDefenseStatPoints;
        stamina += staminaStatPoints;
        health += healthStatPoints;

        playerData.PlayerAttack = attack;
        playerData.PlayerDefense = defense;
        playerData.PlayerHealth = health;
        playerData.PlayerTotalPoints = totalStatPoints;
        playerData.PlayerSpDefense = specialDefense;
        playerData.PlayerSpAttack = specialAttack;
        playerData.PlayerStamina = stamina;


        attackStatPoints = 0;
        defenseStatPoints = 0;
        specialAttackStatPoints = 0;
        specialDefenseStatPoints = 0;
        staminaStatPoints = 0;
        healthStatPoints = 0;

        //save stats to main stat page


        initializingLabels();
    }

    #endregion

    #region Helper Methods

    public void initializingLabels()
    {
        // Total Stat Points
        Node statPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/TotalStat");
        statPointsLabel.Set("text", "Total Stat Points to Use: " + totalStatPoints);

        // Attack
        Node attackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Attack");
        attackLabel.Set("text", "Attack: " + attack);

        Node attackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground/Label");
        attackPointsLabel.Set("text", "AttackPoints: " + attackStatPoints);

        // Defense
        Node defenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Defense");
        defenseLabel.Set("text", "Defense: " + defense);

        Node defensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground2/Label");
        defensePointsLabel.Set("text", "DefensePoints: " + defenseStatPoints);

        //Special Attack
        Node specialAttackPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground3/Label");
        specialAttackPointsLabel.Set("text", "SpecialAttackPoints: " + specialAttackStatPoints);

        Node specialAttackLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpAttack");
        specialAttackLabel.Set("text", "Special Attack: " + specialAttack);

        //Special Defense
        Node specialDefenseLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/SpDefense");
        specialDefenseLabel.Set("text", "Special Defense: " + specialDefense);

        Node specialDefensePointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground4/Label");
        specialDefensePointsLabel.Set("text", "Special Defense Points: " + specialDefenseStatPoints);

        //Stamina
        Node staminaLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Stamina");
        staminaLabel.Set("text", "Stamina: " + stamina);

        Node staminaPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground6/Label");
        staminaPointsLabel.Set("text", "StaminaPoints: " + staminaStatPoints);

        //Health
        Node healthLabel = GetNode("Background/VBoxContainer/HBoxContainer/StatBackground7/VBoxContainer/Health");
        healthLabel.Set("text", "Health: " + health);

        Node healthPointsLabel = GetNode("Background/VBoxContainer/HBoxContainer/MainStats/StatBackground5/Label");
        healthPointsLabel.Set("text", "HealthPoints: " + healthStatPoints);
    }
    #endregion
}