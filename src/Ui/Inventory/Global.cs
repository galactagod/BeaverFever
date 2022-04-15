using Godot;
using System;
using System.Collections.Generic;


//where I will put all the items avaliable, and then in player data i will house the string names of the items and take them off as the player aquires them
public static class Global
{
    private static string assetRoute = "res://assets/";
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
    public static List<PlayerData.item> skillsAvaliable = new List<PlayerData.item> {
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
}
