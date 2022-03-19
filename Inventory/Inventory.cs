using Godot;
using System;
using System.Collections.Generic;

public class Inventory : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    private PlayerData playerData;

    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        InitializeUI();
    }

    public void InitializeUI()
    {
        var templateInvSlot = GD.Load<PackedScene>("res://Inventory/InventorySlot.tscn");

        var gridContainer = GetNode("Background/MarginContainer/WholeContainer/WholeInventory/InventoryElements/GridContainer");
        //Creating some test items and holding them here


        foreach (var item in playerData.inv)
        {
            var invSlotNew = templateInvSlot.Instance();
            invSlotNew.Name = "InventorySlot" + item.inventorySlot.ToString();
            invSlotNew.GetNode("Icon").Set("texture", item.texture);
            invSlotNew.GetNode("Icon").Set("slot", item.inventorySlot);
            gridContainer.AddChild(invSlotNew);
        }

        var helmetNode = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/EquipBars/Helmet/Icon");
        if(playerData.equipment.TryGetValue("Helmet", out var temp))
        {
            helmetNode.Set("texture", temp.texture);
        }
        else
        {
            helmetNode.Set("texture", null);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
