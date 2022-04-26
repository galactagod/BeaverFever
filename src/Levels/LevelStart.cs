using Godot;
using System;

public class LevelStart : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    string levelName;

    // Called when the node enters the scene tree for the first time.
    LevelControl levelControl;
    public override void _Ready()
    {
        levelControl = (LevelControl)GetNode("/root/LevelControl");
        levelControl.changeLevel(levelName);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
