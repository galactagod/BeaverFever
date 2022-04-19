using Godot;
using System;
using System.Collections.Generic;

public class EquiptmentSlot : TextureRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int a = 2;
    private PlayerData playerData;
    private LevelControl levelControl;
    private PlayerStats playerStats;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        levelControl = GetNode<LevelControl>("/root/LevelControl");
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }
    public override object GetDragData(Vector2 position)
    {
        
        String nameOfSlot = GetParent().Name;

        if (nameOfSlot == "Consumable" || nameOfSlot == "Trash")
            return null;

        if (!playerData.equipment.TryGetValue(nameOfSlot, out var data))
        {
            return null;
        }

        string comingFrom = "EquipBars3";
        if(nameOfSlot == "Necklace" || nameOfSlot == "Weapon" || nameOfSlot == "Talisman" || nameOfSlot == "Consumable")
        {
            comingFrom = "EquipBars";
        }
          
        var returnedData = new PlayerData.test(data, comingFrom);

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

        //Getting the slot we are dropping it into
        String nameOfSlot = GetParent().Name;
        //Getting the data and converting it as needed
        var asTest = (PlayerData.test)data;
        string comingFrom = asTest.comingFrom;
        var actualData = asTest.makeItem();
        asTest.Free();


        String compareSlot = nameOfSlot;
        if (compareSlot == "Skill1" || compareSlot == "Skill2" || compareSlot == "Skill3")
        {
            compareSlot = "Skill";
        }

        if (comingFrom == "EquipBars3" || comingFrom == "EquipBars")
        {
            return;
        }

        if (actualData.equippedSlot != "none")
        {
            return;
        }

        if (nameOfSlot == "Trash")
        {
            playerData.RemoveFromInv(actualData.inventorySlot);
            return;
        }

        if (compareSlot != actualData.ableToBeEquippedSlot)
        {
            return;
        }

        

        //if a consumable, do the consumable things
        if (actualData.ableToBeEquippedSlot == "Consumable")
        {
            Texture = actualData.texture;
            //counter could go here?
            //use consumable?
            Texture = (Texture)GD.Load("res://assets/" + "Consumable" + "Empty" + ".png");
            playerStats.UseConsumable(actualData.name);
            playerData.RemoveFromInv(actualData.inventorySlot);
            return;
        }

        
        //else, do the rest

        //handling if there is something already equipted

        PlayerData.item apple;
        if(playerData.equipment.TryGetValue(nameOfSlot, out apple))
        {
            //if its there, we gotta go to its inventory and unequipt it
            apple.equippedSlot = "none";
            if (compareSlot != "Skill")
                playerData.inv[apple.inventorySlot] = apple;
            else
                playerData.skills[apple.inventorySlot] = apple;

            //here we need to undo the equip stat changes of the item we have equipped
            playerData.EquipChangesStatFilter(apple, true);
        }

        

        //Changing inventory of inserted to make it equipted
        actualData.equippedSlot = nameOfSlot;
        if (compareSlot != "Skill")
            playerData.inv[actualData.inventorySlot] = actualData;
        else
            playerData.skills[actualData.inventorySlot] = actualData;

        //Changing texture
        Texture = actualData.texture;
        Set("scale", actualData.scale);
        Set("hint_tooltip", playerData.getStatLine(actualData));
        


        //Replacing it in the dictionary
        playerData.equipment.Remove(nameOfSlot);
        playerData.equipment.Add(nameOfSlot, actualData);

        



        //here we need to do the equip stat changes of the new item we have equipped
        playerData.EquipChangesStatFilter(actualData, false);
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

    

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override bool CanDropData(Vector2 position, object data)
    {
        return true;
    }
}
