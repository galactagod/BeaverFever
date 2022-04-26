using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public struct chestData
{
    public int Id;
    public bool Opened;
}

public class EventManager : Node
{
    public List<chestData> chestEventList = new List<chestData>();

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

        chestData temp = new chestData();
        temp.Id = id;
        temp.Opened = false;

        chestEventList.Add(temp);
    }
}