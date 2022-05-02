using Godot;
using System;

public class SkillMove : Node2D
{
    protected float _power;
    protected string _type;

    protected Vector2 _userPos;
    protected Vector2 _targetPos;
    protected string _userType;

    protected ObjPlayer _player;
    protected EnemyMovementAct _enemy;
    protected Timer _timer;

    // node reference
    protected PlayerStats _ndPlayerStats;


    public SkillMove(ObjPlayer player, EnemyMovementAct enemy, string userType)
    {
        _player = player;
        _enemy = enemy;
        _userType = userType;
    }


    public override void _Ready()
    {
        // acquire node references
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");

        _timer = new Timer();
        AddChild(_timer);
        _timer.Connect("timeout", this, nameof(OnTimeout));

    }



    public override void _PhysicsProcess(float delta)
    {

    }

    public Sprite CreateSprite(Texture sprite, int hFrames)
    {
        Sprite skillSprite = new Sprite();
        skillSprite.Texture = sprite;
        skillSprite.Hframes = hFrames;
        AddChild(skillSprite);

        return skillSprite;
    }

    public Area2D CreateArea(Vector2 sprSize)
    {
        Area2D skillArea = new Area2D();
        CollisionShape2D skillAreaCol = new CollisionShape2D();
        RectangleShape2D shape = new RectangleShape2D();

        shape.Extents = sprSize;
        skillAreaCol.Shape = shape;

        AddChild(skillArea);
        skillArea.AddChild(skillAreaCol);


        return skillArea;
    }

    public Tween CreateTween()
    {
        Tween tween = new Tween();
        AddChild(tween);

        return tween;
    }

    public void OnTimeout()
    {
        GD.Print("TIMEOUT  PARENT");
    }



}
