using Godot;
using System;

public class Crunch : SkillMove
{
    private Texture _sprite = (Texture)GD.Load("res://src/Assets/Moves/SprCrunch.png");
    private Sprite _ndSprite;

    public Crunch(Vector2 UserPos, Vector2 targetPos, int tier) : base (UserPos, targetPos, tier)
    {
        _UserPos = UserPos;
        _targetPos = targetPos;
        _tier = tier;
    }

    public override void _Ready()
    {
        _type = "physical";
        _energy = 10;

        switch (_tier)
        {
            case 1: _power = 45; break;
            case 2: _power = 50; break;
            case 3: _power = 60; break;
        }


        _ndSprite = CreateSprite(_sprite, 5);

    }

    public override void _PhysicsProcess(float delta)
    {
        
    }


}
