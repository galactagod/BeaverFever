using Godot;
using System;

public class Player : Node2D
{

    int skillPoints = 0;

    /***********************************/
    //          Skill Counter
    /***********************************/

    // Strength 
    int punchSkill = 0;
    int clawSkill = 0;
    int jawsSkill = 0;


    // Body 
    int armorSkill;
    int bootSkill;
    int wisdomSkill;


    // Weapon 
    int swordSkill;
    int daggerSkill;
    int bowArrowSkill;


    // Passive 
    int nightSkill;
    int leavesSkill;
    int vampireSkill;


    /***********************************/
    //          Buttons
    /***********************************/

    // Strength buttons
    TextureButton punchBtn;
    TextureButton clawsBtn;
    TextureButton jawsBtn;


    // Body Buttons
    TextureButton armorBtn;
    TextureButton bootBtn;
    TextureButton wisdomBtn;


    // Weapon Buttons
    TextureButton swordBtn;
    TextureButton daggerBtn;
    TextureButton bowArrowBtn;

    // Passive Buttons
    TextureButton nightBtn;
    TextureButton leavesBtn;
    TextureButton vampireBtn;



    /***********************************/
    //          Labels
    /***********************************/

    // Strength Labels
    Label punchLabel;
    Label clawsLabel;
    Label jawsLabel;


    // Body Labels
    Label armorLabel;
    Label bootLabel;
    Label wisdomLabel;


    // Weapon Labels
    Label swordLabel;
    Label daggerLabel;
    Label bowArrowLabel;

    // Passive Labels
    Label nightLabel;
    Label leavesLabel;
    Label vampireLabel;



    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("so");


        // strength 
        this.punchBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer/VBoxContainer/PunchBtn");
        this.punchLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer/VBoxContainer/punchLabel");
        this.punchBtn.Connect("pressed", this, "upgradePunch");

        this.clawsBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer2/VBoxContainer/clawsBtn");
        this.clawsBtn.Connect("pressed", this, "upgradeClaws");
        this.clawsLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer2/VBoxContainer/clawsLabel");


        this.jawsBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer3/VBoxContainer/jawsBtn");
        this.jawsBtn.Connect("pressed", this, "upgradeJaws");
        this.jawsLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_4 - strength/VBoxContainer3/VBoxContainer/jawsLabel");


        // body 
        this.armorBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_3 - body/VBoxContainer/VBoxContainer/armorBtn");
        this.armorBtn.Connect("pressed", this, "upgradeArmor");
        this.armorLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_3 - body/VBoxContainer/VBoxContainer/armorLabel");


        this.bootBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_3 - body/VBoxContainer2/VBoxContainer/bootBtn");
        this.bootBtn.Connect("pressed", this, "upgradeBoot");
        this.bootLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_3 - body/VBoxContainer2/VBoxContainer/bootLabel");


        this.wisdomBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_3 - body/VBoxContainer3/VBoxContainer/wisdomBtn");
        this.wisdomBtn.Connect("pressed", this, "upgradeWisdom");
        this.wisdomLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_3 - body/VBoxContainer3/VBoxContainer/wisdomLabel");


        // weapons
        this.swordBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer/VBoxContainer/swordBtn");
        this.swordBtn.Connect("pressed", this, "upgradeSword");
        this.swordLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer/VBoxContainer/swordLabel");

        this.daggerBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer2/VBoxContainer/daggerBtn");
        this.daggerBtn.Connect("pressed", this, "upgradeDagger");
        this.daggerLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer2/VBoxContainer/daggerLabel");

        this.bowArrowBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer3/VBoxContainer/bowBtn");
        this.bowArrowBtn.Connect("pressed", this, "upgradeBowArrowBtn");
        this.bowArrowLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - weapons/VBoxContainer3/VBoxContainer/bowLabel");


        // passives
        this.nightBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer/VBoxContainer/moonBtn");
        this.nightBtn.Connect("pressed", this, "upgradeNight");
        this.nightLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer/VBoxContainer/moonLabel");

        this.leavesBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer2/VBoxContainer/leavesBtn");
        this.leavesBtn.Connect("pressed", this, "upgradeLeaves");
        this.leavesLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer2/VBoxContainer/leavesLabel");

        this.vampireBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer3/VBoxContainer/prayingBtn");
        this.vampireBtn.Connect("pressed", this, "upgradeVampire");
        this.vampireLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_1 - passives/VBoxContainer3/VBoxContainer/prayingLabel");

    }


    void upgradePunch()
    {
        if (punchSkill < 3)
        {
            punchSkill++;
            this.punchLabel.Text = "Punch - Level " + punchSkill;
        }

        switch (punchSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/strength/punch/Attack Mod 2.png", this.punchBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/strength/punch/Attack Mod 3.png", this.punchBtn);
                break;
        }
    }

    void upgradeClaws()
    {
        if (clawSkill < 5)
        {
            clawSkill++;
            this.clawsLabel.Text = "Claws - Level " + clawSkill;
        }



        switch (clawSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 2.png", this.clawsBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 3.png", this.clawsBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 4.png", this.clawsBtn);
                break;
            case 4:
                changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 5.png", this.clawsBtn);
                break;
        }
    }

    void upgradeJaws()
    {
        if (jawsSkill < 5)
        {
            jawsSkill++;
            this.jawsLabel.Text = "Jaws - Level " + jawsSkill;

        }

        switch (jawsSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 2.png", this.jawsBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 3.png", this.jawsBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 4.png", this.jawsBtn);
                break;
            case 4:
                changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 5.png", this.jawsBtn);
                break;
        }
    }



    void upgradeArmor()
    {
        if (armorSkill < 4)
        {
            armorSkill++;
            this.armorLabel.Text = "Armor - Level " + armorSkill;

        }

        switch (armorSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/armor/Body 3 Mod 1.png", this.armorBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/armor/Body 3 Mod 2.png", this.armorBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/armor/Body 3 Mod 3.png", this.armorBtn);
                break;
        }
    }

    void upgradeBoot()
    {
        if (bootSkill < 4)
        {
            bootSkill++;
            this.bootLabel.Text = "Boot - Level " + bootSkill;

        }

        switch (bootSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 4.png", this.bootBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 6.png", this.bootBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 7.png", this.bootBtn);
                break;
        }
    }

    void upgradeWisdom()
    {
        if (wisdomSkill < 5)
        {
            wisdomSkill++;
            this.wisdomLabel.Text = "Wisdom - Level " + wisdomSkill;

        }

        switch (wisdomSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 2.png", this.wisdomBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 3.png", this.wisdomBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 4.png", this.wisdomBtn);
                break;
            case 4:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 5.png", this.wisdomBtn);
                break;
        }
    }


    void upgradeSword()
    {
        if (swordSkill < 4)
        {
            swordSkill++;
            this.swordLabel.Text = "Sword - Level " + swordSkill;
        }

        switch (swordSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/weapons/sword/Blade Mod 2.png", this.swordBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/weapons/sword/Blade Mod 3.png", this.swordBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/weapons/sword/Blade Mod 4.png", this.swordBtn);
                break;
        }
    }



    void upgradeDagger()
    {
        if (daggerSkill < 4)
        {
            daggerSkill++;
            this.daggerLabel.Text = "Dagger - Level " + daggerSkill;

        }

        switch (daggerSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/weapons/dagger/Dagger 1 Mod 2.png", this.daggerBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/weapons/dagger/Dagger 1 Mod 3.png", this.daggerBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/weapons/dagger/Dagger 1 Mod 4.png", this.daggerBtn);
                break;
        }
    }


    void upgradeBowArrowBtn()
    {
        if (bowArrowSkill < 4)
        {
            bowArrowSkill++;
            this.bowArrowLabel.Text = "Bow- Level " + bowArrowSkill;

        }

        switch (bowArrowSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/weapons/bow and arrow/Bow Mod 2.png", this.bowArrowBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/weapons/bow and arrow/Bow Mod 3.png", this.bowArrowBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/weapons/bow and arrow/Bow Mod 4.png", this.bowArrowBtn);
                break;
        }
    }


    void upgradeNight()
    {
        if (nightSkill < 4)
        {
            nightSkill++;
            this.nightLabel.Text = "Night - Level " + nightSkill;

        }

        switch (nightSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 2.png", this.nightBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 3.png", this.nightBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 4.png", this.nightBtn);
                break;
        }
    }



    void upgradeLeaves()
    {
        if (leavesSkill < 3)
        {
            leavesSkill++;
            this.leavesLabel.Text = "Leaves - Level " + leavesSkill;

        }

        switch (leavesSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.leavesBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.leavesBtn);
                break;
        }
    }


    void upgradeVampire()
    {
        if (vampireSkill < 4)
        {
            vampireSkill++;
            this.vampireLabel.Text = "Vampire - Level " + vampireSkill;

        }

        switch (vampireSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 2.png", this.vampireBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 3.png", this.vampireBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 4.png", this.vampireBtn);
                break;
        }
    }

















    void changeBtnTexture(String texturePath, TextureButton textureButton)
    {
        Texture texture = (Texture)GD.Load(texturePath);
        textureButton.TextureNormal = texture;
        textureButton.TexturePressed = texture;
        GD.Print("change texture" + texturePath);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
