using Godot;
using System;

public class ObjPlayer : BaseMovementAct
{
    // member vars
    private float _curdmg = 2;
    private bool _isInAir = false;
    private float collBasePositionX;
    private float collStompPositionX;
    private bool _isDamaged = false;
    private int _damagedTimer = 0;
    private bool _isAnimationOver = false;
    private float _stompImpulseY = 600;
    private float _stompImpulseX = 150;
    private bool _stompJump = false;
    private int _stompJumpTimer = 0;
    private int _timer = 0;
    protected readonly Random _rnd = new Random();

    // node reference
    private PlayerStats _ndPlayerStats;
    private AnimationPlayer _ndAnimPlayer;
    private AnimatedSprite _ndSprPlayer;
    private CollisionShape2D _ndCollBase;
    private Area2D _ndStompDetector;
    private CollisionShape2D _ndCollStomp;

    // state setups
    protected PlayerStateMachineManager stateMachine;
    public PlayerCrawl playerCrawl = new PlayerCrawl();
    public PlayerCrouch playerCrouch = new PlayerCrouch();
    public PlayerHurt playerHurt = new PlayerHurt();
    public PlayerIdle playerIdle = new PlayerIdle();
    public PlayerAir playerAir = new PlayerAir();
    public PlayerSwim playerSwim = new PlayerSwim();
    public PlayerWalk playerWalk = new PlayerWalk();

    // getters and setters
    public float CurDmg { get { return _curdmg; } set { _curdmg = value; } }
    public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }
    public int DamagedTimer { get { return _damagedTimer; } set { _damagedTimer = value; } }
    public bool StompJump { get { return _stompJump; } set { _stompJump = value; } }
    public bool IsAnimationOver { get { return _isAnimationOver; } set { _isAnimationOver = value; } }
    public bool IsInAir { get { return _isInAir; } set { _isInAir = value; } }
    public PlayerStats NdPlayerStats { get { return _ndPlayerStats; } set { _ndPlayerStats = value; } }
    public AnimatedSprite NdSprPlayer { get { return _ndSprPlayer; } set { _ndSprPlayer = value; } }

    public override void _Ready()
    {
        // acquire node references
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndAnimPlayer = GetNode<AnimationPlayer>("AnimPlayer");
        _ndSprPlayer = GetNode<AnimatedSprite>("SprPlayer");
        _ndCollBase = GetNode<CollisionShape2D>("CollBase");
        _ndStompDetector = GetNode<Area2D>("StompDetector");
        _ndCollStomp = GetNode<CollisionShape2D>("StompDetector/CollStomp");

        _ndStompDetector.Connect("area_entered", this, nameof(OnAreaShapeEntered));
        _ndAnimPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));


        // start state
        stateMachine = new PlayerStateMachineManager(this, playerIdle);

        collBasePositionX = _ndCollBase.Position.x;
        collStompPositionX = _ndCollStomp.Position.x;
        _ndPlayerStats.PlayerPos = Position;
    }


    public override void _PhysicsProcess(float delta)
    {

        base._PhysicsProcess(delta);
        _ndPlayerStats.PlayerPos = Position;
        _timer++;

        stateMachine.Update();
        //GD.Print("Xposition = " + Position.x);
        //_velocity.y += _gravity * delta;
        //GD.Print("new velocity Y = " + _velocity.y);
        //GD.Print("gravity = " + _gravity);
        if (_isDamaged)
        {
            int maxDmgTime = 150;
            Visible = Calculations.HitFlash(_damagedTimer, Visible);
            _damagedTimer++;
            if (_damagedTimer == maxDmgTime)
            {
                _damagedTimer = 0;
                _isDamaged = false;
                Visible = true;
            }
        }
            


    }

    public void BaseMovementControl()
    {
        bool jumpInterrupted = (Input.IsActionJustReleased("ui_up") && _velocity.y < 0.0f) ? true : false;
        _direction = GetDirection();
        _velocity = CalculateVelocity(_velocity, _direction, _speed, jumpInterrupted);

        // snap down or at zero
        Vector2 snap = (_direction.y == 0.0f) ? new Vector2(0, 1) * 10.0f : new Vector2(0, 0);

        _velocity = MoveAndSlideWithSnap(_velocity, snap, FLOORNORMAL, true);

        switch (_direction.x)
        {
            // make sure base position not at zero for flips
            case -1:
                _ndSprPlayer.FlipH = true;
                _ndCollBase.Position = new Vector2(-collBasePositionX, _ndCollBase.Position.y);
                _ndCollStomp.Position = new Vector2(-15, _ndCollStomp.Position.y);
                //GD.Print("BASE POS = " + _ndCollBase.Position);
                //GD.Print("STOMP POS = " + _ndCollStomp.Position);
                break;

            case 1:
                _ndSprPlayer.FlipH = false;
                _ndCollBase.Position = new Vector2(collBasePositionX, _ndCollBase.Position.y);
                _ndCollStomp.Position = new Vector2(0, _ndCollStomp.Position.y);
                //GD.Print("BASE POS = " + _ndCollBase.Position);
                //GD.Print("STOMP POS = " + _ndCollStomp.Position);
                break;
        }
    }

    public Vector2 GetDirection()
    {
        float xDirection = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        float yDirection = (IsOnFloor() && Input.IsActionJustPressed("ui_up")) ? -Input.GetActionStrength("ui_up") : 0.0f;
        //GD.Print("new Direction X = " + xDirection);

        return new Vector2(xDirection, yDirection); 
    }

    public Vector2 CalculateVelocity(Vector2 linearVelocity, Vector2 direction, Vector2 speed, bool jumpInterrupted)
    {
        Vector2 velocity = linearVelocity;

        if (_stompJumpTimer > 0 && _isInAir)
        {
            _stompJumpTimer++;
            if (_stompJumpTimer == 100) _stompJumpTimer = 0;
            velocity.x = velocity.x + (GetPhysicsProcessDeltaTime() * direction.x);
        }
        else
        {
            _stompJumpTimer = 0;
            velocity.x = speed.x * direction.x;
        }

        if (direction.y != 0.0) velocity.y = speed.y * direction.y;

        if (jumpInterrupted) velocity.y = 0.0f;

        return velocity;
    }

    public void SprAnimation(string animation)
    {
        // prevents player from constantly switching btw idle and walk/other states too quickly
        if (_velocity == new Vector2(0, 0) || (IsOnWall() && !_isInAir && !_isDamaged))
        {
            _ndAnimPlayer.Play("Idle"); //Idle
            //GD.Print("Animation === " + animation + " but its idle");
        }
        else
        {
            _ndAnimPlayer.Play(animation);
            //GD.Print("Animation === " + animation);
        }   
        
    }

    public Vector2 CalculateStompVelocity()
    {
        //float mod = (_rnd.Next(0, 2) == 0) ? -1 : 1;
        float stompJumpX = (_ndSprPlayer.FlipH) ? -_stompImpulseX :  _stompImpulseX;
        float stompJumpY = (Input.IsActionPressed("ui_up")) ? -_speed.y : -_stompImpulseY;
        //GD.Print("STOMPJUMPX = " + stompJumpX);
        //GD.Print("STOMPJUMPY = " + stompJumpY);
        // X may not be modifying because of movement control reseting x value
        return new Vector2(stompJumpX, stompJumpY);
    }

    public void OnAreaShapeEntered(Area2D area)
    {
        GD.Print("Stomp Jump");
        _stompJump = true;
        _stompJumpTimer++;
        _velocity = CalculateStompVelocity();

    }

    public void OnAnimationFinished(string anim_name)
    {
        GD.Print("Animation " + anim_name + " Finished");
        _isAnimationOver = true;

    }
}
