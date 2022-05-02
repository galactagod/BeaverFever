using Godot;
using System;

public class Accelerate : SkillMove
{
    private Texture _sprite = (Texture)GD.Load("res://src/Assets/Moves/SprStatChange.png");
    private Sprite _ndSprite;
    private int _hFrame = 11;

    public Accelerate(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {
    }

    public override void _Ready()
    {
        base._Ready();
        _ndSprite = CreateSprite(_sprite, _hFrame);
        _ndSprite.Frame = _hFrame - 1;
        _ndSprite.Scale = new Vector2(1.2f, 1.2f);
        _ndSprite.Modulate = Color.Color8(255, 255, 153);

        //DarkGoldenRod
        _timer.Start(0.1f);

        Vector2 playerSprSize = _player.NdSprPlayer.Frames.GetFrame("Buff", 0).GetSize();
        

        Position = new Vector2(Position.x, Position.y - (playerSprSize.y / 2));
    }

    public override void _PhysicsProcess(float delta)
    {

    }

    new public void OnTimeout()
    {
        if (_ndSprite.Frame == 0)
        {
            QueueFree();
            _player.UseSkill = false;
            return;
        }
        _ndSprite.Frame = Mathf.Clamp(_ndSprite.Frame - 1, 0, _hFrame - 1);
    }
}
