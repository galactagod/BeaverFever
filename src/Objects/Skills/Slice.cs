using Godot;
using System;

public class Slice : SkillMove
{
    private Area2D _ndArea;
    private Tween _ndTween;
    private int _counter = 0;
    private int _maxCounter = 3;

    public Slice(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {

    }

    public override void _Ready()
    {
        base._Ready();

        string anim = _player.NdSprPlayer.Animation;
        int frame = _player.NdSprPlayer.Frame;
        Vector2 playerSprSize = _player.NdSprPlayer.Frames.GetFrame(anim, frame).GetSize();
        _ndArea = CreateArea(new Vector2(playerSprSize.x/3, playerSprSize.y/2));
        _ndTween = CreateTween();

        Position = new Vector2(Position.x + (_player.Direction.x * playerSprSize.x / 2), Position.y - (playerSprSize.y / 1.2f));

        _timer.Start(0.1f);

        switch (_player.NdSprPlayer.FlipH)
        {
            case true:
                _ndTween.InterpolateProperty(this, "position:x", Position.x, Position.x - 20, 0.3f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
            case false:
                _ndTween.InterpolateProperty(this, "position:x", Position.x, Position.x + 20, 0.3f, Tween.TransitionType.Quart, Tween.EaseType.Out);
                break;
        }

        _ndTween.Start();
    }

    public override void _PhysicsProcess(float delta)
    {

    }
    new public void OnTimeout()
    {
        _counter++;

        if (_counter > _maxCounter)
        {
            QueueFree();
            _player.UseSkill = false;
            return;
        }
    }
}
