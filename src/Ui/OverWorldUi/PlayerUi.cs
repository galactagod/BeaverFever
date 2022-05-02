using Godot;
using System;

public class PlayerUi : Node
{
    private float _health;
    private float _maxHealth;
    private float _exp;
    private float _expChange;
    private bool _isExp = false;
    private float _muny;
    private float _munyChange;
    private bool _isMuny = false;
    private float _maxMuny;
    private float _energy;
    private float _maxEnergy;

    // node reference
    private PlayerStats _ndPlayerStats;
    public TextureProgress _ndHpBar;
    private TextureProgress _ndEnergyBar;
    private TextureRect _ndSkillA;
    private TextureRect _ndSkillB;
    private TextureRect _ndSkillC;
    private Label _lblSkillA;
    private Label _lblSkillB;
    private Label _lblSkillC;
    private TextureProgress _coolDownBarA;
    private TextureProgress _coolDownBarB;
    private TextureProgress _coolDownBarC;
    private TextureRect _ndMunyBar;
    private TextureRect _ndExpBar;
    private TextureRect _ndMunyTexture;
    private TextureRect _ndExpTexture;
    private Label _ndMunyCount;
    private Label _ndExpCount;
    private Tween _ndTween;


    public override void _Ready()
    {
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");

        // acquire nodes
        _ndPlayerStats = GetNode<PlayerStats>("/root/PlayerStats");
        _ndHpBar = GetNode<TextureProgress>("CanvasA/HpBar");
        _ndEnergyBar = GetNode<TextureProgress>("CanvasA/EnergyBar");
        _ndSkillA = GetNode<TextureRect>("CanvasA/SkillA");
        _ndSkillB = GetNode<TextureRect>("CanvasA/SkillB");
        _ndSkillC = GetNode<TextureRect>("CanvasA/SkillC");
        _lblSkillA = GetNode<Label>("CanvasA/LblSkillA");
        _lblSkillB = GetNode<Label>("CanvasA/LblSkillB");
        _lblSkillC = GetNode<Label>("CanvasA/LblSkillC");
        _coolDownBarA = GetNode<TextureProgress>("CanvasA/CoolDownBarA");
        _coolDownBarB = GetNode<TextureProgress>("CanvasA/CoolDownBarB");
        _coolDownBarC = GetNode<TextureProgress>("CanvasA/CoolDownBarC");
        _ndMunyCount = GetNode<Label>("CanvasA/MunyCount");
        _ndExpCount = GetNode<Label>("CanvasA/ExpCount");
        _ndMunyBar = GetNode<TextureRect>("CanvasA/MunyBar");
        _ndExpBar = GetNode<TextureRect>("CanvasA/ExpBar");
        _ndMunyTexture = GetNode<TextureRect>("CanvasA/MunyTexture");
        _ndExpTexture = GetNode<TextureRect>("CanvasA/ExpTexture");
        _ndTween = new Tween();
        AddChild(_ndTween);

        // connect events
        _ndPlayerStats.HealthChange += OnHealthChange;
        _ndPlayerStats.EnergyChange += OnChangeEnergy;
        _ndPlayerStats.ExpChange += OnChangeExp;
        _ndPlayerStats.MoneyChange += OnChangeMoney;
        _ndPlayerStats.MaxHealthChange += OnChangeMaxHealth;
        _ndPlayerStats.MaxEnergyChange += OnChangeMaxEnergy;
        _ndPlayerStats.ReplenishHealthChange += OnChangeReplenishHealth;
        _ndPlayerStats.ReplenishEnergyChange += OnChangeReplenishEnergy;

        _ndTween.Connect("tween_all_completed", this, nameof(OnAllTweenCompletion));
        _ndTween.Connect("tween_step", this, nameof(OnTweenStep));

        // obtain node values
        _health = _ndPlayerStats.Health;
        _maxHealth = _ndPlayerStats.MaxHealth;
        _muny = _ndPlayerStats.Muny;
        _munyChange = _ndPlayerStats.Muny;
        _maxMuny = _ndPlayerStats.MaxMuny;
        _exp = _ndPlayerStats.Exp;
        _expChange = _ndPlayerStats.Exp;
        _energy = _ndPlayerStats.Energy;
        _maxEnergy = _ndPlayerStats.MaxEnergy;

        _ndHpBar.MaxValue = _ndPlayerStats.MaxHealth;
        _ndHpBar.Value = _ndPlayerStats.Health;

        _ndEnergyBar.MaxValue = _ndPlayerStats.MaxEnergy;
        _ndEnergyBar.Value = _ndPlayerStats.Energy;

        _ndMunyCount.Text = Calculations.IntStringCommas((int)_ndPlayerStats.Muny);
        _ndExpCount.Text = _ndPlayerStats.Exp.ToString();
    }

    public override void _Process(float delta)
    {
        /*
        _ndHpBar.MaxValue = _ndPlayerStats.MaxHealth;
        _ndEnergyBar.MaxValue = _ndPlayerStats.MaxEnergy;
        _ndHpBar.Value = _ndPlayerStats.Health;
        _ndEnergyBar.Value = _ndPlayerStats.Energy;
        */

        _lblSkillA.Text = _ndPlayerStats.SkillNames[0];
        _lblSkillB.Text = _ndPlayerStats.SkillNames[1];
        _lblSkillC.Text = _ndPlayerStats.SkillNames[2];

        _coolDownBarA.MaxValue = _ndPlayerStats.SkillMaxCoolDowns[0];
        _coolDownBarB.MaxValue = _ndPlayerStats.SkillMaxCoolDowns[1];
        _coolDownBarC.MaxValue = _ndPlayerStats.SkillMaxCoolDowns[2];

        _coolDownBarA.Value = _ndPlayerStats.SkillCoolDowns[0];
        _coolDownBarB.Value = _ndPlayerStats.SkillCoolDowns[1];
        _coolDownBarC.Value = _ndPlayerStats.SkillCoolDowns[2];

        // dim the skills if able to be used if enough energy
        _lblSkillA.Modulate = (_ndPlayerStats.SkillEnergyUse[0] > _ndPlayerStats.Energy) ? Color.Color8(100, 100, 100) : Color.Color8(255, 255, 255);
        _lblSkillB.Modulate = (_ndPlayerStats.SkillEnergyUse[1] > _ndPlayerStats.Energy) ? Color.Color8(100, 100, 100) : Color.Color8(255, 255, 255);
        _lblSkillC.Modulate = (_ndPlayerStats.SkillEnergyUse[2] > _ndPlayerStats.Energy) ? Color.Color8(100, 100, 100) : Color.Color8(255, 255, 255);
    }

    public void OnHealthChange(float value)
    {
        _ndPlayerStats.HealthPause = true;
        TweenChange(_ndHpBar, "value", _health, _ndPlayerStats.Health);
        
        _health = _ndPlayerStats.Health;
        GD.Print("HEALTH texture SIGNAL");
    }

    public void OnChangeReplenishHealth(float value)
    {
        _health = _ndPlayerStats.Health;
        _ndHpBar.Value = _health;
        GD.Print("Replenish HEALTH texture SIGNAL");
    }

    public void OnChangeEnergy(float value)
    {
        _ndPlayerStats.EnergyPause = true;
        TweenChange(_ndEnergyBar, "value", _energy, _ndPlayerStats.Energy);

        _energy = _ndPlayerStats.Energy;
        GD.Print("Energy SIGNAL");
    }

    public void OnChangeReplenishEnergy(float value)
    {
        _energy = _ndPlayerStats.Energy;
        _ndEnergyBar.Value = _energy;
        //GD.Print("Replenish Energy SIGNAL");
    }

    public void OnChangeMaxHealth(float value)
    {
        _ndHpBar.MaxValue = value;
    }

    public void OnChangeMaxEnergy(float value)
    {
        _ndEnergyBar.MaxValue = value;
    }

    public void OnChangeMoney(float value)
    {
        TweenChange(this, "_munyChange", _muny, _ndPlayerStats.Muny);

        _isMuny = true;
        _muny = _ndPlayerStats.Muny;
        GD.Print("Money SIGNAL");
    }



    public void OnChangeExp(float value)
    {
        TweenChange(this, "_expChange", _exp, _ndPlayerStats.Exp);

        _isExp = true;
        _exp = _ndPlayerStats.Exp;
        GD.Print("Exp SIGNAL");
    }

    public void TweenChange(Node objRef, string varName, float curValue, float newValue)
    {
        float valueChange = Math.Abs(curValue - newValue);
        
        if (curValue > newValue)
            _ndTween.InterpolateProperty(objRef, varName, curValue, curValue - valueChange, 1, Tween.TransitionType.Sine, Tween.EaseType.Out);
        else
            _ndTween.InterpolateProperty(objRef, varName, curValue, curValue + valueChange, 1, Tween.TransitionType.Sine, Tween.EaseType.Out);

        _ndTween.Start();
    }

    public void OnTweenStep(Godot.Object Obj, NodePath path, float timeElapsed, Godot.Object value)
    {
        if (_isExp) _ndExpCount.Text = ((int)_expChange).ToString();
        if (_isMuny) _ndMunyCount.Text = Calculations.IntStringCommas((int)_munyChange);

        //GD.Print("Tween step");
    }

    public void OnAllTweenCompletion()
    {
        //GD.Print("Tween Completed");
        _isExp = false;
        _isMuny = false;
        _ndPlayerStats.EnergyPause = false;
        _ndPlayerStats.HealthPause = false;
    }

    public void Hide()
    {
        _ndHpBar.Hide();
        _ndEnergyBar.Hide();
        _ndSkillA.Hide();
        _ndSkillB.Hide();
        _ndSkillC.Hide();
        _lblSkillA.Hide();
        _lblSkillB.Hide();
        _lblSkillC.Hide();
        _coolDownBarA.Hide();
        _coolDownBarB.Hide();
        _coolDownBarC.Hide();
        _ndMunyCount.Hide();
        _ndExpCount.Hide();
        _ndMunyBar.Hide();
        _ndExpBar.Hide();
        _ndMunyTexture.Hide();
        _ndExpTexture.Hide();
    }

    public void Show()
    {
        _ndHpBar.Show();
        _ndEnergyBar.Show();
        _ndSkillA.Show();
        _ndSkillB.Show();
        _ndSkillC.Show();
        _lblSkillA.Show();
        _lblSkillB.Show();
        _lblSkillC.Show();
        _coolDownBarA.Show();
        _coolDownBarB.Show();
        _coolDownBarC.Show();
        _ndMunyCount.Show();
        _ndExpCount.Show();
        _ndMunyBar.Show();
        _ndExpBar.Show();
        _ndMunyTexture.Show();
        _ndExpTexture.Show();
    }
}
