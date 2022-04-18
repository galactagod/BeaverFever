/*
 * LEGACY CODE
*/
using Godot;
using System;

public class HeartUiLegacy : Control
{
    /*
    private float _health;
    private float _maxHealth;
    private float _extraHealth;
    private Vector2 _heartSize = new Vector2(68 , 64);
    private float _scale = 0.5f;
    [Export] private int _rowCount = 1;
    private int _maxRowCount = 3;

    
    private PlayerStats _ndPlayerStats;
    private TextureRect _ndHeartsFull;
    private TextureRect _ndHeartsHalf;
    private TextureRect _ndHeartsEmpty;
    private TextureRect _ndHeartsHalfEmpty;
    private TextureRect _ndHeartsExtraFull;
    private TextureRect _ndHeartsExtraHalf;

    public int Health { get { return _health; } set { _health = value; } }
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    public override void _Ready()
    {
        _ndHeartsFull = GetNode<TextureRect>("HeartsFull");
        _ndHeartsHalf = GetNode<TextureRect>("HeartsHalf");
        _ndHeartsEmpty = GetNode<TextureRect>("HeartsEmpty");
        _ndHeartsHalfEmpty = GetNode<TextureRect>("HeartsHalfEmpty");
        _ndHeartsExtraFull = GetNode<TextureRect>("HeartsExtraFull");
        _ndHeartsExtraHalf = GetNode<TextureRect>("HeartsExtraHalf");

        // set scale
        _ndHeartsFull.RectScale = new Vector2(_scale, _scale);
        _ndHeartsHalf.RectScale = new Vector2(_scale, _scale);
        _ndHeartsEmpty.RectScale = new Vector2(_scale, _scale);
        _ndHeartsHalfEmpty.RectScale = new Vector2(_scale, _scale);
        _ndHeartsExtraFull.RectScale = new Vector2(_scale, _scale);
        _ndHeartsExtraHalf.RectScale = new Vector2(_scale, _scale);

        // acquire autoloads health stats
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");

        
    }

    public override void _Process(float delta)
    {
        _ndPlayerStats.ClampHealth();

        _health = _ndPlayerStats.Health;
        _maxHealth = _ndPlayerStats.MaxHealth;
        _extraHealth = _ndPlayerStats.ExtraHealth;

        

        HealthRows(_maxRowCount, _health, _maxHealth, _extraHealth);

        ModifyHealth(_health, _ndHeartsFull, _ndHeartsHalf, 0);
        ModifyHealth(_maxHealth, _ndHeartsEmpty, _ndHeartsHalfEmpty, 0);
        ModifyHealth(_extraHealth, _ndHeartsExtraFull, _ndHeartsExtraHalf, Mathf.Ceil((float)_maxHealth / 2) * _heartSize.x * _scale);

        _ndHeartsFull.Visible = (_health == 0 || _health == 1) ? false : true;
        _ndHeartsHalf.Visible = (_health % 2 == 0) ? false : true;

        _ndHeartsEmpty.Visible = (_maxHealth == 0 || _maxHealth == 1) ? false : true;
        _ndHeartsHalfEmpty.Visible = (_maxHealth % 2 == 0) ? false : true;

        _ndHeartsExtraFull.Visible = (_extraHealth == 0 || _extraHealth == 1) ? false : true;
        _ndHeartsExtraHalf.Visible = (_extraHealth % 2 == 0) ? false : true;

        
        //GD.Print("Health = " + _health);
        //GD.Print("MaxHealth = " + _maxHealth);
        //GD.Print("ExtraHealth = " + _extraHealth);
        
    }

    public void ModifyHealth(int health, TextureRect ndFullHeart, TextureRect ndHalfHeart, float startPosX)
    {
        // modify the full hearts
        if (health % 2 == 0)
        {
            ndFullHeart.RectPosition = new Vector2(startPosX, 0);
            ndFullHeart.RectSize = new Vector2((health / 2) * _heartSize.x, _heartSize.y);
            
        }
        // modify the Full/half hearts
        else
        {
            if (health == 1)
            {
                // one half heart
                ndHalfHeart.RectPosition = new Vector2(startPosX, 0);
                ndHalfHeart.RectSize = new Vector2((health / 2) * _heartSize.x, _heartSize.y);
            }
            else
            {
                // add full hearts and one half heart
                ndFullHeart.RectPosition = new Vector2(startPosX, 0);
                ndFullHeart.RectSize = new Vector2(((health - 1) / 2) * _heartSize.x, _heartSize.y);
                ndHalfHeart.RectPosition = new Vector2(startPosX + ndFullHeart.RectSize.x * _scale, 0);
                ndHalfHeart.RectSize = new Vector2(_heartSize.x, _heartSize.y);
            }
        }
    }

    // recursive rowcount
    public void HealthRows(int maxRowCount, int health, int maxHealth, int extraHealth)
    {
        // maintain health amount per hearts row
        // 40 max regular health pts for 2 rows
        // 20 max extra health points for 1 row

        int healthReduction = 20;

        

        
        switch(_rowCount)
        {
            case 1: break;


            case 2: break;

            case 3: break;

        }

        maxRowCount--;
        health -= healthReduction;
        maxHealth -= healthReduction;
        extraHealth -= healthReduction;

        if (maxRowCount != 0)
        {
            HealthRows(maxRowCount, health, maxHealth, extraHealth);
        }

        Sprite hearts = new Sprite();
       //prite.Texture = Hide();

        return;

    }

    */
}

