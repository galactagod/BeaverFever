using Godot;
using System;

public class MasterAddButton : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    string Type;

    [Signal]
    public delegate void statPointsAdd(string type);
    private LevelControl levelControl;

    // Used to help with dynamic routing
    private string routeUntilScene = "/root/";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = GetNode<LevelControl>("/root/LevelControl");
        var mainSheet = GetNode(levelControl.rootPath + "CharacterSheet");
        mainSheet.Connect("statPointsEmptied", this, "disableThis");
        mainSheet.Connect("statPointsFilled", this, "enableThis");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _Pressed()
    {
        EmitSignal("statPointsAdd", Type);
    }
    public void disableThis()
    {
        Disabled = true;
    }
    public void enableThis()
    {
        Disabled = false;
    }

        
}
