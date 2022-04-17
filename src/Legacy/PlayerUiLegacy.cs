using Godot;
using System;


public class PlayerUiLegacy : CanvasLayer
{
    /*
    private float _health;
    private float _maxHealth;
    private float _extraHealth;
    private const int MAXROWSIZE = 10;
    private const int HEARTOFFSETX = 68;
    private const int HEARTOFFSETY = 64;

    private PlayerStats _ndPlayerStats;
    private Sprite _ndSprHearts;
    private enum HeartSprites { Full, Half, Empty, EmptyHalf, Extra, ExtraHalf}

    public override void _Ready()
    {
        // acquire autoloads health stats
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndSprHearts = GetNode<Sprite>("SprHearts");

        // connect events
        _ndPlayerStats.HealthChange += OnHealthChange;
        _ndPlayerStats.MaxHealthChange += OnMaxHealthChange;
        _ndPlayerStats.ExtraHealthChange += OnExtraHealthChange;

        ObtainHealth();

        float heartCount = Mathf.Ceil((float)(_maxHealth) / 2);
        // set up hearts
        for (int i = 0; i < heartCount; i++)
        {
            Sprite sprHeart = new Sprite();
            sprHeart.Texture = _ndSprHearts.Texture;
            sprHeart.Hframes = _ndSprHearts.Hframes;
            
            _ndSprHearts.AddChild(sprHeart);
            //_ndSprHearts.RemoveChild(sprHeart);
        }

        // when removing a child node make sure  to delete it as well
    }


    public override void _Process(float delta)
    {
        
        ObtainHealth();

        foreach (Sprite heart in _ndSprHearts.GetChildren())
        {
            int index = heart.GetIndex();

            // maintain heart positon on X/Y axis
            int x = (index % MAXROWSIZE) * HEARTOFFSETX;
            int y = (index / MAXROWSIZE) * HEARTOFFSETY;
            heart.Position = new Vector2(x, y);

            float lastHeart = Mathf.Floor(_health/2 );
            //GD.Print("Index = " + index);
            // empty hearts
            if (index > lastHeart)
            {
                heart.Frame = (int)HeartSprites.Empty;
            }

            if (index < lastHeart)
            {
                heart.Frame = (int)HeartSprites.Full;
            }

            // half heart
            if (index == lastHeart)
            {
                if (_health % 2 == 0)
                {
                    heart.Frame = (int)HeartSprites.Empty;
                }
                else
                {
                    heart.Frame = (int)HeartSprites.Half;
                }
            }


        }

        
    }

    public void ObtainHealth()
    {
        _ndPlayerStats.ClampHealth();

        _health = _ndPlayerStats.Health;
        _maxHealth = _ndPlayerStats.MaxHealth;
        _extraHealth = _ndPlayerStats.ExtraHealth;
    }

    public void OnHealthChange(float health)
    {
        GD.Print("HEALTH SIGNAL");
    }

    public void OnMaxHealthChange(float health)
    {
        GD.Print("MAX HEALTH SIGNAL");
    }

    public void OnExtraHealthChange(float health)
    {
        GD.Print("EXTRA HEALTH SIGNAL");
    }
    */
}
