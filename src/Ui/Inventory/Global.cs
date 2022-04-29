using Godot;
using System;
using System.Collections.Generic;


//where I will put all the items avaliable, and then in player data i will house the string names of the items and take them off as the player aquires them
public static class Global
{
    private static string assetRoute = "res://assets/";
    private static string skillAssetRoute = "res://assets/skills";
    public static List<PlayerData.item> itemsAvaliable = new List<PlayerData.item> {
        new PlayerData.item {
        name = "Small Health Potion",
        price = 300,
        texture = (Texture) GD.Load(assetRoute + "Small Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 10 Health"
        },
        new PlayerData.item {
        name = "Medium Health Potion",
        price = 600,
        texture = (Texture) GD.Load(assetRoute + "Medium Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 30 Health"
        },
        new PlayerData.item {
        name = "Large Health Potion",
        price = 900,
        texture = (Texture) GD.Load(assetRoute + "Large Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 60 Health"
        }

    };

    // A list of every skill avaliable, held as an "item" class
    public static List<PlayerData.item> skillsAvaliable = new List<PlayerData.item> {
        //Bubble burst = Attack Mod
        new PlayerData.item {
        name = "Attack Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/strength/bubble burst/bubble burst tier 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/strength/bubble burst/bubble burst tier 1.png",
        type = "skill",
        tooltip = "Shoots a water projectile toward the target. Attack power: 30"
        },
        new PlayerData.item {
        name = "Attack Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/strength/bubble burst/bubble burst tier 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/strength/bubble burst/bubble burst tier 2.png",
        type = "skill",
        tooltip = "Shoots a water projectile toward the target. Attack power: 35"
        },
        new PlayerData.item {
        name = "Attack Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/strength/bubble burst/bubble burst tier 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/strength/bubble burst/bubble burst tier 3.png",
        type = "skill",
        tooltip = "Shoots a water projectile toward the target. Attack power: 45"
        },
        new PlayerData.item {
        name = "Rip Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/strength/claws/Rip Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/claws/Rip Mod 2.png",
        type = "skill",
        tooltip = "Beaver slices nearby opponent. Attack power: 30"
        },
        new PlayerData.item {
        name = "Rip Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/strength/claws/Rip Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/claws/Rip Mod 3.png",
        type = "skill",
        tooltip = "Beaver bites nearby opponent. Attack power: 35"
        },
        new PlayerData.item {
        name = "Rip Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/strength/claws/Rip Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/claws/Rip Mod 4.png",
        type = "skill",
        tooltip = "Beaver bites nearby opponent. Attack power: 45"
        },
        new PlayerData.item {
        name = "Rip Mod",
        level = 4,
        texture = (Texture) GD.Load("res://assets/skills/strength/claws/Rip Mod 5.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/strength/claws/Rip Mod 5.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Leaf Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/passives/leaves/Leafs 1 Original.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/leaves/Leafs 1 Original.png",
        type = "skill",
        tooltip = "Regenerate 1% of hp over time."
        },
        new PlayerData.item {
        name = "Leaf Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/leaves/Leafs 1 Mod 1.png",
        type = "skill",
        tooltip = "Regenerate 2% of hp over time."
        },
        new PlayerData.item {
        name = "Leaf Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/passives/leaves/Leafs Mod 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/leaves/Leafs Mod 1.png",
        type = "skill",
        tooltip = "Regenerate 3% of hp over time."
        },
        new PlayerData.item {
        name = "Moon Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/passives/night/Moon Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/night/Moon Mod 2.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Moon Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/passives/night/Moon Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/night/Moon Mod 3.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Moon Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/passives/night/Moon Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/night/Moon Mod 4.png",
        type = "skill"
        },
        //done
        new PlayerData.item {
        name = "Book Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/passives/praying/Praying Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/praying/Praying Mod 2.png",
        type = "skill",
        tooltip = "Increases money gained by 1.5%."
        },
        new PlayerData.item {
        name = "Book Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/passives/praying/Praying Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/praying/Praying Mod 3.png",
        type = "skill",
        tooltip = "Increases money gained by 2%."
        },
        new PlayerData.item {
        name = "Book Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/passives/praying/Praying Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/praying/Praying Mod 4.png",
        type = "skill",
        tooltip = "Increases money gained by 3%."
        },
        new PlayerData.item {
        name = "Book Mod",
        level = 4,
        texture = (Texture) GD.Load("res://assets/skills/body/wisdom/Book 1 Mod 5.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/wisdom/Book 1 Mod 5.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Sharp Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/strength/teeth/Sharp Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/teeth/Sharp Mod 2.png",
        type = "skill",
        tooltip = "Beaver bites nearby opponent. Attack power: 45"
        },
        new PlayerData.item {
        name = "Sharp Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/strength/teeth/Sharp Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/teeth/Sharp Mod 3.png",
        type = "skill",
        tooltip = "Beaver bites nearby opponent. Attack power: 50"
        },
        new PlayerData.item {
        name = "Sharp Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/strength/teeth/Sharp Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/teeth/Sharp Mod 4.png",
        type = "skill",
        tooltip = "Beaver bites nearby opponent. Attack power: 60"
        },
        new PlayerData.item {
        name = "Sharp Mod",
        level = 4,
        texture = (Texture) GD.Load("res://assets/skills/strength/teeth/Sharp Mod 5.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/strength/teeth/Sharp Mod 5.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Boots Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/body/boots/Boots 1 Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/boots/Boots 1 Mod 4.png",
        type = "skill",
        tooltip = "Increases speed by 5%."
        },
        new PlayerData.item {
        name = "Boots Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/body/boots/Boots 1 Mod 6.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/boots/Boots 1 Mod 6.png",
        type = "skill",
        tooltip = "Increases speed by 10%."
        },
        new PlayerData.item {
        name = "Boots Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/body/boots/Boots 1 Mod 7.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/boots/Boots 1 Mod 7.png",
        type = "skill",
        tooltip = "Increases speed by 20%."
        },


        //done
        new PlayerData.item {
        name = "Body Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 1.png"),
        amountOnStat = new List<string> {"1.05"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 1.png",
        type = "skill",
        tooltip = "Boosts defense by 5%."
        },
        new PlayerData.item {
        name = "Body Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 2.png"),
        amountOnStat = new List<string> {"1.10"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 2.png",
        type = "skill",
        tooltip = "Boosts defense by 10%."
        },
        new PlayerData.item {
        name = "Body Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 3.png"),
        amountOnStat = new List<string> {"1.30"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 3.png",
        type = "skill",
        tooltip = "Boosts defense by 30%."
        }
    };

    public static List<PlayerData.item> itemTemplates = new List<PlayerData.item> {
        new PlayerData.item {
        name = "Iron Broadsword",
        price = 400,
        texture = (Texture) GD.Load(assetRoute + "Iron Broadsword.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"8"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "A basic broadsword for the common traveler."
        },
        new PlayerData.item {
        name = "Small Health Potion",
        price = 300,
        texture = (Texture) GD.Load(assetRoute + "Small Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 10 Health"
        },
        new PlayerData.item {
        name = "Medium Health Potion",
        price = 600,
        texture = (Texture) GD.Load(assetRoute + "Medium Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 30 Health"
        },
        new PlayerData.item {
        name = "Large Health Potion",
        price = 900,
        texture = (Texture) GD.Load(assetRoute + "Large Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 60 Health"
        },
        new PlayerData.item {
        name = "Sword of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Sword of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"20", "20"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Madness flows from this sword, mixing each aspect of offense to boost both by a large amount."
        },
        new PlayerData.item {
        name = "Sword of Malice",
        texture = (Texture) GD.Load(assetRoute + "Sword of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"25"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Ill intentions leak from this blade forged from the evil vines of the forest."
        },
        new PlayerData.item {
        name = "Staff of Malice",
        texture = (Texture) GD.Load(assetRoute + "Staff of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"25"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Ill intentions leak from this staff forged from the evil vines of the forest."
        },
        new PlayerData.item {
        name = "Beaver Tribe Horn",
        texture = (Texture) GD.Load(assetRoute + "Beaver Tribe Horn.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"10","10"},
        whichStat = new List<string> {"Attack", "Defense"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "The ancient talisman of the Beaver Tribe. Holds ferocious energy."
        },
        new PlayerData.item {
        name = "Amulet of Malice",
        texture = (Texture) GD.Load(assetRoute + "Amulet of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"20"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Necklace",
        type = "item",
        tooltip = "A necklace forged from the willpower of ill intentions."
        },
        new PlayerData.item {
        name = "Amulet of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Amulet of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"15", "15"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+","+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Necklace",
        type = "item",
        tooltip = "This necklace overflows with offensive power."
        },
        new PlayerData.item {
        name = "Talisman of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Talisman of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"15", "15"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+","+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "This heirloom overflows with offensive power."
        },
        new PlayerData.item {
        name = "Ring of Malice",
        texture = (Texture) GD.Load(assetRoute + "Ring of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"20"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "Made from the malice of snakes, this ring contains evil power beyond comprehention."
        },
        //look at this
        new PlayerData.item {
        name = "Sasuke's Blade",
        texture = (Texture) GD.Load(assetRoute + "Sasuke's Blade.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"15", "15"},
        whichStat = new List<string> {"Attack", "Defense"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "The sword containing the souls of the fallen Uchiha Clan. Multiverse?"
        }
    };

    public static List<EnemyTemplate> enemyTemplates = new List<EnemyTemplate>
    {
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 1,
            health = 10,
            attack = 3,
            defense = 5,
            spAttack = 3,
            spDefense = 5
        },
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 2,
            health = 14,
            attack = 7,
            defense = 5,
            spAttack = 7,
            spDefense = 5
        },
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 3,
            health = 16,
            attack = 12,
            defense = 5,
            spAttack = 12,
            spDefense = 5
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 1,
            health = 6,
            attack = 7,
            defense = 1,
            spAttack = 7,
            spDefense = 1
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 2,
            health = 9,
            attack = 9,
            defense = 2,
            spAttack = 9,
            spDefense = 2
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 3,
            health = 11,
            attack = 11,
            defense = 3,
            spAttack = 11,
            spDefense = 3
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 1,
            health = 10,
            attack = 10,
            defense = 4,
            spAttack = 10,
            spDefense = 4
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 2,
            health = 15,
            attack = 15,
            defense = 6,
            spAttack = 15,
            spDefense = 6
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 3,
            health = 20,
            attack = 20,
            defense = 8,
            spAttack = 20,
            spDefense = 8
        }
    };
}
