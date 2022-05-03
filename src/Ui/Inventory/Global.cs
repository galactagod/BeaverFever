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
        tooltip = "Restores 25 Health"
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
        tooltip = "Restores 55 Health"
        }

    };

    // A list of every skill avaliable, held as an "item" class
    public static List<PlayerData.item> skillsAvaliable = new List<PlayerData.item> {
        //Bubble burst = Attack Mod
        new PlayerData.item {
        name = "BubbleBurst",
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
        tooltip = "Shoots a water projectile toward the target. Attack power: 3 + Special Attack"
        },
        new PlayerData.item {
        name = "BubbleBurst",
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
        tooltip = "Shoots a water projectile toward the target. Attack power: 5 + Special Attack"
        },
        new PlayerData.item {
        name = "BubbleBurst",
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
        tooltip = "Shoots a water projectile toward the target. Attack power: 8 + Special Attack"
        },
        new PlayerData.item {
        name = "Slice",
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
        tooltip = "Beaver slices nearby opponent. Attack power: 3 + Attack"
        },
        new PlayerData.item {
        name = "Slice",
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
        tooltip = "Beaver bites nearby opponent. Attack power: 5 + Attack"
        },
        new PlayerData.item {
        name = "Slice",
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
        tooltip = "Beaver bites nearby opponent. Attack power: 8 + Attack"
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
        name = "Regeneration",
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
        name = "Regeneration",
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
        name = "Regeneration",
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
        name = "Grace",
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
        name = "Grace",
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
        name = "Grace",
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
        name = "Grace",
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
        name = "Crunch",
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
        tooltip = "Beaver bites nearby opponent. Attack power: 4 + Attack"
        },
        new PlayerData.item {
        name = "Crunch",
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
        tooltip = "Beaver bites nearby opponent. Attack power: 6 + Attack"
        },
        new PlayerData.item {
        name = "Crunch",
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
        tooltip = "Beaver bites nearby opponent. Attack power: 9 + Attack"
        },
        new PlayerData.item {
        name = "Accelerate",
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
        name = "Accelerate",
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
        name = "Accelerate",
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
        name = "Aegis",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 1.png",
        type = "skill",
        tooltip = "Boosts defense by 20."
        },
        new PlayerData.item {
        name = "Aegis",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 2.png",
        type = "skill",
        tooltip = "Boosts defense by 40."
        },
        new PlayerData.item {
        name = "Aegis",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/body/aegis/Body Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/aegis/Body Mod 3.png",
        type = "skill",
        tooltip = "Boosts defense by 60."
        }
    };

    public static List<PlayerData.item> itemTemplates = new List<PlayerData.item> {
        //0
        new PlayerData.item {
        name = "Iron Broadsword",
        price = 400,
        texture = (Texture) GD.Load(assetRoute + "Iron Broadsword.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"2"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "A basic broadsword for the common traveler."
        },
        //1
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
        //2
        new PlayerData.item {
        name = "Medium Health Potion",
        price = 600,
        texture = (Texture) GD.Load(assetRoute + "Medium Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 25 Health"
        },
        //3
        new PlayerData.item {
        name = "Large Health Potion",
        price = 900,
        texture = (Texture) GD.Load(assetRoute + "Large Health Potion.png"),
        scale = new Vector2(3,3),
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item",
        tooltip = "Restores 55 Health"
        },
        //4
        new PlayerData.item {
        name = "Sword of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Sword of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"4", "4"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Madness flows from this sword, mixing each aspect of offense to boost both by a large amount."
        },
        //5
        new PlayerData.item {
        name = "Sword of Malice",
        texture = (Texture) GD.Load(assetRoute + "Sword of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"5"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Ill intentions leak from this blade forged from the evil vines of the forest."
        },
        //6
        new PlayerData.item {
        name = "Staff of Malice",
        texture = (Texture) GD.Load(assetRoute + "Staff of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"5"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "Ill intentions leak from this staff forged from the evil vines of the forest."
        },
        //7
        new PlayerData.item {
        name = "Beaver Tribe Horn",
        texture = (Texture) GD.Load(assetRoute + "Beaver Tribe Horn.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"2","2"},
        whichStat = new List<string> {"Attack", "Defense"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "The ancient talisman of the Beaver Tribe. Holds ferocious energy."
        },
        //8
        new PlayerData.item {
        name = "Amulet of Malice",
        texture = (Texture) GD.Load(assetRoute + "Amulet of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"5"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Necklace",
        type = "item",
        tooltip = "A necklace forged from the willpower of ill intentions."
        },
        //9
        new PlayerData.item {
        name = "Amulet of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Amulet of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"4", "4"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+","+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Necklace",
        type = "item",
        tooltip = "This necklace overflows with offensive power."
        },
        //10
        new PlayerData.item {
        name = "Talisman of Mixed Madness",
        texture = (Texture) GD.Load(assetRoute + "Talisman of Mixed Madness.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"4", "4"},
        whichStat = new List<string> {"Attack", "SpAttack"},
        operatorOnStat = new List<string> {"+","+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "This heirloom overflows with offensive power."
        },
        //11
        new PlayerData.item {
        name = "Ring of Malice",
        texture = (Texture) GD.Load(assetRoute + "Ring of Malice.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"5"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "Made from the malice of snakes, this ring contains evil power beyond comprehention."
        },
        //look at this
        //12
        new PlayerData.item {
        name = "Sasuke's Blade",
        texture = (Texture) GD.Load(assetRoute + "Sasuke's Blade.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"3", "3"},
        whichStat = new List<string> {"Attack", "Defense"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "The sword containing the souls of the fallen Uchiha Clan. Multiverse?"
        },
        //13
        new PlayerData.item {
        name = "Zoro's Katana",
        texture = (Texture) GD.Load(assetRoute + "Zoro's Katana.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"10"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "One of the blades of a legendary swordsman. It's said to hold the power of its previous owner."
        },
        //14
        new PlayerData.item {
        name = "Pacifist Wand",
        texture = (Texture) GD.Load(assetRoute + "Pacifist Wand.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"8", "15"},
        whichStat = new List<string> {"SpAttack", "Defense"},
        operatorOnStat = new List<string> {"+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item",
        tooltip = "A wand belonging to the elder beaver cleric. Peace is the main option with this wand of grace."
        },
        //15
        new PlayerData.item {
        name = "Water Spirit Ring",
        texture = (Texture) GD.Load(assetRoute + "Water Spirit Ring.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"10"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "A ring bestowed to the water tribes by the gods themselves. Holds amazing bubble power."
        },
        //16
        new PlayerData.item {
        name = "Beaver Claw Amulet",
        texture = (Texture) GD.Load(assetRoute + "Beaver Claw Amulet.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"10"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Necklace",
        type = "item",
        tooltip = "A beaver claw trinket left behind from a killed member of the tribe. The memories within the claw give you the will to fight."
        },
        //17
        new PlayerData.item {
        name = "Snake Ring",
        texture = (Texture) GD.Load(assetRoute + "Snake Ring.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"3", "1", "4"},
        whichStat = new List<string> {"Attack", "Defense", "SpAttack"},
        operatorOnStat = new List<string> {"+", "+", "+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Talisman",
        type = "item",
        tooltip = "A ring forged of pure snake skin. Kinda gross."
        }



    };

    public static List<EnemyTemplate> enemyTemplates = new List<EnemyTemplate>
    {
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 1,
            health = 4,
            attack = 3,
            defense = 5,
            spAttack = 3,
            spDefense = 5
        },
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 2,
            health = 18,
            attack = 7,
            defense = 7,
            spAttack = 7,
            spDefense = 7
        },
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 3,
            health = 26,
            attack = 12,
            defense = 9,
            spAttack = 12,
            spDefense = 9
        },
        new EnemyTemplate
        {
            name = "GrayWolf",
            level = 4,
            health = 34,
            attack = 17,
            defense = 12,
            spAttack = 12,
            spDefense = 12
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 1,
            health = 6,
            attack = 8,
            defense = 1,
            spAttack = 7,
            spDefense = 1
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 2,
            health = 9,
            attack = 13,
            defense = 2,
            spAttack = 9,
            spDefense = 2
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 3,
            health = 12,
            attack = 19,
            defense = 3,
            spAttack = 19,
            spDefense = 3
        },
        new EnemyTemplate
        {
            name = "Snake",
            level = 4,
            health = 15,
            attack = 26,
            defense = 4,
            spAttack = 26,
            spDefense = 4
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 1,
            health = 20,
            attack = 10,
            defense = 4,
            spAttack = 10,
            spDefense = 4
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 2,
            health = 25,
            attack = 15,
            defense = 8,
            spAttack = 15,
            spDefense = 8
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 3,
            health = 30,
            attack = 20,
            defense = 12,
            spAttack = 20,
            spDefense = 12
        },
        new EnemyTemplate
        {
            name = "BrownBear",
            level = 4,
            health = 35,
            attack = 25,
            defense = 16,
            spAttack = 20,
            spDefense = 16
        },
        new EnemyTemplate
        {
            name = "Spider",
            level = 1,
            health = 6,
            attack = 10,
            defense = 1,
            spAttack = 7,
            spDefense = 1
        },
        new EnemyTemplate
        {
            name = "Spider",
            level = 2,
            health = 9,
            attack = 13,
            defense = 2,
            spAttack = 9,
            spDefense = 2
        },
        new EnemyTemplate
        {
            name = "Spider",
            level = 3,
            health = 12,
            attack = 19,
            defense = 3,
            spAttack = 19,
            spDefense = 3
        },
        new EnemyTemplate
        {
            name = "Spider",
            level = 4,
            health = 15,
            attack = 26,
            defense = 4,
            spAttack = 26,
            spDefense = 4
        },
        new EnemyTemplate
        {
            name = "Crow",
            level = 1,
            health = 6,
            attack = 10,
            defense = 1,
            spAttack = 7,
            spDefense = 1
        },
        new EnemyTemplate
        {
            name = "Crow",
            level = 2,
            health = 9,
            attack = 13,
            defense = 2,
            spAttack = 9,
            spDefense = 2
        },
        new EnemyTemplate
        {
            name = "Crow",
            level = 3,
            health = 12,
            attack = 19,
            defense = 3,
            spAttack = 19,
            spDefense = 3
        },
        new EnemyTemplate
        {
            name = "Crow",
            level = 4,
            health = 15,
            attack = 26,
            defense = 4,
            spAttack = 26,
            spDefense = 4
        },
    };
}
