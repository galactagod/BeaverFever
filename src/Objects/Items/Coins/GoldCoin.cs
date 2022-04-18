using Godot;
using System;

public class GoldCoin : FloatItems
{
    public override void _Ready()
    {
        base._Ready();
        // set up member vars from parent
        _moneyValue = 50;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }
}
