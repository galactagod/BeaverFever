using Godot;
using System;

public class Crow : EnemyMovementAct
{
    private int _timer = 0;

    public override void _Ready()
    {
        base._Ready();

        // modify member vars
        _enemyType = "Crow";
        _health = 6;
        _maxHealth = 6;
        _speed.x = 75;
        _origSpeed.x = 75;
        _chaseSpeed.x = 200;
        _exp = 100;
        _atkFrm = new int[] { 2, 4};
        _attackRadius = new Vector2(20, 50);
        _detectionRadius = new Vector2(200, 50);
        //_skillA = "Glide";

        EnemyTemplate temp = Global.enemyTemplates.FindAll(x => x.name == _enemyType)[level];
        _health = temp.health;
        _curattack = temp.attack;
        _curdefense = temp.defense;
        _curspAttack = temp.spAttack;
        _curspDefense = temp.spDefense;


        if (level == 0)
        {
            Modulate = Color.Color8(255, 255, 255);
            _exp = 100;
        }
        else if (level == 1)
        {
            Modulate = Color.Color8(238, 86, 86);
            _exp = 200;
        }
        else if (level == 2)
        {
            Modulate = Color.Color8(223, 175, 73);
            _exp = 300;
        }
        else if (level == 3)
        {
            Modulate = Color.Color8(63, 225, 85);
            _exp = 400;
        }


        // start state
        //stateMachine = new EnemyStateMachineManager(this, enemyIdle);
        SprAnimation("Wander");
        _direction = new Vector2(-1,0);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        //stateMachine.Update();
        _timer++;
        
        //BaseMovementControl();
        WanderLogic(_direction);
        

        //GD.Print("Xposition = " + Position.x);
    }

    public override void WanderLogic(Vector2 direction)
    {

        // Wolf will go back and forth relative to start position
        if (_timer % _crowPlatformTime == 0)
        {
            direction.x = (direction.x == 1) ? -1 : 1;
           
        }

        Position = (direction.x == 1) ? new Vector2(Position.x + 1, Position.y) : new Vector2(Position.x - 1, Position.y);

        _direction = new Vector2(direction.x, 0);

        switch (_direction.x)
        {
            case -1:
                _ndSprEnemy.FlipH = false;
                //_ndCollBaseEnemy.Position = new Vector2(-_collBasePositionX, _ndCollBaseEnemy.Position.y);
                break;

            case 1:
                _ndSprEnemy.FlipH = true;
                //_ndCollBaseEnemy.Position = new Vector2(_collBasePositionX, _ndCollBaseEnemy.Position.y);
                break;
        }
    }
}
