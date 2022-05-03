using Godot;
using System;

public class Glide : SkillMove
{
    private Area2D _ndArea;
    private int _glideTimer = 0;
    private bool _glideFinished = false;

    public Glide(ObjPlayer player, EnemyMovementAct enemy, string userType) : base(player, enemy, userType)
    {
    }

    public override void _Ready()
    {
        base._Ready();
        Vector2 skillSprSize = _enemy.NdSprEnemy.Frames.GetFrame("Fly", 0).GetSize();
        _ndArea = CreateArea(skillSprSize / 2);

        _ndArea.Connect("body_entered", this, nameof(OnBodyEntered));

        _userPos = _enemy.Position;
        _targetPos = _player.Position;
    }

    public override void _PhysicsProcess(float delta)
    {
        int moveMaxTime = 50;

        if (_glideTimer <= moveMaxTime)
        {
            // lerp and glide towards player
            float posX = Mathf.Lerp(_userPos.x, _targetPos.x, _glideTimer / moveMaxTime);
            float posY = Mathf.Lerp(_userPos.y, _targetPos.y, _glideTimer / moveMaxTime);

            Position = new Vector2(posX, posY);
            _enemy.Position = new Vector2(posX, posY);
        }
        /*
        else if(_glideTimer <= moveMaxTime + moveMaxTime)
        {
            // after glide go back to return state
            if (!_glideFinished)
            {
                _glideFinished = true;
                _ndArea.QueueFree();

                // lerp and glide towards player
                float posX = Mathf.Lerp(_userPos.x, _targetPos.x, _glideTimer / moveMaxTime);
                float posY = Mathf.Lerp(_userPos.y, _targetPos.y, _glideTimer / moveMaxTime);

                Position = new Vector2(posX, posY);

            }

        }
        */
        else
        {
            QueueFree();
        }


        _glideTimer++;
    }

    public void OnBodyEntered(Node body)
    {
        if (body is ObjPlayer)
        {
            ObjPlayer obj = (ObjPlayer)body;
            _enemy.NdObjPlayer.Attacker = _enemy;
            obj.IsDamaged = true;
            _enemy.UseSkill = false;
            GD.Print("Glide =========" + body.Name);
        }
    }
}
