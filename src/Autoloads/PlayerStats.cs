using Godot;
using System;

public class PlayerStats : Node
{

    [Export] private float _health = 1;
    [Export] private float _maxHealth = 20;
    [Export] private float _extraHealth = 2;
    [Export] private float _energy = 3;
    [Export] private float _maxEnergy = 10;
    [Export] private float _exp = 250;
    [Export] private float _muny = 0;

    public float _maxMuny = 99999999;

    private float _maxHealthCap = 100;
    private float _maxExtraHealthCap = 100;
    private Vector2 _playerpos;

    public delegate void StatEventHandler(float value);
    public event StatEventHandler HealthChange, MaxHealthChange, ExtraHealthChange, MoneyChange, EnergyChange, ExpChange;
    //public event HealthEventHandler<PlayerStats> HealthChange;

    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float ExtraHealth { get { return _extraHealth; } set { _extraHealth = value; } }
    public float Energy { get { return _energy; } set { _energy = value; } }
    public float MaxEnergy { get { return _maxEnergy; } set { _maxEnergy = value; } }
    public float Exp { get { return _exp; } set { _exp = value; } }
    public float Muny { get { return _muny; } set { _muny = value; } }
    public float MaxMuny { get { return _maxMuny; } set { _maxMuny = value; } }
    public Vector2 PlayerPos { get { return _playerpos; } set { _playerpos = value; } }

    private PlayerData playerData;
    private LevelControl levelControl;

    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");

        levelControl = GetNode<LevelControl>("/root/LevelControl");
        Muny = playerData.Muny;
    }

    public override void _Process(float delta)
    {
        if(_health == 0)
        {
            //Add a death scene here
            //levelControl.changeLevel();
            _health = _maxHealth;
            levelControl.playerDied();
        }
    }

    public void ClampHealth()
    {
        // clamp stats
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _maxHealth = Mathf.Clamp(_maxHealth, 0, _maxHealthCap);
        _extraHealth = Mathf.Clamp(_extraHealth, 0, _maxExtraHealthCap);
    }

    public void ChangeHealth(float health)
    {
        _health += health;
        ClampHealth();
        HealthChange?.Invoke(_health);
        GD.Print("health = " + _health);
    }

    public void ChangeMaxHealth(float health)
    {
        _maxHealth += health;
        ClampHealth();
        MaxHealthChange?.Invoke(_maxHealth);
        GD.Print("Max health = " + _maxHealth);
    }

    public void ChangeExtraHealth(float health)
    {
        _extraHealth += health;
        ClampHealth();
        ExtraHealthChange?.Invoke(_extraHealth);
        GD.Print("Extra health = " + _extraHealth);
    }

    public void ChangeEnergy(float energy)
    {
        _energy += energy;
        _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
        EnergyChange?.Invoke(energy);
        GD.Print("Energy = " + _energy);
    }

    public void ReplenishEnergy(float energy)
    {
        _energy += energy;
        _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
    }

    public void ChangeMoney(float money)
    {
        _muny += money;
        _muny = Mathf.Clamp(_muny, 0, _maxMuny);
        MoneyChange?.Invoke(money);
        GD.Print("Money = " + _muny);
    }

    public void ChangeExp(float exp)
    {
        _exp += exp;
        _exp = (_exp < 0) ? 0 : _exp;
        ExpChange?.Invoke(exp);
        GD.Print("Exp = " + _exp);
    }

    public void UseConsumable(string name)
    {
        if (name == "Small Health Potion")
        {
            ChangeHealth(10);
        }

        if (name == "Medium Health Potion")
        {
            ChangeHealth(30);
        }

        if (name == "Large Health Potion")
        {
            ChangeHealth(60);
        }


    }


}
