using Godot;
using System;

public class CopperCoin : FloatItems
{
    public override void _Ready()
    {
        base._Ready();
        // set up member vars from parent
        _moneyValue = 5;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }
}
