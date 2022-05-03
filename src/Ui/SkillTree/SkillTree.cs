using Godot;
using System;
using System.Collections.Generic;

public class SkillTree: Node2D {

  // TODO
  // remove punch change it with 
  int skillPoints = 1000000;

  /***********************************/
  //          Skill Counter
  /***********************************/

  // Strength: done

  // Slice - Tiers 3 tiers
  // Tier 1 - Attack power: 30, Exp Cost: 500
  // Tier 2 - Attack power: 35, Exp Cost: 1000
  // Tier 3 - Attack power: 45, Exp Cost: 3000
  // Explanation: Beaver slices nearby opponent
  int sliceSkill = 0;

  // Crunch - Tiers 3 tiers
  // Tier 1 - Attack power: 45, Exp Cost: 1000
  // Tier 2 - Attack power: 50, Exp Cost: 2000
  // Tier 3 - Attack power: 60, Exp Cost: 4000
  // Explanation: Beaver Bites nearby opponent
  int crunchSkill = 0;

  // Bubble Burst - Tiers 3 tiers
  // Tier 1 - Attack power: 30, Exp Cost: 500
  // Tier 2 - Attack power: 35, Exp Cost: 1000
  // Tier 3 - Attack power: 45, Exp Cost: 3000
  // Explanation: Shoots a water projectile towards target
  int bubbleBurstSkill = 0;

  // ****************************************************
  // Body
  // ****************************************************
  // Aegis - Tiers 3 tiers
  // Tier 1 - Attack power: 5%, Exp Cost: 1000
  // Tier 2 - Attack power: 10%, Exp Cost: 1500
  // Tier 3 - Attack power: 20%, Exp Cost: 2000
  // Explanation: Increases defense temporarily
  int aegisSkill = 0;

  // Accelerate - Tiers 3 tiers
  // Tier 1 - Attack power: 5%, Exp Cost: 1000
  // Tier 2 - Attack power: 10%, Exp Cost: 1500
  // Tier 3 - Attack power: 20%, Exp Cost: 2000
  // Explanation: Increases Speed temporarily
  int accelerateSkill = 0;

  // ****************************************************
  //  Passive  
  // ****************************************************
  // Grace - Tiers 3 tiers
  // Tier 1 - Attack power: 1.5%, Exp Cost: 1000
  // Tier 2 - Attack power: 2%, Exp Cost: 2000
  // Tier 3 - Attack power: 3%, Exp Cost: 3000
  // Explanation: Money multiplier
  int graceSkill = 0;

  // Regeneration - Tiers 3 tiers
  // Tier 1 - Attack power: 1%, Exp Cost: 1500
  // Tier 2 - Attack power: 2%, Exp Cost: 2500
  // Tier 3 - Attack power: 3%, Exp Cost: 5000
  // Explanation: Regenerate a % of a hp over time
  int regenerationSkill = 0;

  /***********************************/
  //          Buttons
  /***********************************/

  // Strength buttons
  TextureButton bubbleBurstBtn;
  TextureButton sliceBtn;
  TextureButton crunchBtn;

  // Body Buttons
  TextureButton aegisBtn;
  TextureButton accelerateBtn;

  // Passive Buttons
  TextureButton graceBtn;
  TextureButton regenerationBtn;

  /***********************************/
  //          Labels
  /***********************************/

  // Strength Labels
  Label bubbleBurstLabel;
  Label sliceLabel;
  Label crunchLabel;

  // Body Labels
  Label aegisLabel;
  Label accelerateLabel;

  // Passive Labels
  Label graceLabel;

  Label regenerationLabel;

  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  PlayerData playerData;
  PlayerStats playerStats;


    private List<int> bubbleCostList = new List<int> { 500, 1000, 3000, -1};
    private List<int> sliceCostList = new List<int> { 500, 1000, 3000, -1};
    private List<int> crunchCostList = new List<int> { 1000, 2000, 4000, -1};

    private List<int> aegisCostList = new List<int> { 1000, 1500, 2000, -1};
    private List<int> accelerateCostList = new List<int> { 1000, 1500, 2000, -1};

    private List<int> graceCostList = new List<int> { 1000, 2000, 3000, -1};
    private List<int> regenerationCostList = new List<int> { 1500, 2500, 5000, -1};




    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
    GD.Print("Init Skill Tree");

    // strength 
    this.bubbleBurstBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer/VBoxContainer/bubbleBurstBtn");
    this.bubbleBurstLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer/VBoxContainer/bubbleBurstLabel");
    this.bubbleBurstBtn.Connect("pressed", this, "upgradeBubbleBurst");

    this.sliceBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer2/VBoxContainer/sliceBtn");
    this.sliceBtn.Connect("pressed", this, "upgradeSlice");
    this.sliceLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer2/VBoxContainer/sliceLabel");

    this.crunchBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer3/VBoxContainer/crunchBtn");
    this.crunchBtn.Connect("pressed", this, "upgradeCrunch");
    this.crunchLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_4 - strength/VBoxContainer3/VBoxContainer/crunchLabel");

    // body 
    this.aegisBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_3 - body/VBoxContainer/VBoxContainer/aegisBtn");
    this.aegisBtn.Connect("pressed", this, "upgradeAegis");
    this.aegisLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_3 - body/VBoxContainer/VBoxContainer/aegisLabel");

    this.accelerateBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_3 - body/VBoxContainer2/VBoxContainer/accelerateBtn");
    this.accelerateBtn.Connect("pressed", this, "upgradeAccelerate");
    this.accelerateLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_3 - body/VBoxContainer2/VBoxContainer/accelerateLabel");

    // passives

    this.graceBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_2 - passives/VBoxContainer/VBoxContainer/graceBtn");
    this.graceBtn.Connect("pressed", this, "upgradeGrace");
    this.graceLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_2 - passives/VBoxContainer/VBoxContainer/graceLabel");

    this.regenerationBtn = this.GetNode < TextureButton > ("Menu/Vertical Container/Tier_2 - passives/VBoxContainer2/VBoxContainer/regenerationBtn");
    this.regenerationBtn.Connect("pressed", this, "upgradeRegeneration");
    this.regenerationLabel = this.GetNode < Label > ("Menu/Vertical Container/Tier_2 - passives/VBoxContainer2/VBoxContainer/regenerationLabel");

    playerData = GetNode < PlayerData > ("/root/PlayerData");
    playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    initializeButtons();
    ResetLabel();
  }

    //done
  void upgradeBubbleBurst() {
    if (playerData.bubbleBurstSkill < 3) {
        switch (playerData.bubbleBurstSkill)
        {
            case 0:
                if (experiencePoints(500))
                {
                    changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 1.png", this.bubbleBurstBtn);
                }
                else
                {
                    return;
                }

                break;

            case 1:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 2.png", this.bubbleBurstBtn);
                }
                else
                {
                    return;
                }

                break;

            case 2:
                if (experiencePoints(3000))
                {
                    changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 3.png", this.bubbleBurstBtn);
                }
                else
                {
                    return;
                }

                break;
        }
        playerData.bubbleBurstSkill++;
        playerData.skillBought("BubbleBurst", playerData.bubbleBurstSkill);
            ResetLabel();
    }

    
  }

    //done
  void upgradeSlice() {
    if (playerData.sliceSkill < 3) {
        switch (playerData.sliceSkill)
        {
            case 0:
                if (experiencePoints(500))
                {
                    changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 2.png", this.sliceBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 3.png", this.sliceBtn);
                }
                else
                {
                    return;

                }

                break;
            case 2:
                if (experiencePoints(3000))
                {
                    changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 4.png", this.sliceBtn);
                }
                else
                {
                    return;

                }
                break;
        }
        playerData.sliceSkill++;
        playerData.skillBought("Slice", playerData.sliceSkill);
            ResetLabel();
    }
    }

    

  void upgradeCrunch() {
    if (playerData.crunchSkill < 3) {
        switch (playerData.crunchSkill)
        {
            case 0:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 2.png", this.crunchBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(2000))
                {
                    changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 3.png", this.crunchBtn);
                }
                else
                {
                    return;

                }

                break;
            case 2:
                if (experiencePoints(4000))
                {
                    changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 4.png", this.crunchBtn);
                }
                else
                {
                    return;

                }
                break;
        }
        playerData.crunchSkill++;   
        playerData.skillBought("Crunch", playerData.crunchSkill);
        ResetLabel();

            
    }

    }

    //done
  void upgradeAegis() {
    if (playerData.aegisSkill < 3) {
        switch (playerData.aegisSkill)
        {
            case 0:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/body/aegis/Body Mod 1.png", this.aegisBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(1500))
                {
                    changeBtnTexture("res://assets/skills/body/aegis/Body Mod 2.png", this.aegisBtn);
                }
                else
                {
                    return;

                }
                break;
            case 2:
                if (experiencePoints(2000))
                {
                    changeBtnTexture("res://assets/skills/body/aegis/Body Mod 3.png", this.aegisBtn);
                }
                else
                {
                    return;

                }

                break;
        }
        playerData.aegisSkill++;
        playerData.skillBought("Aegis", playerData.aegisSkill);
            ResetLabel();
    }

    
  }

    //done
  void upgradeAccelerate() {
    if (playerData.accelerateSkill < 3) {
        switch (playerData.accelerateSkill)
        {
            case 0:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 4.png", this.accelerateBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(1500))
                {
                    changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 6.png", this.accelerateBtn);
                }
                else
                {
                    return;

                }

                break;
            case 2:
                if (experiencePoints(2000))
                {
                    changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 7.png", this.accelerateBtn);
                }
                else
                {
                    return;

                }

                break;
        }
        playerData.accelerateSkill++;
        playerData.skillBought("Accelerate", playerData.accelerateSkill);
            ResetLabel();
    }
    }

    //done
  void upgradeGrace() {
    if (playerData.graceSkill < 3) {
        switch (playerData.graceSkill)
        {
            case 0:
                if (experiencePoints(1000))
                {
                    changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 2.png", this.graceBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(2000))
                {
                    changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 3.png", this.graceBtn);
                }
                else
                {
                    return;

                }

                break;
            case 2:
                if (experiencePoints(3000))
                {
                    changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 4.png", this.graceBtn);
                }
                else
                {
                    return;

                }

                break;
        }
        playerData.graceSkill++;
        playerData.skillBought("Grace", playerData.graceSkill);
            ResetLabel();
    }

   
  }

 
//done
  void upgradeRegeneration() {
    if (playerData.regenerationSkill < 3) {
        switch (playerData.regenerationSkill)
        {
            case 0:
                if (experiencePoints(1500))
                {
                    changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Original.png", this.regenerationBtn);
                }
                else
                {
                    return;

                }

                break;

            case 1:
                if (experiencePoints(2500))
                {
                    changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.regenerationBtn);
                }
                else
                {
                    return;

                }
                break;

            case 2:
                if (experiencePoints(5000))
                {
                    changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.regenerationBtn);
                }
                else
                {
                    return;

                }
                break;
        }
      playerData.regenerationSkill++;
      playerData.skillBought("Regeneration", playerData.regenerationSkill);
            ResetLabel();
    }
  }

  bool experiencePoints(int xp) {
    bool unlocked = false;
    if (playerStats.Exp >= xp) {
            playerStats.Exp -= xp;
            playerStats.ChangeExp(0);
      unlocked = true;
    } else {
      GD.Print("Error, player does not have enough XP for this skill");
      // some error should occur

    }

    return unlocked;
  }

  void initializeButtons() {

    switch (playerData.bubbleBurstSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 1.png", this.bubbleBurstBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 2.png", this.bubbleBurstBtn);
      break;

    case 3:
      changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 3.png", this.bubbleBurstBtn);
      break;
    }

    switch (playerData.sliceSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 2.png", this.sliceBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 3.png", this.sliceBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 4.png", this.sliceBtn);
      break;

    }

    switch (playerData.crunchSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 2.png", this.crunchBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 3.png", this.crunchBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 4.png", this.crunchBtn);
      break;
    }

    switch (playerData.aegisSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/body/aegis/Body Mod 1.png", this.aegisBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/body/aegis/Body Mod 2.png", this.aegisBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/body/aegis/Body Mod 3.png", this.aegisBtn);
      break;
    }

    switch (playerData.accelerateSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 4.png", this.accelerateBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 6.png", this.accelerateBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 7.png", this.accelerateBtn);
      break;
    }

    switch (playerData.graceSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 2.png", this.graceBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 3.png", this.graceBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 4.png", this.graceBtn);
      break;

    }

    switch (playerData.regenerationSkill) {
    case 1:
      changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Original.png", this.regenerationBtn);
      break;

    case 2:
      changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.regenerationBtn);
      break;
    case 3:
      changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.regenerationBtn);
      break;
    }

    
    this.regenerationLabel.Text = "Regeneration - Level " + playerData.regenerationSkill + "\nCost of next level: " + ((regenerationCostList[playerData.regenerationSkill] == -1) ? "Maxed" : regenerationCostList[playerData.regenerationSkill].ToString());
    this.graceLabel.Text = "Grace - Level " + playerData.graceSkill + "\nCost of next level: " + ((graceCostList[playerData.graceSkill] == -1) ? "Maxed" : graceCostList[playerData.graceSkill].ToString());
    this.aegisLabel.Text = "Aegis - Level " + playerData.aegisSkill + "\nCost of next level: " + ((aegisCostList[playerData.aegisSkill] == -1) ? "Maxed" : aegisCostList[playerData.aegisSkill].ToString());
    this.bubbleBurstLabel.Text = "Bubble Burst - Level " + playerData.bubbleBurstSkill + "\nCost of next level: " + ((bubbleCostList[playerData.bubbleBurstSkill] == -1) ? "Maxed" : bubbleCostList[playerData.bubbleBurstSkill].ToString());
    this.sliceLabel.Text = "Slice - Level " + playerData.sliceSkill + "\nCost of next level: " + ((sliceCostList[playerData.sliceSkill] == -1) ? "Maxed" : sliceCostList[playerData.sliceSkill].ToString());
    this.crunchLabel.Text = "Crunch - Level " + playerData.crunchSkill + "\nCost of next level: " + ((crunchCostList[playerData.crunchSkill] == -1) ? "Maxed" : crunchCostList[playerData.crunchSkill].ToString());
    this.accelerateLabel.Text = "Accelerate - Level " + playerData.accelerateSkill + "\nCost of next level: " + ((accelerateCostList[playerData.accelerateSkill] == -1) ? "Maxed" : accelerateCostList[playerData.accelerateSkill].ToString());

    }

    void ResetLabel()
    {
        Node label = GetNode("Menu/Vertical Container/Skill Points/VBoxContainer/RichTextLabel");
        label.Set("text", playerStats.Exp.ToString());

        this.regenerationLabel.Text = "Regeneration - Level " + playerData.regenerationSkill + "\nCost of next level: " + ((regenerationCostList[playerData.regenerationSkill] == -1) ? "Maxed" : regenerationCostList[playerData.regenerationSkill].ToString());
        this.graceLabel.Text = "Grace - Level " + playerData.graceSkill + "\nCost of next level: " + ((graceCostList[playerData.graceSkill] == -1) ? "Maxed" : graceCostList[playerData.graceSkill].ToString());
        this.aegisLabel.Text = "Aegis - Level " + playerData.aegisSkill + "\nCost of next level: " + ((aegisCostList[playerData.aegisSkill] == -1) ? "Maxed" : aegisCostList[playerData.aegisSkill].ToString());
        this.bubbleBurstLabel.Text = "Bubble Burst - Level " + playerData.bubbleBurstSkill + "\nCost of next level: " + ((bubbleCostList[playerData.bubbleBurstSkill] == -1) ? "Maxed" : bubbleCostList[playerData.bubbleBurstSkill].ToString());
        this.sliceLabel.Text = "Slice - Level " + playerData.sliceSkill + "\nCost of next level: " + ((sliceCostList[playerData.sliceSkill] == -1) ? "Maxed" : sliceCostList[playerData.sliceSkill].ToString());
        this.crunchLabel.Text = "Crunch - Level " + playerData.crunchSkill + "\nCost of next level: " + ((crunchCostList[playerData.crunchSkill] == -1) ? "Maxed" : crunchCostList[playerData.crunchSkill].ToString());
        this.accelerateLabel.Text = "Accelerate - Level " + playerData.accelerateSkill + "\nCost of next level: " + ((accelerateCostList[playerData.accelerateSkill] == -1) ? "Maxed" : accelerateCostList[playerData.accelerateSkill].ToString());
    }

  void changeBtnTexture(String texturePath, TextureButton textureButton) {
    Texture texture = (Texture) GD.Load(texturePath);
    textureButton.TextureNormal = texture;
    textureButton.TexturePressed = texture;
  }
  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //      
  //  }
}