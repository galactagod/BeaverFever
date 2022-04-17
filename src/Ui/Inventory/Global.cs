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
        name = "Dual Sword",
        price = 400,
        texture = (Texture) GD.Load(assetRoute + "Dual Sword.png"),
        scale = new Vector2(3,3),
        amountOnStat = new List<string> {"8"},
        whichStat = new List<string> {"Attack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item"
        },
        new PlayerData.item {
        name = "Book",
        price = 600,
        texture = (Texture) GD.Load(assetRoute + "Book.png"),
        scale = new Vector2(1,1),
        amountOnStat = new List<string> {"3"},
        whichStat = new List<string> {"SpAttack"},
        operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Weapon",
        type = "item"
        },
        new PlayerData.item {
        name = "Small Health Potion",
        price = 600,
        texture = (Texture) GD.Load(assetRoute + "Small Health Potion.png"),
        scale = new Vector2(1,1),
        //amountOnStat = new List<string> {"3"},
        //whichStat = new List<string> {"SpAttack"},
        //operatorOnStat = new List<string> {"+"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Consumable",
        type = "item"
        }

    };

    // A list of every skill avaliable, held as an "item" class
    public static List<PlayerData.item> skillsAvaliable = new List<PlayerData.item> {
        new PlayerData.item {
        name = "Attack Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/strength/punch/Attack Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/punch/Attack Mod 2.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Attack Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/strength/punch/Attack Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/strength/punch/Attack Mod 3.png",
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        texture = (Texture) GD.Load("res://assets/skills/passives/leaves/Leafs 1 Mod 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/leaves/Leafs 1 Mod 1.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Leaf Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/passives/leaves/Leafs Mod 1.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/passives/leaves/Leafs Mod 1.png",
        type = "skill"
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
        new PlayerData.item {
        name = "Book Mod",
        level = 1,
        texture = (Texture) GD.Load("res://assets/skills/body/wisdom/Book 1 Mod 2.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/wisdom/Book 1 Mod 2.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Book Mod",
        level = 2,
        texture = (Texture) GD.Load("res://assets/skills/body/wisdom/Book 1 Mod 3.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/wisdom/Book 1 Mod 3.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Book Mod",
        level = 3,
        texture = (Texture) GD.Load("res://assets/skills/body/wisdom/Book 1 Mod 4.png"),
        amountOnStat = new List<string> {},
        whichStat = new List<string> {},
        operatorOnStat = new List<string> {},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = "res://assets/skills/body/wisdom/Book 1 Mod 4.png",
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        type = "skill"
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
        type = "skill"
        },
        new PlayerData.item {
        name = "Body Mod",
        level = 1,
        texture = (Texture) GD.Load(skillAssetRoute + "/body/armor/Body Mod 1.png"),
        amountOnStat = new List<string> {"1.05"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/armor/Body Mod 1.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Body Mod",
        level = 2,
        texture = (Texture) GD.Load(skillAssetRoute + "/body/armor/Body Mod 2.png"),
        amountOnStat = new List<string> {"1.10"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/armor/Body Mod 2.png",
        type = "skill"
        },
        new PlayerData.item {
        name = "Body Mod",
        level = 3,
        texture = (Texture) GD.Load(skillAssetRoute + "/body/armor/Body Mod 3.png"),
        amountOnStat = new List<string> {"1.30"},
        whichStat = new List<string> {"Defense"},
        operatorOnStat = new List<string> {"*"},
        equippable = true,
        equippedSlot = "none",
        ableToBeEquippedSlot = "Skill",
        textureRoute = skillAssetRoute + "/body/armor/Body Mod 3.png",
        type = "skill"
        }
    };
}
