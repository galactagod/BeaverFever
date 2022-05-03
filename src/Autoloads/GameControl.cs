using Godot;
using System;

// Autoload, primary usage is for cheats and bug testing
public class GameControl : Node
{

    private PlayerStats _ndPlayerStats;
    

    public override void _Ready()
    {
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");

        // OS.CenterWindow();
        OS.WindowMaximized = true;
        GD.Print("Size = " + GetViewport().Size.x + " X " + GetViewport().Size.y);
        // original was 1024X600
    }

    /*
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("key_1"))
        {
            _ndPlayerStats.ChangeHealth(1);
            _ndPlayerStats.ChangeMoney(1000);
            _ndPlayerStats.ChangeExp(1000);
        }

        if (Input.IsActionJustPressed("key_2"))
        {
            _ndPlayerStats.ChangeHealth(-1);
        }

        if (Input.IsActionJustPressed("key_3"))
        {
            _ndPlayerStats.ChangeMaxHealth(1);
        }

        if (Input.IsActionJustPressed("key_4"))
        {
            _ndPlayerStats.ChangeMaxHealth(-1);
        }

        if (Input.IsActionJustPressed("key_5"))
        {
            _ndPlayerStats.ChangeMaxEnergy(1);
        }

        if (Input.IsActionJustPressed("key_6"))
        {
            _ndPlayerStats.ChangeMaxEnergy(-1);
        }
    }
    */
}
