using Godot;
using System;

public class BuyButton : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    int slot;

    [Signal]
    public delegate void buyButtonClicked(int slot);

    private LevelControl levelControl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = (LevelControl)GetNode("/root/LevelControl");
        var mainStoreNode = GetNode(levelControl.rootPath + "Control");
        mainStoreNode.Connect("notEnoughCurrency", this, "DisableButton");


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _Pressed()
    {
        EmitSignal("buyButtonClicked", slot);
    }
    public void DisableButton(int slot)
    {
        if(this.slot == slot)
        {
            Disabled = true;
        }
    }
}
