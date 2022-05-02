using Godot;
using System;

public class StartGame : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PlayerData playerData;
    LevelControl levelControl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        levelControl = GetNode<LevelControl>("/root/LevelControl");
    }

    public override void _Pressed()
    {
        if(playerData.currentLevel == "none")
        {
            levelControl.LevelChange(GD.Load<PackedScene>("res://src/Levels/Intro.tscn"));
        }
        else
        {
            levelControl.changeBasedOnName(playerData.currentLevel);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
