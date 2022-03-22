using Godot;
using System;
using System.Collections.Generic;

public class InventorySlot : TextureRect
{
    private PlayerData playerData;

    [Export]
    public int slot;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
    }
    public override object GetDragData(Vector2 position)
    {
       var data = playerData.inv[slot];
       var returnedData = new PlayerData.test(data, "Inventory");
       if (data.name == null)
       {
            Console.WriteLine("hi");
       }
        Console.WriteLine(data.name);
        var pear = GetParent();
        Console.WriteLine(pear.Name);

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
        if(comingFrom == "Inventory")
        {
            //Swapping textures
            var nodeToSwapWith = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeInventory/InventoryElements/GridContainer/InventorySlot" + actualData.inventorySlot + "/Icon");
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

            int counter = 0;
            foreach(var item in playerData.inv)
            {
                Console.WriteLine(counter);
                Console.WriteLine(item.name);
                Console.WriteLine(item.inventorySlot);
                counter++;

            }
        }



        //if coming from equipment, clear the equipment from the slot
        if(comingFrom == "Equiptment")
        {
            playerData.equipment.Remove(actualData.equippedSlot);
            //make node have null texture
            var nodeToEmpty = GetNode("/root/Inventory/Background/MarginContainer/WholeContainer/WholeEquip/EquipElements/EquipBars/" + actualData.equippedSlot + "/Icon");
            nodeToEmpty.Set("texture", (Texture)GD.Load("res://assets/helmet background.png"));
            actualData.equippedSlot = null;
            playerData.inv[actualData.inventorySlot] = actualData;

            Console.WriteLine(playerData.inv[actualData.inventorySlot].equippedSlot == null);
        }
    }

    public override bool CanDropData(Vector2 position, object data)
    {
        //look into locking out certain features
        return true;
    }
}
