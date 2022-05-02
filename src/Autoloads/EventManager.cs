using Godot;
using System;
using System.Collections;
using System.Collections.Generic;


public class EventManager : Node
{
    public List<ChestData> chestEventList = new List<ChestData>();
    private PlayerData playerData;

    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        chestEventList = playerData.allChests;
    }

    public override void _Process(float delta)
    {
        
    }

    //Creates a new chest in chestEventList
    public void createChest(int id, int item)
    {
        //ArrayList temp = new ArrayList();
        //temp.Add(id);
        //temp.Add(false);

        ChestData temp = new ChestData();
        temp.Id = id;
        temp.Opened = false;
        temp.WhichItem = item;

        chestEventList.Add(temp);
    }
}