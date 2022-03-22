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
        files.Open(filepath, Godot.File.ModeFlags.ReadWrite);
        Console.WriteLine(files.GetError().ToString());
        string text = files.GetAsText();





        Godot.Collections.Dictionary jsonToWrite = new Godot.Collections.Dictionary();
        jsonToWrite.Add("Attack", playerData.PlayerAttack.ToString());
        jsonToWrite.Add("Defense", playerData.PlayerDefense.ToString());
        jsonToWrite.Add("SpAttack", playerData.PlayerSpAttack.ToString());
        jsonToWrite.Add("SpDefense", playerData.PlayerSpDefense.ToString());
        jsonToWrite.Add("Stamina", playerData.PlayerStamina.ToString());
        jsonToWrite.Add("Health", playerData.PlayerHealth.ToString());
        jsonToWrite.Add("StatPoints", playerData.PlayerTotalPoints.ToString());
        jsonToWrite.Add("Wallet", playerData.Wallet.ToString());
        Godot.Collections.Array inventory = new Godot.Collections.Array();
        foreach(var item in playerData.inv)
        {
            Godot.Collections.Dictionary temp = new Godot.Collections.Dictionary();
            temp.Add("name", item.name);
            temp.Add("price", item.price.ToString());
            temp.Add("scaleX", item.scale.x.ToString());
            temp.Add("scaleY", item.scale.y.ToString());
            temp.Add("equippable", item.equippable.ToString());
            temp.Add("equippedSlot", item.equippedSlot);
            temp.Add("inventorySlot", item.inventorySlot.ToString());
            //Adding item effects
            Godot.Collections.Array itemEffects = new Godot.Collections.Array();
            for(int i = 0; i < item.whichStat.Count;i++)
            {
                Godot.Collections.Dictionary anotherTemp = new Godot.Collections.Dictionary();
                anotherTemp.Add("stat", item.whichStat[i].ToString());
                anotherTemp.Add("operator", item.operatorOnStat[i].ToString());
                anotherTemp.Add("amount", item.amountOnStat[i].ToString());
                itemEffects.Add(anotherTemp);
            }
            temp.Add("itemEffects", itemEffects);
            inventory.Add(temp);
        }
        jsonToWrite.Add("inventory", inventory);



        files.StoreString(JSON.Print(jsonToWrite, "\t"));

        files.Close();
    }
}
