using Godot;
using System;
using System.Collections.Generic;

public class Store : Control
{
    private PlayerData playerData;
    private PlayerStats playerStats;

    [Signal]
    public delegate void notEnoughCurrency(int slot);

    
    public override void _Ready()
    {


        //ConnectingToSignals
        var slot1Button = GetNode("TabContainer/Items/RichTextLabel/control/Panel1/TextureButton");
        slot1Button.Connect("buyButtonClicked", this, "ItemBought");

        var slot2Button = GetNode("TabContainer/Items/RichTextLabel/control/Panel2/TextureButton");
        slot2Button.Connect("buyButtonClicked", this, "ItemBought");

        var slot3Button = GetNode("TabContainer/Items/RichTextLabel/control/Panel3/TextureButton");
        slot3Button.Connect("buyButtonClicked", this, "ItemBought");

        playerData = GetNode<PlayerData>("/root/PlayerData");
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");

        InitalizingItems();

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void InitalizingItems()
    {
        //currency label
        var currencyLabel = GetNode("Money");
        currencyLabel.Set("text", "Currency: " + playerStats.Muny);
        //get nodes for first 3 items
        //change label, texture, and button status according (if have enough currency)
        if (playerData.itemsAvaliable.Count > 0)
        {
            var slot1Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel1/Label");
            slot1Label.Set("text", playerData.itemsAvaliable[0].name + ": " + playerData.itemsAvaliable[0].price + "--" + playerData.itemsAvaliable[0].tooltip);
            var slot1ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel1/Holder");
            //slot1ButtonTexture.Set("texture", "res://assets/" + itemsAvaliable[0].name + ".png");
            slot1ButtonTexture.Set("texture", playerData.itemsAvaliable[0].texture);
            slot1ButtonTexture.Set("scale", playerData.itemsAvaliable[0].scale);
            if (playerData.itemsAvaliable[0].price > playerStats.Muny)
            {
                EmitSignal("notEnoughCurrency", 1);
            }
        }
        else
        {
            var slot2Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel1/Label");
            slot2Label.Set("text", "");
            var slot2ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel1/Holder");
            slot2ButtonTexture.Set("texture", null);
            EmitSignal("notEnoughCurrency", 1);
        }
        
        if (playerData.itemsAvaliable.Count > 1)
        {
            var slot2Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel2/Label");
            slot2Label.Set("text", playerData.itemsAvaliable[1].name + ": " + playerData.itemsAvaliable[1].price + "--" + playerData.itemsAvaliable[1].tooltip);
            var slot2ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel2/Holder");
            slot2ButtonTexture.Set("texture", playerData.itemsAvaliable[1].texture);
            slot2ButtonTexture.Set("scale", playerData.itemsAvaliable[1].scale);
            if (playerData.itemsAvaliable[1].price > playerStats.Muny)
            {
                EmitSignal("notEnoughCurrency", 2);
            }
        }
        else
        {
            var slot2Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel2/Label");
            slot2Label.Set("text", "");
            var slot2ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel2/Holder");
            slot2ButtonTexture.Set("texture", null);
            EmitSignal("notEnoughCurrency", 2);
        }

        if(playerData.itemsAvaliable.Count > 2)
        {
            var slot3Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel3/Label");
            slot3Label.Set("text", playerData.itemsAvaliable[2].name + ": " + playerData.itemsAvaliable[2].price + "--" + playerData.itemsAvaliable[2].tooltip);
            var slot3ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel3/Holder");
            slot3ButtonTexture.Set("texture", playerData.itemsAvaliable[2].texture);
            slot3ButtonTexture.Set("scale", playerData.itemsAvaliable[2].scale);
            if (playerData.itemsAvaliable[2].price > playerStats.Muny)
            {
                EmitSignal("notEnoughCurrency", 3);
            }
        }
        else
        {
            var slot2Label = GetNode("TabContainer/Items/RichTextLabel/control/Panel3/Label");
            slot2Label.Set("text", "");
            var slot2ButtonTexture = GetNode("TabContainer/Items/RichTextLabel/control/Panel3/Holder");
            slot2ButtonTexture.Set("texture", null);
            EmitSignal("notEnoughCurrency", 3);
        }
    }

    public void ItemBought(int slot)
    {
        //pop item out of list
        //recall initializing items
        
        if(playerData.itemsAvaliable[slot-1].type == "item")
        {
            if(playerData.itemsAvaliable[slot - 1].ableToBeEquippedSlot == "Consumable")
            {
                //if amount > allowed, just return and dont let buy
            }
            playerData.itemsAvaliable[slot - 1].inventorySlot = playerData.inv.Count;
            playerData.inv.Add(playerData.itemsAvaliable[slot - 1]);
        }
        else
        {
            playerData.itemsAvaliable[slot - 1].inventorySlot = playerData.skills.Count;
            playerData.skills.Add(playerData.itemsAvaliable[slot - 1]);
        }
        playerStats.Muny -= playerData.itemsAvaliable[slot - 1].price;
        playerStats.ChangeMoney(0);

        //playerData.itemsInStore.RemoveAt(slot - 1);
        //playerData.itemsAvaliable.RemoveAt(slot - 1);
        InitalizingItems();
    }
}