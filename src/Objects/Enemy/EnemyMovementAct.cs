using Godot;
using System;


public class EnemyMovementAct : KinematicBody2D
{
    // maintain the vector2 upwards
    // csharp limitation, unable to set non primitive types to constants
    protected Vector2 FLOORNORMAL = new Vector2(0, -1);

    [Export]
    public int level;
    [Export] protected int _crowPlatformTime = 300;

    public float _gravity = 2000;

    protected float _curattack = 2;
    protected float _curdefense = 2;
    protected float _curspAttack = 2;
    protected float _curspDefense = 2;

    protected float _health;
    protected float _maxHealth;
    protected float _exp;
    protected Vector2 _speed = new Vector2(0, 400);
    protected Vector2 _origSpeed = new Vector2(0, 400);
    protected Vector2 _chaseSpeed = new Vector2(0, 400);
    protected Vector2 _velocity = new Vector2(0, 0);
    protected Vector2 _direction = new Vector2(0, 0);
    protected Vector2 _detectionRadius = new Vector2(200, 50);
    protected Vector2 _attackRadius = new Vector2(0, 0);
    protected Vector2 _rangeRadius = new Vector2(0, 0);
    protected Vector2 _startPos;
    protected int[] _atkFrm;
    protected bool _isAnimationOver = false;
    protected bool _isStomped = false;
    protected bool _isDamaged = false;
    protected bool _isDead = false;
    protected float _collBasePositionX;
    protected string _enemyType;
    protected string _curSkill;
    protected string _skillA;
    protected bool _useSkill;
    protected readonly Random _rnd = new Random();

    // node reference
    private PlayerStats _ndPlayerStats;
    protected ObjPlayer _ndObjPlayer;
    protected AnimationPlayer _ndAnimEnemy;
    protected AnimatedSprite _ndSprEnemy;
    protected CollisionShape2D _ndCollBaseEnemy;
    protected Area2D _ndStompArea;

    // state setups
    protected EnemyStateMachineManager stateMachine;
    public EnemyAttackA enemyAttackA = new EnemyAttackA();
    public EnemyAttackB enemyAttackB = new EnemyAttackB();
    public EnemyChase enemyChase = new EnemyChase();
    public EnemyDeath enemyDeath = new EnemyDeath();
    public EnemyHurt enemyHurt = new EnemyHurt();
    public EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyJump enemyJump = new EnemyJump();
    public EnemyReturn enemyReturn = new EnemyReturn();
    public EnemyWander enemyWander = new EnemyWander();

    // getters and setters
    public float CurDmg { get { return _curattack; } set { _curattack = value; } }
    public float CurDef { get { return _curdefense; } set { _curdefense = value; } }
    public float CurSpAttack { get { return _curspAttack; } set { _curspAttack = value; } }
    public float CurSpDefense { get { return _curspDefense; } set { _curspDefense = value; } }
    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float Exp { get { return _exp; } set { _exp = value; } }
    public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
    public Vector2 Speed { get { return _speed; } set { _speed = value; } }
    public Vector2 OrigSpeed { get { return _origSpeed; } set { _origSpeed = value; } }
    public Vector2 ChaseSpeed { get { return _chaseSpeed; } set { _chaseSpeed = value; } }
    public int[] AtkFrm { get { return _atkFrm; } set { _atkFrm = value; } }
    public bool IsAnimationOver { get { return _isAnimationOver; } set { _isAnimationOver = value; } }
    public bool IsStomped { get { return _isStomped; } set { _isStomped = value; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public string EnemyType { get { return _enemyType; } set { _enemyType = value; } }
    public string CurSkill { get { return _curSkill; } set { _curSkill = value; } }
    public bool UseSkill { get { return _useSkill; } set { _useSkill = value; } }
    public PlayerStats NdPlayerStats { get { return _ndPlayerStats; } set { _ndPlayerStats = value; } }
    public ObjPlayer NdObjPlayer { get { return _ndObjPlayer; } set { _ndObjPlayer = value; } }
    public AnimationPlayer NdAnimEnemy { get { return _ndAnimEnemy; } set { _ndAnimEnemy = value; } }
    public AnimatedSprite NdSprEnemy { get { return _ndSprEnemy; } set { _ndSprEnemy = value; } }

    public override void _Ready()
    {
        // acquire node references
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndObjPlayer = GetNode<ObjPlayer>("../ObjPlayer");
        _ndAnimEnemy = GetNode<AnimationPlayer>("AnimEnemy");
        _ndSprEnemy = GetNode<AnimatedSprite>("SprEnemy");
        _ndCollBaseEnemy = GetNode<CollisionShape2D>("CollBaseEnemy");
        _ndStompArea = GetNode<Area2D>("StompArea");

        _ndAnimEnemy.Connect("animation_finished", this, nameof(OnAnimationFinished));
        _ndStompArea.Connect("area_entered", this, nameof(OnAreaShapeStompEntered));
        _collBasePositionX = _ndCollBaseEnemy.Position.x;
        _startPos = Position;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_enemyType != "Crow")
        {
            _velocity.y += _gravity * delta;
        }
        
    }

    public void BaseMovementControl()
    {
        _velocity = CalculateVelocity(_velocity, _direction, _speed);

        // snap down or at zero
        Vector2 snap = (_direction.y == 0.0f) ? new Vector2(0, 1) * 10.0f : new Vector2(0, 0);

        _velocity = MoveAndSlideWithSnap(_velocity, snap, FLOORNORMAL, true);

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

        //GD.Print("Velocity = " + _velocity);
    }

    public Vector2 CalculateVelocity(Vector2 linearVelocity, Vector2 direction, Vector2 speed)
    {
        Vector2 velocity = linearVelocity;

        velocity.x = speed.x * direction.x;

        if (direction.y != 0.0) velocity.y = speed.y * direction.y;

        return velocity;
    }

    public void SprAnimation(string animation)
    {
        _ndAnimEnemy.Play(animation);

       //GD.Print("Enemy Animation = " + animation);


    }

    public virtual void WanderLogic(Vector2 direction)
    {

    }

    public bool PlayerDetected()
    {
        float playerPosX = _ndObjPlayer.Position.x;
        float playerPosY = _ndObjPlayer.Position.y;
        bool detectRangeX = (playerPosX < Position.x + _detectionRadius.x && playerPosX > Position.x - _detectionRadius.x) ? true : false;
        bool detectRangeY = (playerPosY < Position.y + _detectionRadius.y && playerPosY > Position.y - _detectionRadius.y) ? true : false;

        return detectRangeX && detectRangeY;
    }

    public bool EnemyReturn()
    {
        float dirX = 0;
        //float dirY = 0;
        bool output;
        float returnRadius = 5;
        
        /*
        if (_enemyType == "Crow")
        {
            bool xRange = (_startPos.x + returnRadius > Position.x && _startPos.x - returnRadius < Position.x) ? true : false;
            bool yRange = (_startPos.y + returnRadius > Position.y && _startPos.y - returnRadius < Position.y) ? true : false;

            if (xRange && yRange)
            {
                output = true;
            }
            else
            {
                dirX = (_startPos.x > Position.x) ? 1 : -1;
                dirY = (_startPos.y > Position.y) ? 1 : -1;
                output = false;
            }

            _direction = new Vector2(dirX, dirY);
        }
        else
        */
        {
            if (_startPos.x + returnRadius > Position.x && _startPos.x - returnRadius < Position.x)
            {
                output = true;
            }
            else
            {
                dirX = (_startPos.x > Position.x) ? 1 : -1;
                output = false;
            }

            _direction = new Vector2(dirX, 0);
        }

        
        return output;
    }

    public bool EnemyAttack()
    {
        float playerPosX = _ndObjPlayer.Position.x;
        float playerPosY = _ndObjPlayer.Position.y;
        bool attackRangeX = false;

        if (_direction.x == 1)
        {
            attackRangeX = (playerPosX > Position.x && playerPosX < Position.x + _attackRadius.x) ? true : false;
        }
        else if (_direction.x == -1)
        {
            attackRangeX = (playerPosX < Position.x && playerPosX > Position.x - _attackRadius.x) ? true : false;
        }


        bool attackRangeY = (playerPosY < Position.y + _attackRadius.y && playerPosY > Position.y - _attackRadius.y) ? true : false;

        // if within range affect the players hp stat and turn him to a damage state
        return attackRangeX && attackRangeY; 
    }

    public bool EnemyRangeAttack()
    {
        // dont run if enemy has nor range radius
        if (_rangeRadius == new Vector2(0, 0))
        {
            return false;
        }

        float playerPosX = _ndObjPlayer.Position.x;
        float playerPosY = _ndObjPlayer.Position.y;
        bool attackRangeX = false;

        if (_direction.x == 1)
        {
            attackRangeX = (playerPosX > Position.x && playerPosX < Position.x + _rangeRadius.x) ? true : false;
        }
        else if (_direction.x == -1)
        {
            attackRangeX = (playerPosX < Position.x && playerPosX > Position.x - _rangeRadius.x) ? true : false;
        }


        bool attackRangeY = (playerPosY < Position.y + _rangeRadius.y && playerPosY > Position.y - _rangeRadius.y) ? true : false;

        // if within range affect the players hp stat and turn him to a damage state
        _curSkill = _skillA;
        _useSkill = true;
        return attackRangeX && attackRangeY;
    }

    public void EnemyTurnIdle()
    {
        if (_velocity == new Vector2(0, 0))
        {
            stateMachine.TransitionToState(enemyIdle);
        }
    }

    public void OnAnimationFinished(string anim_name)
    {
        GD.Print("Animation " + anim_name + " Finished");
        _isAnimationOver = true;
        
    }

    public void OnAreaShapeStompEntered(Area2D area)
    {
        //GD.Print("Enemy Area Position X = " + area.GlobalPosition.x + " Y = " + area.GlobalPosition.y);
        // do not activate stomp if not entered in the correct y position
        if (area.GlobalPosition.y > _ndStompArea.GlobalPosition.y )
        {
            return;
        }

        // do not activated stomp damage if hit during a skill use
        if (_ndObjPlayer.UseSkill == true)
        {
            return;
        }

        if (area.GetParent() is SkillMove)
        {
            return;
        }

            GD.Print("Stomped On");
        // damage calculation
        _isStomped = true;

        // modify player damage
        _ndObjPlayer.CurDmg = _ndObjPlayer.CurAttack;
        _ndObjPlayer.IsPhysical = true;
    }

    public void ChangeHealth(float health)
    {
        _health += health;

        // clamp stats
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        GD.Print(_enemyType +"'s Health = " + _health);
    }

    public void BattledDamage(float attackerAttack, bool isPhysical)
    {
        int totalDamage;
        //coded for a physical stomp attack at the moment
        if (isPhysical)
        {
            totalDamage = DamageCalculation.damageEquation(attackerAttack, _curdefense);
        }
        else
        {
            totalDamage = DamageCalculation.damageEquation(attackerAttack, _curspDefense);
        }
        GD.Print("DAMAGE TIME");
        Console.WriteLine("I AM HURT...THIS IS WHAT HAPPENED" + totalDamage);
        ChangeHealth(-totalDamage);
        // stomp = 5 special phys
        // moveskill = 20
    }

    public int Battled(float attackerAttack, bool isPhysical)
    {
        int totalDamage;
        //coded for a physical stomp attack at the moment
        if (isPhysical)
        {
            totalDamage = DamageCalculation.damageEquation(attackerAttack, _curdefense);
        }
        else
        {
            totalDamage = DamageCalculation.damageEquation(attackerAttack, _curspDefense);
        }

        return totalDamage;
        // stomp = 5 special phys
        // moveskill = 20

    }

}
