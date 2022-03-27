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

    private string[] EquipSlots1 = new string[4] { "Helmet", "Chest", "Legs", "Boots" };
    private string[] EquipSlots2 = new string[4] { "Necklace", "Weapon", "Talisman1", "Talisman2" };

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
            invSlotNew.GetNode("Icon").Set("hint_tooltip", playerData.getStatLine(item));
            gridContainer.AddChild(invSlotNew);
        }

        
        //need to fill for each node in equips (8 in total)
        foreach(var slot in EquipSlots1)
        {
            var tempNode = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/EquipBars/" + slot + "/Icon");
            if (playerData.equipment.TryGetValue(slot, out var temp))
            {
                tempNode.Set("texture", temp.texture);
            }
            else
            {
                tempNode.Set("texture", (Texture)GD.Load("res://assets/" + slot + "Empty.png"));
            }
        }

        foreach (var slot in EquipSlots2)
        {
            var tempNode = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/EquipBars3/" + slot + "/Icon");
            if (playerData.equipment.TryGetValue(slot, out var temp))
            {
                tempNode.Set("texture", temp.texture);
            }
            else
            {
                tempNode.Set("texture", (Texture)GD.Load("res://assets/" + slot + "Empty.png"));
            }
        }




        //filling node labels
        var attackLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/AttackLabel");
        attackLabelAfterEquips.Set("text", "Attack: " + playerData.attackFinal.ToString());
        var defenseLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/DefenseLabel");
        defenseLabelAfterEquips.Set("text", "Defense: " + playerData.defenseFinal.ToString());
        var spAttackLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpAttackLabel");
        spAttackLabelAfterEquips.Set("text", "SpAttack: " + playerData.spAttackFinal);
        var spDefenseLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpDefenseLabel");
        spDefenseLabelAfterEquips.Set("text", "SpDefense: " + playerData.spDefenseFinal);
        var staminaLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/StaminaLabel");
        staminaLabelAfterEquips.Set("text", "Stamina: " + playerData.staminaFinal);
        var healthLabelAfterEquips = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/HealthLabel");
        healthLabelAfterEquips.Set("text", "Health: " + playerData.healthFinal.ToString());
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
