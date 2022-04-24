using Godot;
using System;

public class EventManager : Node
{
    Arraylist chestEventList = new Arraylist();

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        
    }

    //Creates a new chest in chestEventList
    public void createChest(int id)
    {
        ArrayList temp = new ArrayList();
        temp.Add(id);
        temp.Add(false);

        chestEventList.Add(temp);
    }
}