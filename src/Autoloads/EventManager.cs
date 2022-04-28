using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : Node
{
    public List<ChestData> chestEventList = new List<ChestData>();

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        
    }

    //Creates a new chest in chestEventList
    public void createChest(int id)
    {
        //ArrayList temp = new ArrayList();
        //temp.Add(id);
        //temp.Add(false);

        ChestData temp = new ChestData();
        temp.Id = id;
        temp.Opened = false;

        chestEventList.Add(temp);
    }
}