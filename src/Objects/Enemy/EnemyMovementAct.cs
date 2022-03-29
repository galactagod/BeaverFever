using Godot;
using System;

public class EnemyMovementAct : KinematicBody2D
{
    // maintain the vector2 upwards
    // csharp limitation, unable to set non primitive types to constants
    protected Vector2 FLOORNORMAL = new Vector2(0, -1);

    [Export] public Vector2 _speed = new Vector2(300, 400);
    [Export] public float _gravity = 2000;

    protected Vector2 _velocity = new Vector2(0, 0);
    protected Vector2 _direction = new Vector2(0, 0);
    protected Vector2 _detectionRadius = new Vector2(200, 50);
    protected Vector2 _attackRadius = new Vector2(55, 50);
    protected Vector2 _startPos;
    protected bool _isAnimationOver = false;
    protected bool _isStomped = false;
    protected float _collBasePositionX;
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
    public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
    public Vector2 Speed { get { return _speed; } set { _speed = value; } }
    public bool IsAnimationOver { get { return _isAnimationOver; } set { _isAnimationOver = value; } }
    public bool IsStomped { get { return _isStomped; } set { _isStomped = value; } }
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
        _ndStompArea.Connect("area_entered", this, nameof(OnAreaShapeEntered));
        _collBasePositionX = _ndCollBaseEnemy.Position.x;
        _startPos = Position;
    }

    public override void _PhysicsProcess(float delta)
    {
        _velocity.y += _gravity * delta;
        

        // Base Land Ai Logic
        /*(check for y location since enemy may chase player at same x level but completely different y's
         * Idle -> Wander
         * if detect player -> chase
         * chase-> attack/wander
         * if player within attack range -> attack
         * if player out of detection -> wander/return
         */
        //GD.Print("new velocity Y = " + _velocity.y);
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
                _ndCollBaseEnemy.Position = new Vector2(-_collBasePositionX, _ndCollBaseEnemy.Position.y);
                break;

            case 1:
                _ndSprEnemy.FlipH = true;
                _ndCollBaseEnemy.Position = new Vector2(_collBasePositionX, _ndCollBaseEnemy.Position.y);
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
        bool output;
        float returnRadius = 5;

        if (_startPos.x + returnRadius > Position.x && _startPos.x - returnRadius < Position.x)
        {
            output = true;
        }
        else
        {
            dirX = (_startPos.x > Position.x ) ? 1 : -1;
            output = false;
        }

        _direction = new Vector2(dirX, 0);
        return output;
    }

    public bool EnemyAttack()
    {
        float playerPosX = _ndObjPlayer.Position.x;
        float playerPosY = _ndObjPlayer.Position.y;
        bool attackRangeX = (playerPosX < Position.x + _attackRadius.x && playerPosX > Position.x - _attackRadius.x) ? true : false;
        bool attackRangeY = (playerPosY < Position.y + _attackRadius.y && playerPosY > Position.y - _attackRadius.y) ? true : false;

        // if within range affect the players hp stat and turn him to a damage state
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

    public void OnAreaShapeEntered(Area2D area)
    {
        
        // do not activate stomp if not entered in the correct y position
        if (area.Position.y > _ndStompArea.Position.y)
        {
            return;
        }

        GD.Print("Stomped On");
        // damage calculation
        _isStomped = true;
        // if dead then queue_free()
    }

}