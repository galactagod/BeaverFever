using Godot;
using System;

public class Accelerate : SkillMove
{
    private Texture _sprite = (Texture)GD.Load("res://src/Assets/Moves/SprStatChange.png");
    private Sprite _ndSprite;
    private int _hFrame = 11;
    private Vector2 _fastSpeed;

    public Accelerate(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {
    }

    public override void _Ready()
    {
        base._Ready();
        for (int i = 0; i < 3; i++)
        {
            if (_ndPlayerStats.SkillNames[i] == "Accelerate")
            {
                switch (_ndPlayerStats.SkillTiers[i])
                {
                    case 1: _fastSpeed = new Vector2(350, 650); break;
                    case 2: _fastSpeed = new Vector2(400, 700); break;
                    case 3: _fastSpeed = new Vector2(450, 750); break;
                }
                break;
            }
        }
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
            _player.UseSkill = false;
            // set player speed and cooldown
            _player.Speed = _fastSpeed;
            _player.SpeedCoolDown = _player.SpeedMaxCoolDown;
            QueueFree();

            return;
        }
        _ndSprite.Frame = Mathf.Clamp(_ndSprite.Frame - 1, 0, _hFrame - 1);
    }
}
