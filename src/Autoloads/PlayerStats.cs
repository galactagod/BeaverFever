using Godot;
using System;

public class PlayerStats : Node
{

    [Export] private int _health = 18;
    [Export] private int _maxHealth = 20;
    [Export] private int _extraHealth = 2;
    private int _maxHealthCap = 100;
    private int _maxExtraHealthCap = 100;
    private Vector2 _playerpos;
    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler HealthChange, MaxHealthChange, ExtraHealthChange;
    //public event HealthEventHandler<PlayerStats> HealthChange;
    

    public int Health { get { return _health; } set { _health = value; } }
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public int ExtraHealth { get { return _extraHealth; } set { _extraHealth = value; } }
    public Vector2 PlayerPos { get { return _playerpos; } set { _playerpos = value; } }

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        
    }

    public void ClampHealth()
    {
        // clamp stats
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _maxHealth = Mathf.Clamp(_maxHealth, 0, _maxHealthCap);
        _extraHealth = Mathf.Clamp(_extraHealth, 0, _maxExtraHealthCap);
    }

    
    public void ChangeHealth(int health)
    {
        _health = health;
        ClampHealth();
        HealthChange?.Invoke(_health);
        GD.Print("health = " + _health);
    }

    public void ChangeMaxHealth(int health)
    {
        _maxHealth = health;
        ClampHealth();
        MaxHealthChange?.Invoke(_maxHealth);
        GD.Print("Max health = " + _maxHealth);
    }

    public void ChangeExtraHealth(int health)
    {
        _extraHealth = health;
        ClampHealth();
        ExtraHealthChange?.Invoke(_extraHealth);
        GD.Print("Extra health = " + _extraHealth);
    }
    

    
}
