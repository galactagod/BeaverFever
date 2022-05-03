using Godot;
using System;

public class PlayerStats : Node
{

    [Export] private float _health = 1;
    [Export] private float _maxHealth = 20;
    [Export] private float _energy = 8;
    [Export] private float _maxEnergy = 10;
    [Export] private float _exp = 250;
    [Export] private float _muny = 0;
    [Export] private string[] _skillNames = new string[3];
    [Export] private int[] _skillTiers = new int[3];
    [Export] private float[] _skillCoolDowns = new float[3];
    [Export] private float[] _skillMaxCoolDowns = new float[3];
    [Export] private float[] _skillEnergyUse = new float[3];
    private bool _energyPause = false;
    private bool _healthPause = false;
    private float _moneyMultiplier = 1;

    public float _maxMuny = 99999999;

    private float _maxHealthCap = 1000;
    private float _maxEnergyCap = 100;
    private Vector2 _playerpos;

    public delegate void StatEventHandler(float value);
    public event StatEventHandler HealthChange, MaxHealthChange, EnergyChange, MaxEnergyChange, MoneyChange, 
    ExpChange, ReplenishEnergyChange, ReplenishHealthChange;
    //public event HealthEventHandler<PlayerStats> HealthChange;

    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float Energy { get { return _energy; } set { _energy = value; } }
    public float MaxEnergy { get { return _maxEnergy; } set { _maxEnergy = value; } }
    public float Exp { get { return _exp; } set { _exp = value; } }
    public float Muny { get { return _muny; } set { _muny = value; } }
    public float MaxMuny { get { return _maxMuny; } set { _maxMuny = value; } }
    public Vector2 PlayerPos { get { return _playerpos; } set { _playerpos = value; } }
    public string[] SkillNames { get { return _skillNames; } set { _skillNames = value; } }
    public int[] SkillTiers { get { return _skillTiers; } set { _skillTiers = value; } }
    public float[] SkillCoolDowns { get { return _skillCoolDowns; } set { _skillCoolDowns = value; } }
    public float[] SkillMaxCoolDowns { get { return _skillMaxCoolDowns; } set { _skillMaxCoolDowns = value; } }
    public float[] SkillEnergyUse { get { return _skillEnergyUse; } set { _skillEnergyUse = value; } }
    public bool EnergyPause { get { return _energyPause; } set { _energyPause = value; } }
    public bool HealthPause { get { return _healthPause; } set { _healthPause = value; } }
    public float MoneyMultiplier { get { return _moneyMultiplier; } set { _moneyMultiplier = value; } }

    private PlayerData _ndplayerData;
    private LevelControl levelControl;

    public override void _Ready()
    {
        _ndplayerData = GetNode<PlayerData>("/root/PlayerData");

        levelControl = GetNode<LevelControl>("/root/LevelControl");
        Muny = _ndplayerData.Muny;
        _health = _ndplayerData.currentHealth;
    }

    public override void _Process(float delta)
    {
        
        // update skills
        for (int i = 0; i < 3; i++)
        {
            string name = "";
            if (_ndplayerData.equipment.TryGetValue("Skill" + (i + 1), out var skill))
            {
                /*
                switch (skill.name)
                {
                    case "Rip Mod": name = "Slice"; break;

                    case "Attack Mod": name = "BubbleBurst"; break;

                    case "Leaf Mod": name = "Regeneration"; break;

                    case "Book Mod": name = "Grace"; break;

                    case "Sharp Mod": name = "Crunch"; break;

                    case "Boots Mod": name = "Accelerate"; break;

                    case "Body Mod": name = "Aegis"; break;
                }
                */

                _skillNames[i] = skill.name;
                //_skillNames[i] = name;
                _skillTiers[i] = skill.level;
                SetSkillCoolDownEnergy(_skillNames[i], i);
            }
            else
            {
                _skillNames[i] = name;
                SetSkillCoolDownEnergy("", i);
            }

        }
        

        //_skills[0];

        if (_health == 0)
        {
            //Add a death scene here
            //levelControl.changeLevel();
            _health = _maxHealth;
            ChangeHealth(0);
            levelControl.playerDied();
        }
    }


    public void SetSkillCoolDownEnergy(string name, int index)
    {
        float cooldown;
        int energy = 0;

        cooldown = 100;
        if (name != "")
        {
            switch (name)
            {
                case "Slice":
                    cooldown = 40;
                    energy = 2;
                    break;
                case "Crunch": case "BubbleBurst":
                    cooldown = 70;
                    energy = 4;
                    break;
                case "Aegis": case "Accelerate":
                    cooldown = 150;
                    energy = 5;
                    break;
                case "Regeneration": case "Grace":
                    cooldown = 0.1f;
                    energy = 0;
                    break;
            }

            _skillMaxCoolDowns[index] = cooldown;
            _skillEnergyUse[index] = energy;
        }
        else
        {
            _skillMaxCoolDowns[index] = cooldown;
            _skillCoolDowns[index] = cooldown;
        }
    }

    public void ChangeHealth(float health)
    {
        _health += health;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        HealthChange?.Invoke(_health);
        GD.Print("health = " + _health);
    }

    public void ChangeReplenishHealth(float health)
    {
        _health += health;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        ReplenishHealthChange?.Invoke(_health);
        GD.Print("health = " + _health);
    }

    public void ChangeMaxHealth(float health)
    {
        _maxHealth += health;
        _maxHealth = Mathf.Clamp(_maxHealth, 0, _maxHealthCap);
        MaxHealthChange?.Invoke(_maxHealth);
        GD.Print("Max health = " + _maxHealth);
    }

    public void ChangeEnergy(float energy)
    {
        _energy += energy;
        _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
        EnergyChange?.Invoke(_energy);
        GD.Print("Energy = " + _energy);
    }

    public void ChangeReplenishEnergy(float energy)
    {
        _energy += energy;
        _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
        ReplenishEnergyChange?.Invoke(_energy);
        //GD.Print("Energy = " + _energy);
    }

    public void ChangeMaxEnergy(float energy)
    {
        _maxEnergy += energy;
        _maxEnergy = Mathf.Clamp(_maxEnergy, 0, _maxEnergyCap);
        MaxEnergyChange?.Invoke(_maxEnergy);
        GD.Print("Max Energy = " + _maxEnergy);
    }

    public void ChangeCooldown(float value)
    {
        for (int i = 0; i < 3; i++)
        {
            if(_skillNames[i] != "")
            {
                _skillCoolDowns[i] = Mathf.Clamp(_skillCoolDowns[i] - value, 0, _skillMaxCoolDowns[i]);
            }

        }
    }

    public void ChangeMoney(float money)
    {
        _muny += (money * _moneyMultiplier);
        _muny = Mathf.Clamp(_muny, 0, _maxMuny);
        MoneyChange?.Invoke(money * _moneyMultiplier);
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
            ChangeHealth(25);
        }

        if (name == "Large Health Potion")
        {
            ChangeHealth(55);
        }


    }


}
