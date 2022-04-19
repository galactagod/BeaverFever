using Godot;
using System;

public class ReturnToMenu : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private LevelControl levelControl;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = (LevelControl)GetNode("/root/LevelControl");
    }

    public override void _Pressed()
    {
        levelControl.changeScene("res://src/Ui/MainMenu/MasterUI.tscn");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
