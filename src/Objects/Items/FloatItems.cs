using Godot;
using System;

public class FloatItems : Area2D
{
    protected int _timer;
    protected int _moneyValue;
    protected bool _invert = false;
    protected bool _stopFloat = false;
    protected Tween _ndTween;
    protected float _origPosY;

    // node references
    protected ObjPlayer _ndObjPlayer;
    protected PlayerStats _ndPlayerStats;
    protected LevelControl _ndLevelControl;

    // sound paths
    private AudioStreamSample _sndCoinCollect = (AudioStreamSample)GD.Load("res://src/Assets/Sounds/Items/SndCoinCollect.wav");


    public override void _Ready()
    {
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndLevelControl = GetNode<LevelControl>("/root/LevelControl");

        _ndTween = new Tween();
        this.AddChild(_ndTween);
        _ndTween.Connect("tween_all_completed", this, nameof(OnAllTweenCompletion));
        //_ndTween.InterpolateProperty(this, "position:y", 0, 3, 1, Tween.TransitionType.Sine, Tween.EaseType.In);
        //_ndTween.Start();
        

        this.Connect("body_entered", this, nameof(OnBodyEntered));
        _origPosY = Position.y;

    }

    public override void _Process(float delta)
    {
        if (!_stopFloat)
        {
            ItemFloat();
        }

    }

    public void ItemFloat()
    {
        float freq = 0.08f;
        float amp = -3;

        Position = new Vector2(Position.x, _origPosY + (float)(Math.Sin(_timer * freq) * amp));
        _timer++;
    }

    public void OnBodyEntered(Node body)
    {
        if (body is ObjPlayer && !_stopFloat)
        {
            _stopFloat = true;
            _ndTween.InterpolateProperty(this, "position:y", Position.y, Position.y - 30, 1, Tween.TransitionType.Sine, Tween.EaseType.Out);
            _ndTween.InterpolateProperty(this, "modulate:a", 1, 0, 1, Tween.TransitionType.Sine, Tween.EaseType.Out);
            _ndTween.Start();
            _ndLevelControl.SfxPlayerManager(-1, _sndCoinCollect, 0, 1);
            _ndPlayerStats.ChangeMoney(_moneyValue);
        }
        
    }

    public void OnAllTweenCompletion()
    {
        QueueFree();
    }

        // looping tween example
        /*
        public void OnTweenCompletion(Godot.Object obj, NodePath path)
        {
            switch(_invert)
            {
                case true:
                    _ndTween.InterpolateProperty(this, "position:y", 0, -5, 0.5f, Tween.TransitionType.Sine, Tween.EaseType.In);
                    _ndTween.Start();
                    _invert = false;
                    break;
                case false:
                    _ndTween.InterpolateProperty(this, "position:y", -5, 0, 0.5f, Tween.TransitionType.Sine, Tween.EaseType.In);
                    _ndTween.Start();
                    _invert = true;
                    break;
            }
        }
        */

    }
