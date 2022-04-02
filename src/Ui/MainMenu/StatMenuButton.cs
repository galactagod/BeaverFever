using Godot;
using System;

public class StatMenuButton : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    String toOpen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        GetTree().ChangeScene(toOpen);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}