using Godot;
using System;

public class ReplayLevelButton : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private LevelControl levelControl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = GetNode<LevelControl>("/root/LevelControl");
    }

    public override void _Pressed()
    {
        levelControl.comingFromDeath();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
