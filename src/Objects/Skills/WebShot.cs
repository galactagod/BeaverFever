using Godot;
using System;

public class WebShot : SkillMove
{
    private Texture _spriteA = (Texture)GD.Load("res://src/Assets/Moves/SprWebA.png");
    private Texture _spriteB = (Texture)GD.Load("res://src/Assets/Moves/SprWebB.png");
    private Area2D _ndArea;
    private Sprite _ndSpriteA;
    private Sprite _ndSpriteB;
    private Tween _ndTween;
    private Vector2 _enemySprSize;
    private Vector2 _skillPos;

    private int _hFrame = 1;
    private bool _isWebbed = false;

    // times
    private float _sprWebTimer = 0;
    private float _sprWebStopTimer = 0;
    private float _rotationMaxTime;
    private float _moveStartTime;
    private float _moveMaxTime;
    private float _arcHeight;
    private float _arcMaxHeight;
    private float _spin;
    private float _arc;
    private bool _expandWeb = false;

    public WebShot(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {
    }

    public override void _Ready()
    {
        base._Ready();

        // create sprite + area + collision
        _ndSpriteA = CreateSprite(_spriteA, _hFrame);
        Vector2 skillSprSize = _ndSpriteA.GetRect().Size;
        _ndArea = CreateArea(skillSprSize / 2);
        _ndTween = CreateTween();

        // connections
        _ndArea.Connect("body_entered", this, nameof(OnBodyEntered));
        _ndTween.Connect("tween_completed", this, nameof(OnTweenCompletion));

        // set position based on user sprite height
        string anim = _enemy.NdSprEnemy.Animation;
        int frame = _enemy.NdSprEnemy.Frame;

        _enemySprSize = _enemy.NdSprEnemy.Frames.GetFrame(anim, frame).GetSize();


        Position = new Vector2(Position.x , Position.y - (_enemySprSize.y / 2 + skillSprSize.y / 2));
        
        _ndSpriteA.FlipH = _enemy.NdSprEnemy.FlipH;

        // set up some times
        _rotationMaxTime = 3 * (360);
        _moveStartTime = 40;
        _moveMaxTime = 70;

        // movement info
        _arcHeight = 300;
        _userPos = _enemy.Position;
        _targetPos = _player.Position;
        _skillPos = Position;
    }

    public override void _PhysicsProcess(float delta)
    {
        // web after enemy last frame times
        if (_sprWebTimer >= _moveStartTime && _sprWebTimer <= _moveStartTime + _moveMaxTime)
        {
            WebMovement();
        }
        else if (_sprWebTimer > _moveStartTime + _moveMaxTime)
        {
            // if the web ends its position then expand web
            if (!_expandWeb)
            {
                _ndArea.QueueFree();
                _ndSpriteA.Texture = _spriteB;
                _ndSpriteA.Scale = new Vector2(0, 0);
                _expandWeb = true;
                GD.Print("expand web");
                _ndTween.InterpolateProperty(_ndSpriteA, "scale:x", _ndSpriteA.Scale.x, _ndSpriteA.Scale.x + 1, 1, Tween.TransitionType.Quart, Tween.EaseType.Out);
                _ndTween.InterpolateProperty(_ndSpriteA, "scale:y", _ndSpriteA.Scale.y, _ndSpriteA.Scale.y + 1, 1, Tween.TransitionType.Quart, Tween.EaseType.Out);
                _ndTween.InterpolateProperty(_ndSpriteA, "modulate:a", 1, 0, 1, Tween.TransitionType.Sine, Tween.EaseType.In);
                _ndTween.Start();
                // let spider use the move again without having to wait for web expanse
                _enemy.UseSkill = false;
            }


        }
        _sprWebTimer++;

    }

    public void WebMovement()
    {
        // get some quick info
        float tempTimer = _sprWebTimer - _moveStartTime;
        
        float userCenterPosY = _userPos.y - _enemySprSize.y / 2;

        // max room height from enemies position
        _arcMaxHeight = Math.Abs(userCenterPosY);

        // if were above the room clamp it down a bit
        if (userCenterPosY - _arcHeight < 0) _arcHeight = _arcMaxHeight;
        //_arcHeight = -400;
        if (_arcHeight > 200) _arcHeight = 200;


        // web moving sound should play twice
        //if (tempTimer % Math.Round((double)_moveMaxTime / 2) == 15) playsound(websound);

        // rotation
        _spin = (float)CustomTween(tempTimer, 0, _rotationMaxTime, _moveMaxTime);

        // arc movement
        _arc = ArcMovement(0, _arcHeight, 0, tempTimer / _moveMaxTime, 0.8f);
       // _arc = ArcMovement(_userPos.y, _arcHeight, _targetPos.y, tempTimer / _moveMaxTime, 0.8f);

        // x/y lerp movement
        float posX = Mathf.Lerp(_userPos.x, _targetPos.x, tempTimer / _moveMaxTime);
        float posY = Mathf.Lerp(userCenterPosY, _targetPos.y, tempTimer / _moveMaxTime);

        // scale modifier
        float scaleX = 0.7f - Mathf.Sin(tempTimer * 0.4f) * 0.19f;
        float scaleY = 0.7f + Mathf.Sin(tempTimer * 0.3f) * 0.18f;

        // enemy to player direction
        /*
        if (_targetPos.x > _userPos.x && _targetPos.y > _userPos.y)
        {
            if (posX >= _targetPos.x && posY >= _targetPos.y) _isWebbed = true;
        }
        // enemy to player direction
        else
        {
            if (posX <= _targetPos.x && posY <= _targetPos.y) _isWebbed = true;
        }
        */

        // make sure the moving sprite disappears when it reaches its target
        /*
        GD.Print("tempTimer = " + tempTimer);
        GD.Print("userCenterPosY = " + userCenterPosY);
        GD.Print("_arcHeight = " + _arcHeight + " || _arcMaxHeight = " + _arcMaxHeight);
        GD.Print("_arc = " + _arc);
        GD.Print("posX = " + posX + " posY = " + posY);
        GD.Print("tempTimer / _moveMaxTime =" + tempTimer / _moveMaxTime);
        */

        
        // otherwise keep moving toward target
        if (!_isWebbed)
        {
            Position = new Vector2(posX, posY - _arc);
            _ndSpriteA.Rotation = _spin;
            _ndSpriteA.Scale = new Vector2(scaleX, scaleY);
        }
        
    }

    public double CustomTween(double inputValue, double outputMin, double outputMax, double inputMax)
    {
        // EaseOutSine -> quarter-cycle of sine wave (different phase)
        
        return outputMax * Math.Sin(inputValue / inputMax * (Math.PI / 2)) + outputMin;
    }

    public float ArcMovement(float startVal, float peakVal, float floorVal, float positionVal, float biasVal)
    {
        if (positionVal <= 0.5)
        {
            return (Mathf.Lerp(startVal, peakVal, ArcBias(biasVal, positionVal * 2)));
        }
        else
        {
            float bias = 1 - biasVal;
            float position = 2 * positionVal - 1;

            return (Mathf.Lerp(peakVal, floorVal, ArcBias(bias, position)));
        }
    }

    public float ArcBias(float bias, float position)
    {
        return position / ((1 / bias - 2) * (1 - position) + 1);
    }

    // upon web expanse the move destroys itself
    public void OnTweenCompletion(object obj, NodePath key)
    {
        QueueFree();
    }

    public void OnBodyEntered(Node body)
    {
        if (body is ObjPlayer)
        {
            ObjPlayer obj = (ObjPlayer)body;
            _enemy.NdObjPlayer.Attacker = _enemy;
            obj.IsDamaged = true;

            _isWebbed = true;
            _enemy.UseSkill = false;
            GD.Print("WebShot =========" + body.Name);
        }
    }


}
