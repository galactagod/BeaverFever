using Godot;
using System;

public class GrayWolf : EnemyMovementAct
{
    private int _timer = 0;
  
    public override void _Ready()
    {
        base._Ready();
        
        // modify member vars
        _enemyType = "GrayWolf";
        _health = 10;
        _maxHealth = 10;
        _speed.x = 100;
        _origSpeed.x = 100;
        _chaseSpeed.x = 250;
        _exp = 200;
        _atkFrm = new int[] {3, 7};
        _attackRadius = new Vector2(55, 50);

        EnemyTemplate temp = Global.enemyTemplates.FindAll(x => x.name == _enemyType)[level];
        _health = temp.health;
        _curattack = temp.attack;
        _curdefense = temp.defense;
        _curspAttack = temp.spAttack;
        _curspDefense = temp.spDefense;


        if (level == 0)
        {
            Modulate = Color.Color8(255, 255, 255);
            _exp = 200;
        }
        else if(level == 1)
        {
            Modulate = Color.Color8(238, 86, 86);
            _exp = 400;
        }
        else if (level == 2)
        {
            Modulate = Color.Color8(223, 175, 73);
            _exp = 600;
        }
        else if (level == 3)
        {
            Modulate = Color.Color8(63, 225, 85);
            _exp = 800;
        }


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
