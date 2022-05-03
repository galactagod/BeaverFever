using Godot;
using System;

public class BaseMovementAct : KinematicBody2D
{
    // maintain the vector2 upwards
    // csharp limitation, unable to set non primitive types to constants
    protected Vector2 FLOORNORMAL = new Vector2(0, -1);

    [Export] public Vector2 _speed = new Vector2(300.0f, 600.0f);
    [Export] public float _gravity = 2000;
    


    protected Vector2 _velocity = new Vector2(0, 0);
    protected Vector2 _direction = new Vector2(0, 0);

    public override void _PhysicsProcess(float delta)
    {
        _velocity.y += _gravity * delta;
        //GD.Print("new velocity Y = " + _velocity.y);
    }
}
