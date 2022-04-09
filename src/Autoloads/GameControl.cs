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
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("key_1"))
        {
            _ndPlayerStats.ChangeHealth(_ndPlayerStats.Health + 1);
        }

        if (Input.IsActionJustPressed("key_2"))
        {
            _ndPlayerStats.ChangeHealth(_ndPlayerStats.Health - 1);
        }

        if (Input.IsActionJustPressed("key_3"))
        {
            _ndPlayerStats.ChangeMaxHealth(_ndPlayerStats.MaxHealth + 1);
        }

        if (Input.IsActionJustPressed("key_4"))
        {
            _ndPlayerStats.ChangeMaxHealth(_ndPlayerStats.MaxHealth - 1);
        }

        if (Input.IsActionJustPressed("key_5"))
        {
            _ndPlayerStats.ChangeExtraHealth(_ndPlayerStats.ExtraHealth + 1);
        }

        if (Input.IsActionJustPressed("key_6"))
        {
            _ndPlayerStats.ChangeExtraHealth(_ndPlayerStats.ExtraHealth - 1);
        }
    }
}
