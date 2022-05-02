using Godot;
using System;

public class Crunch : SkillMove
{
    private Texture _sprite = (Texture)GD.Load("res://src/Assets/Moves/SprCrunch.png");
    private Sprite _ndSprite;
    private Area2D _ndArea;
    private Tween _ndTween;
    private int _hFrame = 5;

    public Crunch(ObjPlayer player, EnemyMovementAct enemy, string userType) : base (player, enemy, userType)
    {
    }
    

    public override void _Ready()
    {
        base._Ready();
        _ndSprite = CreateSprite(_sprite, _hFrame);
        Vector2 skillSprSize = _ndSprite.GetRect().Size;
        _ndArea = CreateArea(skillSprSize/2);
        _ndTween = CreateTween();

        _type = "physical";
        _energy = 10;

        // set position based on user sprite height
        string anim = _player.NdSprPlayer.Animation;
        int frame = _player.NdSprPlayer.Frame;

        Vector2 playerSprSize = _player.NdSprPlayer.Frames.GetFrame(anim, frame).GetSize();
        

        Position = new Vector2(Position.x + (_player.Direction.x * playerSprSize.x/2), Position.y - (playerSprSize.y/2 + skillSprSize.y/2));
        _ndSprite.FlipH = _player.NdSprPlayer.FlipH;

        switch (_tier)
        {
            case 1: _power = 45; break;
            case 2: _power = 50; break;
            case 3: _power = 60; break;
        }

        _timer.Start(0.1f);

        switch(_ndSprite.FlipH)
        {
            case true:
                _ndTween.InterpolateProperty(this, "position:x", Position.x, Position.x - 40, 0.3f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
            case false:
                _ndTween.InterpolateProperty(this, "position:x", Position.x, Position.x + 40, 0.3f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
        }

        _ndTween.Start();
    }

    public override void _PhysicsProcess(float delta)
    {
        
    }

    new public void OnTimeout()
    {
        if (_ndSprite.Frame == _hFrame - 1)
        {
            QueueFree();
            _player.UseSkill = false;
            return;
        }

        _ndSprite.Frame = Mathf.Clamp(_ndSprite.Frame + 1, 0, _hFrame - 1);
    }

}
