using Godot;
using System;

public class SkillMove : Node2D
{
    protected float _power;
    protected string _type;
    protected float _energy;
    protected int _tier;

    protected Vector2 _UserPos;
    protected Vector2 _targetPos;



    public SkillMove(Vector2 UserPos, Vector2 targetPos, int tier)
    {
        _UserPos = UserPos;
        _targetPos = targetPos;
        _tier = tier;
    }

    public override void _Ready()
    {

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


}
