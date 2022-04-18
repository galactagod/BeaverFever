using Godot;
using System;

public class SkillTree : Node2D
{



 // TODO
 // remove punch change it with 
    int skillPoints = 0;

    /***********************************/
    //          Skill Counter
    /***********************************/

    // Punch: Beaver performs a charged attack, 20pts, 40pts, 50pts
    // Claws: Beaver’s default attack, quick move, uses his claws, damage: 5pts, 10pts, 15pts 
    // Jaws: Beaver’s bite attack, I guess this would be the ultimate?? 60pts damage 

    // Strength 
    int punchSkill = 0; //done
    int clawSkill = 0; //done
    int jawsSkill = 0; //done

// Armor: Beaver’s defense against attacks, 5%, 10%, 30%
// Boots: Increase beavers passive movement speed, 5%, 10%, 15%

    // Body 
    int armorSkill = 0; //done
    int bootSkill = 0; //done
    


    // Coin increase: increases the XP gained from enemies, 3%, 7%, 10%

    // Passive 
    int graceSkill = 0;

    int bubbleBurstSkill = 0;
    int windHowlSkill = 0;



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





    // Passive Buttons
        TextureButton graceBtn;
    TextureButton windHowlBtn;
    TextureButton bubbleBurstBtn;



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




    // Passive Labels
    Label graceLabel;

    Label windHowlLabel;
    Label bubbleBurstLabel;



    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    PlayerData playerData;

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


 
        // passives
        
        this.graceBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer/VBoxContainer/coinBtn");
        this.graceBtn.Connect("pressed", this, "upgradeGrace");
        this.graceLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer/VBoxContainer/coinBtn2");


        this.windHowlBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer4/VBoxContainer/windHowlButton");
        this.windHowlBtn.Connect("pressed", this, "upgradeWindHowl");
        this.windHowlLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer4/VBoxContainer/windHowlLabel2");

        this.bubbleBurstBtn = this.GetNode<TextureButton>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer2/VBoxContainer/bubbleBurstBtn");
        this.bubbleBurstBtn.Connect("pressed", this, "upgradeBubbleBurst");
        this.bubbleBurstLabel = this.GetNode<Label>("Menu/Vertical Container/Tier_2 - passives/VBoxContainer2/VBoxContainer/bubbleBurstLabel");

        playerData = GetNode<PlayerData>("/root/PlayerData");
        initializeButtons();
    }


    void upgradePunch()
    {
        if (playerData.punchSkill < 2)
        {
            playerData.punchSkill++;
            playerData.skillBought("Attack Mod", playerData.punchSkill);
            this.punchLabel.Text = "Punch - Level " + playerData.punchSkill;
        }

        switch (playerData.punchSkill)
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
        if (playerData.clawSkill < 4)
        {
            playerData.clawSkill++;
            playerData.skillBought("Rip Mod", playerData.clawSkill);
            this.clawsLabel.Text = "Claws - Level " + playerData.clawSkill;
        }



        switch (playerData.clawSkill)
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
        if (playerData.jawsSkill < 4)
        {
            playerData.jawsSkill++;
            playerData.skillBought("Sharp Mod", playerData.jawsSkill);
            this.jawsLabel.Text = "Jaws - Level " + playerData.jawsSkill;

        }

        switch (playerData.jawsSkill)
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
        if (playerData.armorSkill < 3)
        {
            playerData.armorSkill++;
            playerData.skillBought("Body Mod", playerData.armorSkill);
            this.armorLabel.Text = "Armor - Level " + playerData.armorSkill;

        }

        switch (playerData.armorSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 1.png", this.armorBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 2.png", this.armorBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 3.png", this.armorBtn);
                break;
        }
    }

    void upgradeBoot()
    {
        if (playerData.bootSkill < 4)
        {
            playerData.bootSkill++;
            playerData.skillBought("Boots Mod", playerData.bootSkill);
            this.bootLabel.Text = "Boot - Level " + playerData.bootSkill;

        }

        switch (playerData.bootSkill)
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

    void upgradeGrace()
    {
        if (playerData.graceSkill < 5)
        {
            playerData.graceSkill++;
            playerData.skillBought("Book Mod", playerData.graceSkill);
            this.graceLabel.Text = "Wisdom - Level " + playerData.graceSkill;

        }

        switch (playerData.graceSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 2.png", this.graceBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 3.png", this.graceBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 4.png", this.graceBtn);
                break;
            case 4:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 5.png", this.graceBtn);
                break;
        }
    }






    void upgradeWindHowl()
    {
        if (playerData.windHowlSkill < 4)
        {
            playerData.windHowlSkill++;
            playerData.skillBought("Moon Mod", playerData.graceSkill);
            this.windHowlLabel.Text = "Night - Level " + playerData.windHowlSkill;

        }

        switch (playerData.windHowlSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 2.png", this.windHowlBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 3.png", this.windHowlBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 4.png", this.windHowlBtn);
                break;
        }
    }



    void upgradeBubbleBurst()
    {
        if (playerData.bubbleBurstSkill < 3)
        {
            playerData.bubbleBurstSkill++;
            playerData.skillBought("Leaf Mod", playerData.graceSkill);
            this.bubbleBurstLabel.Text = "Leaves - Level " + playerData.bubbleBurstSkill;

        }

        switch (playerData.bubbleBurstSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.bubbleBurstBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.bubbleBurstBtn);
                break;
        }
    }


    // void upgradeVampire()
    // {
    //     if (vampireSkill < 4)
    //     {
    //         vampireSkill++;
    //         this.vampireLabel.Text = "Vampire - Level " + vampireSkill;

    //     }

    //     switch (vampireSkill)
    //     {
    //         case 1:
    //             changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 2.png", this.vampireBtn);
    //             break;

    //         case 2:
    //             changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 3.png", this.vampireBtn);
    //             break;
    //         case 3:
    //             changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 4.png", this.vampireBtn);
    //             break;
    //     }
    // }



    void initializeButtons()
    {
        switch (playerData.punchSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/strength/punch/Attack Mod 2.png", this.punchBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/strength/punch/Attack Mod 3.png", this.punchBtn);
                break;
        }

        switch (playerData.clawSkill)
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

        switch (playerData.jawsSkill)
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

        switch (playerData.armorSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 1.png", this.armorBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 2.png", this.armorBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/armor/Body Mod 3.png", this.armorBtn);
                break;
        }

        switch (playerData.bootSkill)
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

        switch (playerData.graceSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 2.png", this.graceBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 3.png", this.graceBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 4.png", this.graceBtn);
                break;
            case 4:
                changeBtnTexture("res://assets/skills/body/wisdom/Book 1 Mod 5.png", this.graceBtn);
                break;
        }

        switch (playerData.windHowlSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 2.png", this.windHowlBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 3.png", this.windHowlBtn);
                break;
            case 3:
                changeBtnTexture("res://assets/skills/passives/night/Moon Mod 4.png", this.windHowlBtn);
                break;
        }

        switch (playerData.bubbleBurstSkill)
        {
            case 1:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.bubbleBurstBtn);
                break;

            case 2:
                changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.bubbleBurstBtn);
                break;
        }
    }













    void changeBtnTexture(String texturePath, TextureButton textureButton)
    {
        Texture texture = (Texture)GD.Load(texturePath);
        textureButton.TextureNormal = texture;
        textureButton.TexturePressed = texture;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
