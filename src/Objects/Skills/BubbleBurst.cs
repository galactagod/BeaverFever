using Godot;
using System;

public class BubbleBurst : SkillMove
{
    private Texture _sprite = (Texture)GD.Load("res://src/Assets/Moves/SprBubbleBurst.png");
    private Area2D _ndArea;
    private Tween _ndTween;
    private Sprite[] _ndSprite = new Sprite[5];
    
    private int _sprCreateCounter = 0;
    private int _sprPopCounter = 0;
    private int _maxSprCounter = 5;
    private int _hFrame = 5;
    private int _sprTimer = 0;
    private float _sprOrigPosY;

    public BubbleBurst(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {
    }

    public override void _Ready()
    {
        base._Ready();
        // modify tiers

        for (int i = 0; i < 3; i++)
        {
            if (_ndPlayerStats.SkillNames[i] == "BubbleBurst")
            {
                switch (_ndPlayerStats.SkillTiers[i])
                {
                    case 1: _power = 3; break;
                    case 2: _power = 5; break;
                    case 3: _power = 8; break;
                }
                break;
            }
        }

        _ndTween = CreateTween();
        MakeSprites();
        _sprOrigPosY = _ndSprite[0].Position.y;
        _timer.Start(0.1f);

        string anim = _player.NdSprPlayer.Animation;
        int frame = _player.NdSprPlayer.Frame;
        Vector2 skillSprSize = _ndSprite[0].GetRect().Size;
        Vector2 playerSprSize = _player.NdSprPlayer.Frames.GetFrame(anim, frame).GetSize();
        _ndArea = CreateArea(skillSprSize/2);
        _ndArea.Position = new Vector2(_ndArea.Position.x, _ndArea.Position.y - (playerSprSize.y/2 + skillSprSize.y/2));

        _ndArea.Connect("body_entered", this, nameof(OnBodyEntered));

        switch (_player.NdSprPlayer.FlipH)
        {
            case true:
                _ndTween.InterpolateProperty(_ndArea, "position:x", Position.x, Position.x - 70, 1f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
            case false:
                _ndTween.InterpolateProperty(_ndArea, "position:x", Position.x, Position.x + 70, 1f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
        }

        _ndTween.Start();
    }

    public override void _PhysicsProcess(float delta)
    {
        float[] freq = { 0.08f, 0.03f, 0.05f, 0.07f, 0.1f };
        float[] amp = { -3, 2, -4, 2.5f, -3 };

        if (_sprTimer % 2 == 0 && _sprTimer != 0)
        {
            if (_sprCreateCounter < _maxSprCounter)
            {
                MakeSprites();
            }
        }

        // start slowly popping each bubble
        if (_sprTimer % 5 == 0 && _sprTimer != 0)
        {
            _sprPopCounter = Mathf.Clamp(_sprPopCounter + 1, 0, _maxSprCounter);
        }

        // make y direction move back and forth for individual bubble
        for (int i = 0; i < _sprCreateCounter; i++)
        {
            if (_ndSprite[i] != null)
            {
                _ndSprite[i].Position = new Vector2(Position.x, _sprOrigPosY + (float)(Math.Sin(_sprTimer * (freq[i]) * 5) * (amp[i] * 5)));
                //GD.Print("SprTimer = " + _sprTimer);
                //GD.Print("Sprite = " + i + " Position Y = " + Position.y);
            }
        }

        _sprTimer++;
    }

    public void StartTween(Sprite spr, Vector2 playerSprSize)
    {
        float sprPosX = 0;
        switch (_player.NdSprPlayer.FlipH)
        {
            case true:
                sprPosX = spr.Position.x - playerSprSize.x/2;
                _ndTween.InterpolateProperty(spr, "position:x", sprPosX, sprPosX - 70, 1f, Tween.TransitionType.Sine, Tween.EaseType.Out);
                break;
            case false:
                sprPosX = spr.Position.x + playerSprSize.x/2;
                _ndTween.InterpolateProperty(spr, "position:x", sprPosX, sprPosX + 70, 1f, Tween.TransitionType.Sine, Tween.EaseType.Out);
                break;
        }

        _ndTween.Start();
    }

    public void MakeSprites()
    {
        string anim = _player.NdSprPlayer.Animation;
        int frame = _player.NdSprPlayer.Frame;

        _ndSprite[_sprCreateCounter] = CreateSprite(_sprite, _hFrame);
        _ndSprite[_sprCreateCounter].Scale = new Vector2(0.5f, 0.5f);

        Vector2 playerSprSize = _player.NdSprPlayer.Frames.GetFrame(anim, frame).GetSize();
        Vector2 skillSprSize = _ndSprite[_sprCreateCounter].GetRect().Size;
        float sprPosY = _ndSprite[_sprCreateCounter].Position.y - (playerSprSize.y/2 + skillSprSize.y/2);

        _ndSprite[_sprCreateCounter].Position = new Vector2(_ndSprite[_sprCreateCounter].Position.x, sprPosY);

        StartTween(_ndSprite[_sprCreateCounter], playerSprSize);

        _sprCreateCounter = Mathf.Clamp(_sprCreateCounter + 1, 0, _maxSprCounter);
    }

    new public void OnTimeout()
    {
        // start popping the bubbles as the pop counter increases from each tween completion
        for (int i = 0; i < _sprPopCounter; i++)
        {
            if (_ndSprite[i] != null)
            {
                _ndSprite[i].Frame = Mathf.Clamp(_ndSprite[i].Frame + 1, 0, _hFrame - 1);

                // delete sprite node after they have popped
                if (_ndSprite[i].Frame == _hFrame - 1)
                {
                    _ndSprite[i].QueueFree();
                    _ndSprite[i] = null;

                    // if last bubble is popped move is done
                    if (i == _sprPopCounter - 1)
                    {
                        QueueFree();
                        _player.UseSkill = false;
                        return;
                    }
                }
            }
        }
    }

    public void OnBodyEntered(Node body)
    {
        if (body is EnemyMovementAct)
        {
            EnemyMovementAct obj = (EnemyMovementAct)body;
            obj.IsDamaged = true;
            _player.CurDmg = _power + _player.CurSpAttack;
            _player.IsPhysical = false;
            GD.Print("EnemyMovementAct =========" + body.Name);
        }
    }
}
