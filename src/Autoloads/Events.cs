using Godot;
using System;

public class Chest : Node
{
    //2d array where x value is id, y value is _is_opened
    //_is_opened will be 0 if chest is unopened, 1 if opened
    [Export] private int[,] chests;

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        
    }

    //Checks if a specific chest is opened given an id value
    public bool IsOpened(int id)
    {
        if(chests[id, 1] == 1)
            return true;
        
        else return false;
    }

    //Sets a specific chest _is_opened value to 1
    public void Open(int id)
    {
        chests[id, 1] = 1;
    }

    //Sets a specific chest _is_opened value to 1
    public void Close(int id)
    {
        chests[id, 1] = 0;
    }
}