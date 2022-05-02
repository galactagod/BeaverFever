using Godot;
using System;
using System.Collections.Generic;

public class Inventory : Control
{
    //want to make a button that flips a boolean to show inventory/skills and to be able to equip them
    private bool showingInv = true;


    private PlayerData playerData;
    private PlayerStats playerStats;

    private string[] EquipSlots1 = new string[4] { "Necklace", "Weapon", "Talisman", "Consumable" };
    private string[] EquipSlots2 = new string[4] { "Skill1", "Skill2", "Skill3", "Trash" };

    public override void _Ready()
    {
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        playerData = GetNode<PlayerData>("/root/PlayerData");
        playerData.Connect("itemRemoved", this, "InitializeUI");

        var flipButton = GetNode("Background/MarginContainer/WholeContainer/WholeInventory/InventoryHeader/Control2/TextureButton");
        flipButton.Connect("FlipInv", this, "FlipBool");

        playerData.RefreshStatFinals();
        InitializeUI();
    }

    public void InitializeUI()
    {
        var templateInvSlot = GD.Load<PackedScene>("res://src/Ui/Inventory/InventorySlot.tscn");

        var gridContainer = GetNode("Background/MarginContainer/WholeContainer/WholeInventory/InventoryElements/GridContainer");
        //Creating some test items and holding them here
        var gridChildren = gridContainer.GetChildren();
        
        foreach(Node child in gridChildren)
        {
            gridContainer.RemoveChild(child);
        }

        var inventoryLabelNode = GetNode("Background/MarginContainer/WholeContainer/WholeInventory/InventoryHeader/TextureRect/Label");
        if(showingInv)
        {
            int counter = 0;
            foreach (var item in playerData.inv)
            {
                var invSlotNew = templateInvSlot.Instance();
                invSlotNew.Name = "InventorySlot" + item.inventorySlot.ToString();
                invSlotNew.GetNode("Icon").Set("texture", item.texture);
                invSlotNew.GetNode("Icon").Set("slot", item.inventorySlot);
                invSlotNew.GetNode("Icon").Set("hint_tooltip", playerData.getStatLine(item));
                gridContainer.AddChild(invSlotNew);
                counter++;
            }
            for(int i = counter; i < playerData.inventorySize; i++)
            {
                var invSlotNew = templateInvSlot.Instance();
                invSlotNew.GetNode("Icon").Set("slot", -1);
                gridContainer.AddChild(invSlotNew);
            }
            gridContainer.Set("columns", ((Godot.Vector2)gridContainer.Get("rect_size")).x / 90);
            inventoryLabelNode.Set("text", "Inventory");
        }

        else
        {
            foreach (var item in playerData.skills)
            {
                var invSlotNew = templateInvSlot.Instance();
                invSlotNew.Name = "InventorySlot" + item.inventorySlot.ToString();
                invSlotNew.GetNode("Icon").Set("texture", item.texture);
                invSlotNew.GetNode("Icon").Set("slot", item.inventorySlot);
                invSlotNew.GetNode("Icon").Set("hint_tooltip", playerData.getStatLine(item));
                gridContainer.AddChild(invSlotNew);
            }
            gridContainer.Set("columns", ((Godot.Vector2)gridContainer.Get("rect_size")).x / 90);
            inventoryLabelNode.Set("text", "Skills");
        }
        

        
        //need to fill for each node in equips (8 in total)
        foreach(var slot in EquipSlots1)
        {
            var tempNode = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/EquipBars/" + slot + "/Icon");
            if (playerData.equipment.TryGetValue(slot, out var temp))
            {
                tempNode.Set("texture", temp.texture);
                tempNode.Set("hint_tooltip", playerData.getStatLine(temp));
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
                tempNode.Set("hint_tooltip", playerData.getStatLine(temp));
            }
            else
            {
                if(slot == "Trash")
                    tempNode.Set("texture", (Texture)GD.Load("res://assets/" + "Trash" + "Empty.png"));
                else
                    tempNode.Set("texture", (Texture)GD.Load("res://assets/" + "Skill" + "Empty.png"));
            }
        }




        //filling node labels
        var healthBarLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/HealthBar");
        healthBarLabelAfterEquips.Set("text", "HealthBar: " + playerStats.Health + "/" + playerStats.MaxHealth);
        var attackLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/AttackLabel");
        attackLabelAfterEquips.Set("text", "Attack: " + playerData.attackFinal.ToString());
        var defenseLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/DefenseLabel");
        defenseLabelAfterEquips.Set("text", "Defense: " + playerData.defenseFinal.ToString());
        var spAttackLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpAttackLabel");
        spAttackLabelAfterEquips.Set("text", "SpAttack: " + playerData.spAttackFinal);
        var spDefenseLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpDefenseLabel");
        spDefenseLabelAfterEquips.Set("text", "SpDefense: " + playerData.spDefenseFinal);
        var staminaLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/StaminaLabel");
        staminaLabelAfterEquips.Set("text", "Stamina: " + playerData.staminaFinal);
        var healthLabelAfterEquips = GetNode("Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/HealthLabel");
        healthLabelAfterEquips.Set("text", "Health: " + playerData.healthFinal.ToString());
    }

    public void FlipBool()
    {
        showingInv = !showingInv;
        InitializeUI();
    }
}
