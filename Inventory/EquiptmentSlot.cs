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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
    }
    public override object GetDragData(Vector2 position)
    {
        
        String nameOfSlot = GetParent().Name;

        if (!playerData.equipment.TryGetValue(nameOfSlot, out var data))
        {
            return null;
        }


        var returnedData = new PlayerData.test(data, "Equiptment");

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

        //handling if there is something already equipted
        PlayerData.item apple;
        if(playerData.equipment.TryGetValue(nameOfSlot, out apple))
        {
            //if its there, we gotta go to its inventory and unequipt it
            apple.equippedSlot = null;
            playerData.inv[apple.inventorySlot] = apple;
        }

        //Changing inventory of inserted to make it equipted
        var asTest = (PlayerData.test)data;
        var actualData = asTest.makeItem();
        asTest.Free();
        actualData.equippedSlot = nameOfSlot;
        playerData.inv[actualData.inventorySlot] = actualData;

        //Changing texture
        Texture = actualData.texture;
        Set("scale", actualData.scale);
        Set("hint_tooltip", playerData.getStatLine(actualData));

        //Replacing it in the dictionary
        playerData.equipment.Remove(nameOfSlot);
        playerData.equipment.Add(nameOfSlot, actualData);


        //making sure it saved
        var ban = new PlayerData.item();
        playerData.equipment.TryGetValue(nameOfSlot, out ban);
        Console.WriteLine(ban.name);
        Console.WriteLine(ban.inventorySlot);
        Console.WriteLine(ban.equippedSlot);

        Console.WriteLine(playerData.inv[ban.inventorySlot].name);


    }

    

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override bool CanDropData(Vector2 position, object data)
    {
        //var asTest = (Global.test)data;
        //var comingFrom = asTest.comingFrom;
        //asTest.Free();
        //if(comingFrom == "Equiptment")
        //{
        //    return false;
        //}
        return true;
    }
}
