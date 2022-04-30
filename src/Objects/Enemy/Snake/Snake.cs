using Godot;
using System;

public class Snake : EnemyMovementAct
{
    private int _timer = 0;

    public override void _Ready()
    {
        base._Ready();

        // modify member vars
        _enemyType = "Snake";
        _health = 6;
        _maxHealth = 6;
        _speed.x = 75;
        _origSpeed.x = 75;
        _chaseSpeed.x = 200;
        _exp = 100;
        _atkFrm = new int[] {2, 4};
        _attackRadius = new Vector2(50, 50);

        EnemyTemplate temp = Global.enemyTemplates.Find(x => x.name == _enemyType);
        _curattack = temp.attack;
        _curdefense = temp.defense;
        _curspAttack = temp.spAttack;
        _curspDefense = temp.spDefense;

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
        if (_timer % 70 == 0)
        {
            direction.x = (direction.x == 1) ? -1 : 1;
        }

        _direction = new Vector2(direction.x, 0);
    }

}
