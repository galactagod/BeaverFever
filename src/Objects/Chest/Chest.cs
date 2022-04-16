using Godot;
using System;

public class Chest: Node
{
    [Export] private int id = "A";

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

        for(int i=0; i < _ndEventManager.chestEventList.Size; i++)
        {
            if(_ndEventManager.chestEventList[i][idpos] == id)
            {
                //opened status will always be in the second position
                opened = _ndEventManager.chestEventList[i][openedpos];
                flag = true;
            }
        }

        if(flag = false)
        {
            _ndEventManager.createChest(id);
        }
    }

    public override void _Process(float delta)
    {
        
    }

    public void openChest(int id)
    {
        _ndEventManager.chestEventList[id][openedpos] = true;
    }
}