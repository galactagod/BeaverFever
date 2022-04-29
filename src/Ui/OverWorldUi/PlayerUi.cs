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

    // node reference
    private PlayerStats _ndPlayerStats;
    public TextureProgress _ndHpBar;
    private TextureProgress _ndEnergyBar;
    private TextureRect _ndSkillA;
    private TextureRect _ndSkillB;
    private TextureRect _ndSkillC;
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

        _ndHpBar.MaxValue = _ndPlayerStats.MaxHealth;
        _ndHpBar.Value = _ndPlayerStats.Health;

        _ndEnergyBar.MaxValue = _ndPlayerStats.MaxEnergy;
        _ndEnergyBar.Value = _ndPlayerStats.Energy;

        _ndMunyCount.Text = Calculations.IntStringCommas((int)_ndPlayerStats.Muny);
        _ndExpCount.Text = _ndPlayerStats.Exp.ToString();
    }

    public override void _Process(float delta)
    {
        _ndHpBar.Value = _ndPlayerStats.Health;
        _ndEnergyBar.Value = _ndPlayerStats.Energy;
    }

    public void OnHealthChange(float value)
    {
        TweenChange(_ndHpBar, "value", _health, _ndPlayerStats.Health);
        
        _health = _ndPlayerStats.Health;
        GD.Print("HEALTH texture SIGNAL");
    }
    public void OnChangeMaxHealth(float value)
    {
        _ndHpBar.MaxValue = value;
        
    }

    public void OnChangeMoney(float value)
    {
        TweenChange(this, "_munyChange", _muny, _ndPlayerStats.Muny);

        _isMuny = true;
        _muny = _ndPlayerStats.Muny;
        GD.Print("Money SIGNAL");
    }

    public void OnChangeEnergy(float value)
    {
        GD.Print("Energy SIGNAL");
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
    }

    public void Hide()
    {
        _ndHpBar.Hide();
        _ndEnergyBar.Hide();
        _ndSkillA.Hide();
        _ndSkillB.Hide();
        _ndSkillC.Hide();
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
        _ndMunyCount.Show();
        _ndExpCount.Show();
        _ndMunyBar.Show();
        _ndExpBar.Show();
        _ndMunyTexture.Show();
        _ndExpTexture.Show();
    }
}
