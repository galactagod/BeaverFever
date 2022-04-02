using Godot;
using System;

public class GrayWolf : EnemyMovementAct
{
    private int _timer = 0;
  
    public override void _Ready()
    {
        base._Ready();

        // modify member vars
        _speed.x = 100.0f;

        // start state
        stateMachine = new EnemyStateMachineManager(this, enemyIdle);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        stateMachine.Update();
        _timer++;

        //GD.Print("Xposition = " + Position.x);
    }

    public override void WanderLogic(Vector2 direction)
    {
        if (direction.x == 0)
        {
            // randomizes btw 0 and 1
            direction.x = (_rnd.Next(0, 2) == 0) ? -1 : 1;
        }

        // Wolf will go back and forth relative to start position
        if (_timer % 50 == 0)
        {
            direction.x = (direction.x == 1) ? -1 : 1;
        }

        _direction = new Vector2(direction.x, 0);
    }

}
