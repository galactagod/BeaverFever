using Godot;
using System;

public class Chest: Node
{
    [Export] private int id = 0;

    //id will always be the first item in the arraylist
    private int idpos = 0;

    //false if unopened
    private bool opened;

    //opened status will always be in the second position
    private int openedpos = 1;

    //Node reference
    private EventManager _ndEventManager;
    
    public override void _Ready()
    {
        _ndEventManager = GetNode<EventManager>("/root/EventManager");

        bool flag = false;

        for(int i=0; i < _ndEventManager.chestEventList.Count; i++)
        {
            if(_ndEventManager.chestEventList[i].Id == id)
            {
                //opened status will always be in the second position
                opened = _ndEventManager.chestEventList[i].Opened;
                flag = true;
            }
        }

        if(!flag)
        {
            _ndEventManager.createChest(id);
        }
    }

    public override void _Process(float delta)
    {
        
    }

    public void openChest(int id)
    {

        var a = _ndEventManager.chestEventList[id];
        a.Opened = true;
        _ndEventManager.chestEventList[id] = a;
    }
}