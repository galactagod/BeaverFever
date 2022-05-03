using Godot;
using System;

public class ObjPlayer : BaseMovementAct
{
    // member vars
    private float _curattack = 2;
    private float _curDmg = 0;
    private float _curdefense = 2;
    private float _curspAttack = 2;
    private float _curspDefense = 2;
    private bool _isPhysical;
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
    private string _curSkill = "";
    private bool _useSkill = false;
    protected readonly Random _rnd = new Random();

    private Vector2 _origSpeed = new Vector2(300, 600);
    private int _speedCoolDown = 0;
    private int _speedMaxCoolDown = 500;

    private float _initDefense = 2;
    private float _origCurDefense = 2;
    private int _defCoolDown = 0;
    private int _defMaxCoolDown = 700;


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
    public PlayerAir playerAir = new PlayerAir();
    public PlayerCrawl playerCrawl = new PlayerCrawl();
    public PlayerCrouch playerCrouch = new PlayerCrouch();
    public PlayerHurt playerHurt = new PlayerHurt();
    public PlayerIdle playerIdle = new PlayerIdle();
    public PlayerSkill playerSkill = new PlayerSkill();
    public PlayerSwim playerSwim = new PlayerSwim();
    public PlayerWalk playerWalk = new PlayerWalk();

    // getters and setters
    public float CurAttack { get { return _curattack; } set { _curattack = value; } }
    public float CurDmg { get { return _curDmg; } set { _curDmg = value; } }
    public float CurDef { get { return _curdefense; } set { _curdefense = value; } }
    public float CurSpAttack { get { return _curspAttack; } set { _curspAttack = value; } }
    public float CurSpDefense { get { return _curspDefense; } set { _curspDefense = value; } }
    public Vector2 Speed { get { return _speed; } set { _speed = value; } }
    public Vector2 OrigSpeed { get { return _origSpeed; } set { _origSpeed = value; } }
    public int SpeedCoolDown { get { return _speedCoolDown; } set { _speedCoolDown = value; } }
    public int SpeedMaxCoolDown { get { return _speedMaxCoolDown; } set { _speedMaxCoolDown = value; } }

    public float OrigCurDefense { get { return _origCurDefense; } set { _origCurDefense = value; } }
    public float InitDefense { get { return _initDefense; } set { _initDefense = value; } }
    public int DefCoolDown { get { return _defCoolDown; } set { _defCoolDown = value; } }
    public int DefMaxCoolDown { get { return _defMaxCoolDown; } set { _defMaxCoolDown = value; } }

    public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }
    public bool IsPhysical { get { return _isPhysical; } set { _isPhysical = value; } }

    public EnemyMovementAct Attacker { get { return _enemyMovementAct; } set { _enemyMovementAct = value; } }
    public int DamagedTimer { get { return _damagedTimer; } set { _damagedTimer = value; } }
    public bool StompJump { get { return _stompJump; } set { _stompJump = value; } }
    public bool IsAnimationOver { get { return _isAnimationOver; } set { _isAnimationOver = value; } }
    public bool IsInAir { get { return _isInAir; } set { _isInAir = value; } }
    public string CurSkill { get { return _curSkill; } set { _curSkill = value; } }
    public bool UseSkill { get { return _useSkill; } set { _useSkill = value; } }
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

        CurAttack = _ndPlayerData.attackFinal + 2;
        CurDef = _ndPlayerData.defenseFinal + _initDefense;
        CurSpAttack = _ndPlayerData.spAttackFinal + 2;
        CurSpDefense = _ndPlayerData.spDefenseFinal + 2;
    }


    public override void _PhysicsProcess(float delta)
    {

        base._PhysicsProcess(delta);
        _ndPlayerStats.PlayerPos = Position;
        _timer++;

        CurAttack = _ndPlayerData.attackFinal + 2;
        CurDef = _ndPlayerData.defenseFinal + _initDefense;
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
        if (!_ndPlayerStats.EnergyPause)
        {
            _ndPlayerStats.ChangeReplenishEnergy(0.005f);
        }

        // replenish health if regeration is on and set its cooldown to zero

        bool extraMoney = false;

        for (int i = 0; i < 3; i++)
        {
            if (_ndPlayerStats.SkillNames[i] == "Regeneration")
            {
                // check which regen tier and apply effect
                switch (_ndPlayerStats.SkillTiers[i])
                {
                    case 1: _ndPlayerStats.ChangeReplenishHealth(0.002f); break;
                    case 2: _ndPlayerStats.ChangeReplenishHealth(0.004f); break;
                    case 3: _ndPlayerStats.ChangeReplenishHealth(0.006f); break;
                }
                // set cooldown to 0
                //_ndPlayerStats.SkillCoolDowns[i] = 0;
            }

            if (_ndPlayerStats.SkillNames[i] == "Grace")
            {
                switch (_ndPlayerStats.SkillTiers[i])
                {
                    case 1: _ndPlayerStats.MoneyMultiplier = 1.5f; extraMoney = true; break;
                    case 2: _ndPlayerStats.MoneyMultiplier = 2; extraMoney = true; break;
                    case 3: _ndPlayerStats.MoneyMultiplier = 3; extraMoney = true; break;
                }
            }
        }

        if (extraMoney == false) _ndPlayerStats.MoneyMultiplier = 1;


        // naturally decrease cooldown
        _ndPlayerStats.ChangeCooldown(0.1f);

        // speedcoolldown checker
        _speedCoolDown = Mathf.Clamp(_speedCoolDown - 1, 0, _speedMaxCoolDown);
        if (_speedCoolDown == 0) _speed =_origSpeed;


        // defense checker
        _defCoolDown = Mathf.Clamp(_defCoolDown - 1, 0, _defMaxCoolDown);
        if (_defCoolDown == 0) _initDefense = _origCurDefense;


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

    public bool ActivateSkill()
    {
        int skill = 0;

        string[] skillNames = _ndPlayerStats.SkillNames;
        float[] SkillCoolDowns = _ndPlayerStats.SkillCoolDowns;
        float[] skillEnergy = _ndPlayerStats.SkillEnergyUse;

        if (Input.IsActionJustPressed("SkillA"))
        {
            if (skillNames[0] != "" && SkillCoolDowns[0] == 0 && skillEnergy[0] < _ndPlayerStats.Energy)
            {
                skill = 1;
                _curSkill = skillNames[0];
            }
        }
        else if (Input.IsActionJustPressed("SkillB"))
        {
            if (skillNames[1] != "" && SkillCoolDowns[1] == 0 && skillEnergy[1] < _ndPlayerStats.Energy)
            {
                skill = 2;
                _curSkill = skillNames[1];
            }
        }
        else if (Input.IsActionJustPressed("SkillC"))
        {
            if (skillNames[2] != "" && SkillCoolDowns[2] == 0 && skillEnergy[2] < _ndPlayerStats.Energy)
            {
                skill = 3;
                _curSkill = skillNames[2];
            }
        }

        if (_isInAir && (_curSkill == "Accelerate" || _curSkill == "Aegis"))
        {
            _useSkill = false;
            return false;
        }
        else
        {
            if (skill != 0)
            {
                _useSkill = true;
                // reset cool down and change energy
                _ndPlayerStats.SkillCoolDowns[skill - 1] = _ndPlayerStats.SkillMaxCoolDowns[skill - 1];
                _ndPlayerStats.ChangeEnergy(-_ndPlayerStats.SkillEnergyUse[skill - 1]);
            }

            if (_curSkill == "Regeneration" || _curSkill == "Grace")
            {
                _useSkill = false;
                return false;
            }

            return (skill != 0);
        }
        
    }

    public Vector2 GetDirection()
    {
        float xDirection = 0;
        float yDirection = 0;

        if (!_useSkill)
        {
            xDirection = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            yDirection = (IsOnFloor() && Input.IsActionJustPressed("ui_up")) ? -Input.GetActionStrength("ui_up") : 0;
        }
        

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

    public void AttackAnimation(string curSkill, string pos)
    {
        string animation = "";

        switch (curSkill)
        {
            case "Slice":
                animation = (!IsOnFloor()) ? "AirAttackA" : "AttackA";
                break;
            case "Crunch": case "BubbleBurst":
                animation = (!IsOnFloor()) ? "AirAttackB" : "AttackB";
                break;
            case "Aegis": case "Accelerate":
                animation = "Buff";
                break;
        }

        if (pos == "start")
        {
            _ndAnimPlayer.Play(animation);
        }
        else if (pos == "end")
        {
            //_ndSprPlayer.Animation = animation;
            //_ndSprPlayer.Frame = 1;
            _ndAnimPlayer.Play(animation);
            _ndAnimPlayer.Seek(0.1f, true);
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
