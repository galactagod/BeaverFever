using Godot;
using System;

public class ObjPlayer : BaseMovementAct
{
    // member vars
    private float _curattack = 2;
    private float _curdefense = 2;
    private float _curspAttack = 2;
    private float _curspDefense = 2;
    private bool _isInAir = false;
    private float collBasePositionX;
    private float collStompPositionX;
    private bool _isDamaged = false;
    private EnemyMovementAct _enemyMovementAct;
    private int _damagedTimer = 0;
    private bool _isAnimationOver = false;
    private float _stompImpulseY = 600;
    private float _stompImpulseX = 150;
    private float _hurtImpulseY = 300;
    private float _hurtImpulseX = 150;
    private bool _stompJump = false;
    private int _stompJumpTimer = 0;
    private int _timer = 0;
    protected readonly Random _rnd = new Random();

    // sound paths
    private AudioStreamSample _sndJump = (AudioStreamSample)GD.Load("res://src/Assets/Sounds/Movement/SndJumpA.wav");
    private AudioStreamSample _sndHurt = (AudioStreamSample)GD.Load("res://src/Assets/Sounds/Battle/SndCryAMod.wav");
    private AudioStreamSample _sndHit = (AudioStreamSample)GD.Load("res://src/Assets/Sounds/Battle/hit_p13_b.wav");

    // node reference
    private PlayerStats _ndPlayerStats;
    private LevelControl _ndLevelControl;
    private AnimationPlayer _ndAnimPlayer;
    private AnimatedSprite _ndSprPlayer;
    private CollisionShape2D _ndCollBase;
    private Area2D _ndStompDetector;
    private CollisionShape2D _ndCollStomp;
    private PlayerData _ndPlayerData;
    
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
    public float CurDmg { get { return _curattack; } set { _curattack = value; } }
    public float CurDef { get { return _curdefense; } set { _curdefense = value; } }
    public float CurSpAttack { get { return _curspAttack; } set { _curspAttack = value; } }
    public float CurSpDefense { get { return _curspDefense; } set { _curspDefense = value; } }
    public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }

    public EnemyMovementAct Attacker { get { return _enemyMovementAct; } set { _enemyMovementAct = value; } }
    public int DamagedTimer { get { return _damagedTimer; } set { _damagedTimer = value; } }
    public bool StompJump { get { return _stompJump; } set { _stompJump = value; } }
    public bool IsAnimationOver { get { return _isAnimationOver; } set { _isAnimationOver = value; } }
    public bool IsInAir { get { return _isInAir; } set { _isInAir = value; } }
    public PlayerStats NdPlayerStats { get { return _ndPlayerStats; } set { _ndPlayerStats = value; } }
    public AnimatedSprite NdSprPlayer { get { return _ndSprPlayer; } set { _ndSprPlayer = value; } }

    public PlayerData NdPlayerData { get { return _ndPlayerData; } set { _ndPlayerData = value; } }

    public override void _Ready()
    {
        // acquire node references
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndLevelControl = GetNode<LevelControl>("/root/LevelControl");
        _ndAnimPlayer = GetNode<AnimationPlayer>("AnimPlayer");
        _ndSprPlayer = GetNode<AnimatedSprite>("SprPlayer");
        _ndCollBase = GetNode<CollisionShape2D>("CollBase");
        _ndStompDetector = GetNode<Area2D>("StompDetector");
        _ndCollStomp = GetNode<CollisionShape2D>("StompDetector/CollStomp");
        _ndPlayerData = GetNode<PlayerData>("/root/PlayerData");

        _ndStompDetector.Connect("area_entered", this, nameof(OnAreaShapeStompEntered));
        _ndAnimPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));

        // start state
        stateMachine = new PlayerStateMachineManager(this, playerIdle);

        collBasePositionX = _ndCollBase.Position.x;
        collStompPositionX = _ndCollStomp.Position.x;
        _ndPlayerStats.PlayerPos = Position;

        CurDmg = _ndPlayerData.attackFinal + 2;
        CurDef = _ndPlayerData.defenseFinal + 2;
        CurSpAttack = _ndPlayerData.spAttackFinal + 2;
        CurSpDefense = _ndPlayerData.spDefenseFinal + 2;
    }


    public override void _PhysicsProcess(float delta)
    {

        base._PhysicsProcess(delta);
        _ndPlayerStats.PlayerPos = Position;
        _timer++;

        CurDmg = _ndPlayerData.attackFinal + 2;
        CurDef = _ndPlayerData.defenseFinal + 2;
        CurSpAttack = _ndPlayerData.spAttackFinal + 2;
        CurSpDefense = _ndPlayerData.spDefenseFinal + 2;

        stateMachine.Update();
        //GD.Print("Xposition = " + Position.x);
        //_velocity.y += _gravity * delta;
        //GD.Print("new velocity Y = " + _velocity.y);
        //GD.Print("gravity = " + _gravity);
        if (_isDamaged)
        {
            int maxDmgTime = 150;
            Visible = Calculations.HitFlash(_ndLevelControl, _sndHit, _damagedTimer, Visible);
            _damagedTimer++;
            if (_damagedTimer == maxDmgTime)
            {
                _damagedTimer = 0;
                _isDamaged = false;
                Visible = true;
            }
        }

        // energy naturally replenishes
        _ndPlayerStats.ReplenishEnergy(0.002f);
    }

    public void BaseMovementControl()
    {
        bool jumpInterrupted = (Input.IsActionJustReleased("ui_up") && _velocity.y < 0) ? true : false;
        _direction = GetDirection();
        _velocity = CalculateVelocity(_velocity, _direction, _speed, jumpInterrupted);

        // snap down or at zero
        Vector2 snap = (_direction.y == 0) ? new Vector2(0, 1) * 10 : new Vector2(0, 0);


        _velocity = MoveAndSlideWithSnap(_velocity, snap, FLOORNORMAL, true);


        switch (_direction.x)
        {
            // make sure base position not at zero for flips
            case -1:
                _ndSprPlayer.FlipH = true;
                _ndCollBase.Position = new Vector2(-collBasePositionX, _ndCollBase.Position.y);
                _ndCollStomp.Position = new Vector2(-5f, _ndCollStomp.Position.y);
                //GD.Print("BASE POS = " + _ndCollBase.Position);
                //GD.Print("STOMP POS = " + _ndCollStomp.Position);
                break;

            case 1:
                _ndSprPlayer.FlipH = false;
                _ndCollBase.Position = new Vector2(collBasePositionX, _ndCollBase.Position.y);
                _ndCollStomp.Position = new Vector2(5f, _ndCollStomp.Position.y);
                //GD.Print("BASE POS = " + _ndCollBase.Position);
                //GD.Print("STOMP POS = " + _ndCollStomp.Position);
                break;
        }
    }

    public Vector2 GetDirection()
    {
        float xDirection = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        float yDirection = (IsOnFloor() && Input.IsActionJustPressed("ui_up")) ? -Input.GetActionStrength("ui_up") : 0;

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
            //GD.Print("Air........");
            //GD.Print("velocityX = " + _velocity.x);
            //GD.Print("velocityY = " + _velocity.y);
            //velocity.x += direction.x;
        }
        else if (_damagedTimer > 0 && _damagedTimer < 30)
        {
            
            //GD.Print("damaging........");
            //GD.Print("velocityX = " + _velocity.x);
            //GD.Print("velocityY = " + _velocity.y);
        }
        else
        {
            _stompJumpTimer = 0;
            velocity.x = speed.x * direction.x;
        }

        if (direction.y != 0) velocity.y = speed.y * direction.y;

        if (jumpInterrupted) velocity.y = 0;

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

            // run audio on animation
            PlayAudio(animation);
        }    
    }

    public void PlayAudio(string animation)
    {
        switch (animation)
        {
            case "Jump": _ndLevelControl.SfxPlayerManager(-1, _sndJump, 5, 1.3f); break;
            case "Hurt": 
                _ndLevelControl.SfxPlayerManager(-1, _sndHurt, 10, 0.65f);
                float hurtImpulsX = (_ndSprPlayer.FlipH) ? -_hurtImpulseX : _hurtImpulseX;
                float hurtImpulsY = -_hurtImpulseY;
                GD.Print("_hurtImpulseX = " + _hurtImpulseX);
                GD.Print("_hurtImpulseY = " + _hurtImpulseY);

                _velocity = new Vector2(hurtImpulsX, hurtImpulsY);
                _direction.y = -1;
                break;
            
        }
    }

    public Vector2 CalculateStompVelocity()
    {
        float stompJumpX = (_ndSprPlayer.FlipH) ? -_stompImpulseX :  _stompImpulseX;
        float stompJumpY = (Input.IsActionPressed("ui_up")) ? -_speed.y : -_stompImpulseY;
        GD.Print("STOMPJUMPX = " + stompJumpX);
        GD.Print("STOMPJUMPY = " + stompJumpY);
        // X may not be modifying because of movement control reseting x value
        return new Vector2(stompJumpX, stompJumpY);
    }

    public void OnAreaShapeStompEntered(Area2D area)
    {
        if (area.GlobalPosition.y < _ndStompDetector.GlobalPosition.y)
        {
            return;
        }

        GD.Print("Stomp Jump");
 
        _stompJump = true;
        _stompJumpTimer++;
        _velocity = CalculateStompVelocity();
    }

    public void OnAnimationFinished(string anim_name)
    {
        //GD.Print("Animation " + anim_name + " Finished");
        _isAnimationOver = true;

    }
    public void Battled()
    {
        //only have it for physical attack at the moment
        Console.WriteLine("Attacker damage" + Attacker.CurDmg);
        Console.WriteLine("Damage Calced" + DamageCalculation.damageEquation(Attacker.CurDmg, CurDef));
        NdPlayerStats.ChangeHealth(-DamageCalculation.damageEquation(Attacker.CurDmg, CurDef));
    }
}
