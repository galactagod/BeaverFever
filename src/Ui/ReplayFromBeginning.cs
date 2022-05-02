using Godot;
using System;

public class ReplayFromBeginning : TextureButton
{
    private LevelControl levelControl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = GetNode<LevelControl>("/root/LevelControl");
    }

    public override void _Pressed()
    {
        levelControl.replayFromBeginning();
    }
}
