using Godot;
using System;

public class SkillTree: Node2D {

  // TODO
  // remove punch change it with 
  int skillPoints = 0;

  /***********************************/
  //          Skill Counter
  /***********************************/

  // Strength 

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
    initializeButtons();
  }

  void upgradeBubbleBurst() {
    if (playerData.bubbleBurstSkill < 4) {
      playerData.bubbleBurstSkill++;
      playerData.skillBought("Attack Mod", playerData.bubbleBurstSkill);
      this.bubbleBurstLabel.Text = "Bubble Burst - Level " + playerData.bubbleBurstSkill;
    }

    switch (playerData.bubbleBurstSkill) {
    case 1:
      if (experiencePoints(500)) {
        changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 1.png", this.bubbleBurstBtn);
      } else {
         playerData.bubbleBurstSkill--;
      }

      break;

    case 2:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 2.png", this.bubbleBurstBtn);
      } else {
        playerData.bubbleBurstSkill--;
      }

      break;

    case 3:
      if (experiencePoints(3000)) {
        changeBtnTexture("res://assets/skills/strength/bubble burst/bubble burst tier 3.png", this.bubbleBurstBtn);
      } else {
            playerData.bubbleBurstSkill++;
      }

      break;
    }
  }

  void upgradeSlice() {
    if (playerData.sliceSkill < 4) {
      playerData.sliceSkill++;

      playerData.skillBought("Rip Mod", playerData.sliceSkill);
      this.sliceLabel.Text = "Slice - Level " + playerData.sliceSkill;
    }

    switch (playerData.sliceSkill) {
    case 1:
      if (experiencePoints(500)) {
        changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 2.png", this.sliceBtn);
      } else {
      playerData.sliceSkill--;

      }

      break;

    case 2:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 3.png", this.sliceBtn);
      } else {
      playerData.sliceSkill--;

      }

      break;
    case 3:
      if (experiencePoints(3000)) {
        changeBtnTexture("res://assets/skills/strength/claws/Rip Mod 4.png", this.sliceBtn);
      } else {
      playerData.sliceSkill--;

      }


      break;

    }
  }

  void upgradeCrunch() {
    if (playerData.crunchSkill < 4) {
    playerData.crunchSkill++;   
      playerData.skillBought("Sharp Mod", playerData.crunchSkill);
      this.crunchLabel.Text = "Crunch - Level " + playerData.crunchSkill;

    }

    switch (playerData.crunchSkill) {
    case 1:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 2.png", this.crunchBtn);
      } else {
      playerData.crunchSkill--;

      }

      break;

    case 2:
      if (experiencePoints(2000)) {
        changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 3.png", this.crunchBtn);
      } else {
      playerData.crunchSkill--;

      }

      break;
    case 3:
      if (experiencePoints(4000)) {
        changeBtnTexture("res://assets/skills/strength/teeth/Sharp Mod 4.png", this.crunchBtn);
      } else {
      playerData.crunchSkill--;

      }
      break;
    }
  }

  void upgradeAegis() {
    if (playerData.aegisSkill < 4) {
      playerData.aegisSkill++;

      playerData.skillBought("Body Mod", playerData.aegisSkill);
      this.aegisLabel.Text = "Aegis - Level " + playerData.aegisSkill;

    }

    switch (playerData.aegisSkill) {
    case 1:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/body/aegis/Body Mod 1.png", this.aegisBtn);
      } else {
      playerData.aegisSkill--;

      }

      break;

    case 2:
      if (experiencePoints(1500)) {
        changeBtnTexture("res://assets/skills/body/aegis/Body Mod 2.png", this.aegisBtn);
      } else {
      playerData.aegisSkill--;

      }
      break;
    case 3:
      if (experiencePoints(2000)) {
        changeBtnTexture("res://assets/skills/body/aegis/Body Mod 3.png", this.aegisBtn);
      } else {
      playerData.aegisSkill--;

      }

      break;
    }
  }

  void upgradeAccelerate() {
    if (playerData.accelerateSkill < 4) {
        playerData.accelerateSkill++;

      playerData.skillBought("Boots Mod", playerData.accelerateSkill);
      this.accelerateLabel.Text = "Accelerate - Level " + playerData.accelerateSkill;

    }

    switch (playerData.accelerateSkill) {
    case 1:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 4.png", this.accelerateBtn);
      } else {
      playerData.accelerateSkill--;

      }

      break;

    case 2:
      if (experiencePoints(1500)) {
        changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 6.png", this.accelerateBtn);
      } else {
      playerData.accelerateSkill--;

      }

      break;
    case 3:
      if (experiencePoints(2000)) {
        changeBtnTexture("res://assets/skills/body/boots/Boots 1 Mod 7.png", this.accelerateBtn);
      } else {
      playerData.accelerateSkill--;

      }

      break;
    }
  }

  void upgradeGrace() {
    if (playerData.graceSkill < 4) {
      playerData.graceSkill++;
      playerData.skillBought("Book Mod", playerData.graceSkill);
      this.graceLabel.Text = "Wisdom - Level " + playerData.graceSkill;

    }

    switch (playerData.graceSkill) {
    case 1:
      if (experiencePoints(1000)) {
        changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 2.png", this.graceBtn);
      } else {
      playerData.graceSkill--;

      }

      break;

    case 2:
      if (experiencePoints(2000)) {
        changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 3.png", this.graceBtn);
      } else {
      playerData.graceSkill--;

      }

      break;
    case 3:
      if (experiencePoints(3000)) {
        changeBtnTexture("res://assets/skills/passives/praying/Praying Mod 4.png", this.graceBtn);
      } else {
      playerData.graceSkill--;

      }

      break;
    }
  }

 

  void upgradeRegeneration() {
    if (playerData.regenerationSkill < 4) {
        playerData.regenerationSkill++;
      playerData.skillBought("Leaf Mod", playerData.graceSkill);
      this.regenerationLabel.Text = "Regeneration - Level " + playerData.regenerationSkill;

    }

    switch (playerData.regenerationSkill) {
    case 1:
      if (experiencePoints(1500)) {
        changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Original.png", this.regenerationBtn);
      } else {
      playerData.regenerationSkill--;

      }

      break;

    case 2:
      if (experiencePoints(2500)) {
        changeBtnTexture("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png", this.regenerationBtn);
      } else {
      playerData.regenerationSkill--;

      }
      break;

    case 3:
      if (experiencePoints(5000)) {
        changeBtnTexture("res://assets/skills/passives/leaves/Leafs Mod 1.png", this.regenerationBtn);
      } else {
      playerData.regenerationSkill--;

      }
      break;
    }
  }

  bool experiencePoints(int xp) {
    bool unlocked = false;
    if (playerData.PlayerTotalPoints >= xp) {
      playerData.PlayerTotalPoints -= xp;
      unlocked = true;
    } else {
      GD.Print("Error, player does not have enough XP for this skill");
      // some error should occur

    }

    return unlocked;
  }



   //   void upgradeRegeneration() {
  //     if (playerData.regenerationSkill < 4) {
  //       playerData.regenerationSkill++;
  //       playerData.skillBought("Moon Mod", playerData.graceSkill);
  //       this.regenerationLabel.Text = "Night - Level " + playerData.regenerationSkill;

  //     }

  //     switch (playerData.regenerationSkill) {
  //     case 1:
  //       changeBtnTexture("res://assets/skills/passives/night/Moon Mod 2.png", this.regenerationBtn);
  //       break;

  //     case 2:
  //       changeBtnTexture("res://assets/skills/passives/night/Moon Mod 3.png", this.regenerationBtn);
  //       break;
  //     case 3:
  //       changeBtnTexture("res://assets/skills/passives/night/Moon Mod 4.png", this.regenerationBtn);
  //       break;
  //     }
  //   }
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