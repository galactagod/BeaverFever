using Godot;
using System;
using System.Collections.Generic;

public class InventorySlot : TextureRect
{
    private PlayerData playerData;
    private LevelControl levelControl;
    private Node inventoryLabelNode;

    [Export]
    public int slot;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        levelControl = GetNode<LevelControl>("/root/LevelControl");
        inventoryLabelNode = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeInventory/InventoryHeader/TextureRect/Label");
    }
    public override object GetDragData(Vector2 position)
    {
        if(slot == -1)
            return null;
        PlayerData.item data;
        if ((string)inventoryLabelNode.Get("text") == "Inventory")
        {
            data = playerData.inv[slot];
        }
        else
        {
            data = playerData.skills[slot];
        }  
        var returnedData = new PlayerData.test(data, "Inventory");

        // Handling UI aspect of the drag
        var dragTexture = new TextureRect();
        dragTexture.Set("expand", true);
        dragTexture.Set("texture", Texture);
        Vector2 temp;
        temp.x = 90;
        temp.y = 90;
        dragTexture.Set("rect_size", temp);

        var control = new Control();
        control.AddChild(dragTexture);

        Vector2 a = (Vector2)dragTexture.Get("rect_size");
        Vector2 otherTemp;
        otherTemp.x = (float)(a.x * -.5);
        otherTemp.y = (float)(a.y * -.5); 

        dragTexture.Set("rect_position", otherTemp);


        SetDragPreview(control);

        
        return returnedData;
    }
    public override void DropData(Vector2 position, object data)
    {
        var asTest = (PlayerData.test)data;
        var actualData = asTest.makeItem();
        string comingFrom = asTest.comingFrom;
        asTest.Free();




        //if coming from inventory, swap the values
            //get node of where coming from
            //change the texture of that to this texture
            //change the texture of this one to that one
            //swap the values in the inventory
            //done?
        if(comingFrom == "Inventory" && actualData.type == "item")
        {
            if (slot == -1)
                return;
            //Swapping textures
            var nodeToSwapWith = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeInventory/InventoryElements/GridContainer/InventorySlot" + actualData.inventorySlot + "/Icon");
            nodeToSwapWith.Set("texture", Texture);
            nodeToSwapWith.Set("hint_tooltip", Get("hint_tooltip"));

            Texture = actualData.texture;
            Set("hint_tooltip", playerData.getStatLine(actualData));
            //Swapping actual Data

            var current = playerData.inv[slot];
            current.inventorySlot = actualData.inventorySlot;

            var incoming = playerData.inv[actualData.inventorySlot];
            incoming.inventorySlot = slot;

            playerData.inv[slot] = incoming;
            playerData.inv[actualData.inventorySlot] = current;


            //refreshing the inventory values of the swapped values so the equipment pages knows where they are in the inventory
            if(current.equippedSlot != null)
            {
                playerData.equipment.Remove(current.equippedSlot);
                playerData.equipment.Add(current.equippedSlot, current);
            }

            if(incoming.equippedSlot != null)
            {
                playerData.equipment.Remove(incoming.equippedSlot);
                playerData.equipment.Add(incoming.equippedSlot, incoming);
            }
            
        }



        //if coming from equipment, clear the equipment from the slot
        if(comingFrom == "EquipBars")
        {
            playerData.equipment.Remove(actualData.equippedSlot);
            //here we need to undo the stat changes from the item we had equipped
            playerData.EquipChangesStatFilter(actualData, true);

            //make node have null texture
            var nodeToEmpty = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/" + comingFrom + "/" + actualData.equippedSlot + "/Icon");
            nodeToEmpty.Set("texture", (Texture)GD.Load("res://assets/" + actualData.equippedSlot + "Empty" + ".png"));
            actualData.equippedSlot = "none";
            playerData.inv[actualData.inventorySlot] = actualData;

            // Changing label text
            var attackLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/AttackLabel");
            attackLabelAfterEquips.Set("text", "Attack: " + playerData.attackFinal.ToString());
            var defenseLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/DefenseLabel");
            defenseLabelAfterEquips.Set("text", "Defense: " + playerData.defenseFinal.ToString());
            var spAttackLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpAttackLabel");
            spAttackLabelAfterEquips.Set("text", "SpAttack: " + playerData.spAttackFinal);
            var spDefenseLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpDefenseLabel");
            spDefenseLabelAfterEquips.Set("text", "SpDefense: " + playerData.spDefenseFinal);
            var staminaLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/StaminaLabel");
            staminaLabelAfterEquips.Set("text", "Stamina: " + playerData.staminaFinal);
            var healthLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/HealthLabel");
            healthLabelAfterEquips.Set("text", "Health: " + playerData.healthFinal.ToString());
            playerData.ResetInv();
        }

        if(comingFrom == "EquipBars3")
        {
            playerData.equipment.Remove(actualData.equippedSlot);
            //here we need to undo the stat changes from the item we had equipped
            playerData.EquipChangesStatFilter(actualData, true);
            var nodeToEmpty = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/" + comingFrom + "/" + actualData.equippedSlot + "/Icon");
            nodeToEmpty.Set("texture", (Texture)GD.Load("res://assets/" + "Skill" + "Empty" + ".png"));
            actualData.equippedSlot = "none";
            //go to it in skills list and remove it from the equipped area
            playerData.skills[actualData.inventorySlot] = actualData;



            // Changing label text
            var attackLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/AttackLabel");
            attackLabelAfterEquips.Set("text", "Attack: " + playerData.attackFinal.ToString());
            var defenseLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/DefenseLabel");
            defenseLabelAfterEquips.Set("text", "Defense: " + playerData.defenseFinal.ToString());
            var spAttackLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpAttackLabel");
            spAttackLabelAfterEquips.Set("text", "SpAttack: " + playerData.spAttackFinal);
            var spDefenseLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/SpDefenseLabel");
            spDefenseLabelAfterEquips.Set("text", "SpDefense: " + playerData.spDefenseFinal);
            var staminaLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/StaminaLabel");
            staminaLabelAfterEquips.Set("text", "Stamina: " + playerData.staminaFinal);
            var healthLabelAfterEquips = GetNode(levelControl.rootPath + "Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/Character/NinePatchRect/TextureRect/VBoxContainer/VBoxContainer/HealthLabel");
            healthLabelAfterEquips.Set("text", "Health: " + playerData.healthFinal.ToString());
            playerData.ResetInv();
        }
    }

    public override bool CanDropData(Vector2 position, object data)
    {
        //look into locking out certain features
        return true;
    }
}
