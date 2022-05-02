using Godot;
using System;

public class Chest: Node
{
    [Export] private int id;
    [Export] private int whichItem;

    //id will always be the first item in the arraylist
    private int idpos = 0;

    //false if unopened
    private bool opened;

    //opened status will always be in the second position
    private int openedpos = 1;

    private Sprite mySprite;

    //Node reference
    private EventManager _ndEventManager;

    private PlayerData _playerData;

    private DialoguePopUp _dialoguePop;
    public override void _Ready()
    {
        _ndEventManager = GetNode<EventManager>("/root/EventManager");
        _playerData = GetNode<PlayerData>("/root/PlayerData");

        mySprite = GetNode<Sprite>("Sprite");

        //flag to check if the chest has already been created
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

        if(opened)
        {
            Rect2 rect2 = new Rect2(135,0,35,40);
            mySprite.RegionRect = rect2;
        }

        if(!flag)
        {
            _ndEventManager.createChest(id, whichItem);
        }

        this.Connect("body_entered", this, nameof(openChest));
        this.Connect("body_exited", this, nameof(unPopDialogue));
    }

    public override void _Process(float delta)
    {
        
    }

    public void openChest(Node body)
    {
        if(body is ObjPlayer)
        {
            AddChild(GD.Load<PackedScene>("res://src/Dialogue/DialoguePopUp.tscn").Instance());
            _dialoguePop = GetNode<DialoguePopUp>("DialoguePopUp");
            if (!opened)
            {
                ChestData temp = new ChestData();
                int i = 0;
                for (i = 0; i < _ndEventManager.chestEventList.Count; i++)
                {
                    if (_ndEventManager.chestEventList[i].Id == id)
                    {
                        temp = _ndEventManager.chestEventList[i];
                        break;
                    }
                }
                temp.Opened = true;
                _ndEventManager.chestEventList[i] = temp;
                PlayerData.item item = Global.itemTemplates[whichItem];
                item.inventorySlot = _playerData.inv.Count;
                _playerData.inv.Add(item);
                _dialoguePop.PopUp("You received " + item.name + "!");
                Rect2 rect2 = new Rect2(135, 0, 35, 40);
                mySprite.RegionRect = rect2;
                opened = true;
            }
        }
        
    }

    public void unPopDialogue(Node body)
    {
        if(body is ObjPlayer)
        {
            _dialoguePop.UnPop();
            GetNode("DialoguePopUp").QueueFree();
        }
        
    }
}