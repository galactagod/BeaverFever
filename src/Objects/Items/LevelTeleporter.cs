using Godot;
using System;

public class LevelTeleporter : Area2D
{
    [Export]
    public string levelToGo;

    LevelControl levelControl;

    PackedScene teleportTo;

    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(OnBodyEntered));
        levelControl = GetNode<LevelControl>("/root/LevelControl");
        teleportTo = GD.Load<PackedScene>(levelToGo);
    }

    public void OnBodyEntered(Node body)
    {
        if (body is ObjPlayer)
        {

            levelControl.LevelChange(teleportTo);
        }


    }
}
