using Godot;
using System;

public class SaveQuitButton : TextureButton
{
    private PlayerData playerData;
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
    }

    public override void _Pressed()
    {
        string filepath = "user://playerStatsFile.json";
        Godot.File files = new Godot.File();
        files.Open(filepath, Godot.File.ModeFlags.WriteRead);
        files.Seek(0);





        Godot.Collections.Dictionary jsonToWrite = new Godot.Collections.Dictionary();
        jsonToWrite.Add("Attack", playerData.PlayerAttack.ToString());
        jsonToWrite.Add("Defense", playerData.PlayerDefense.ToString());
        jsonToWrite.Add("SpAttack", playerData.PlayerSpAttack.ToString());
        jsonToWrite.Add("SpDefense", playerData.PlayerSpDefense.ToString());
        jsonToWrite.Add("Stamina", playerData.PlayerStamina.ToString());
        jsonToWrite.Add("Health", playerData.PlayerHealth.ToString());
        jsonToWrite.Add("StatPoints", playerData.PlayerTotalPoints.ToString());
        jsonToWrite.Add("Muny", playerData.Muny.ToString());
        Godot.Collections.Array inventory = new Godot.Collections.Array();
        Godot.Collections.Array skills = new Godot.Collections.Array();
        foreach (var item in playerData.inv)
        {
            Godot.Collections.Dictionary temp = new Godot.Collections.Dictionary();
            temp.Add("name", item.name);
            temp.Add("price", item.price.ToString());
            temp.Add("scaleX", item.scale.x.ToString());
            temp.Add("scaleY", item.scale.y.ToString());
            temp.Add("equippable", item.equippable.ToString() == null? "none" : item.equippable.ToString());
            temp.Add("equippedSlot", item.equippedSlot);
            temp.Add("inventorySlot", item.inventorySlot.ToString());
            temp.Add("ableToBeEquippedSlot", item.ableToBeEquippedSlot);
            temp.Add("type", item.type);
            //Adding item effects
            Godot.Collections.Array itemEffects = new Godot.Collections.Array();
            for(int i = 0; i < item.whichStat.Count;i++)
            {
                Godot.Collections.Dictionary anotherTemp = new Godot.Collections.Dictionary();
                anotherTemp.Add("stat", item.whichStat[i]);
                anotherTemp.Add("operator", item.operatorOnStat[i]);
                anotherTemp.Add("amount", item.amountOnStat[i]);
                itemEffects.Add(anotherTemp);
            }
            temp.Add("itemEffects", itemEffects);
            
            inventory.Add(temp);
        }
        jsonToWrite.Add("inventory", inventory);

        foreach (var item in playerData.skills)
        {
            Godot.Collections.Dictionary temp = new Godot.Collections.Dictionary();
            temp.Add("name", item.name);
            temp.Add("price", item.price.ToString());
            temp.Add("scaleX", item.scale.x.ToString());
            temp.Add("scaleY", item.scale.y.ToString());
            temp.Add("equippable", item.equippable.ToString());
            temp.Add("equippedSlot", item.equippedSlot);
            temp.Add("inventorySlot", item.inventorySlot.ToString());
            temp.Add("ableToBeEquippedSlot", item.ableToBeEquippedSlot);
            temp.Add("textureRoute", item.textureRoute);
            temp.Add("level", item.level.ToString());
            temp.Add("type", item.type);
            //Adding item effects
            Godot.Collections.Array itemEffects = new Godot.Collections.Array();
            for (int i = 0; i < item.whichStat.Count; i++)
            {
                Godot.Collections.Dictionary anotherTemp = new Godot.Collections.Dictionary();
                anotherTemp.Add("stat", item.whichStat[i]);
                anotherTemp.Add("operator", item.operatorOnStat[i]);
                anotherTemp.Add("amount", item.amountOnStat[i]);
                itemEffects.Add(anotherTemp);
            }
            temp.Add("itemEffects", itemEffects);
            

            skills.Add(temp);
        }
        jsonToWrite.Add("skills", skills);

        Godot.Collections.Array itemsAvaliable = new Godot.Collections.Array();
        foreach (var name in playerData.itemsInStore)
        {
            itemsAvaliable.Add(name);
        }
        jsonToWrite.Add("itemsAvaliable", itemsAvaliable);



        files.StoreString(JSON.Print(jsonToWrite, "\t"));

        files.Close();
    }
}
