using Godot;
using System;

public class LevelControl : Node
{
    private AudioStreamPlayer _musicPlayer;
    private AudioStreamPlayer[] _sfxPlayers = new AudioStreamPlayer[5];
    private int _maxSfxPlayers = 5;
    private int _sfxPriority = -1;
    private Node _curNodeScene;


    // audio stream paths
    private AudioStreamOGGVorbis _sndDreamFactory = (AudioStreamOGGVorbis)GD.Load("res://src/Assets/Sounds/Levels/SndDreamFactory.ogg");
    private AudioStreamOGGVorbis _sndEpicDeparture = (AudioStreamOGGVorbis)GD.Load("res://src/Assets/Sounds/Levels/SndEpicDeparture.ogg");
    private AudioStreamOGGVorbis _sndEternalExplorer = (AudioStreamOGGVorbis)GD.Load("res://src/Assets/Sounds/Levels/SndEternalExplorer.ogg");
    private AudioStreamOGGVorbis _sndLargo = (AudioStreamOGGVorbis)GD.Load("res://src/Assets/Sounds/Levels/SndLargo.ogg");

    private bool paused = false;

    public string rootPath = "/root/LevelTemplate/CanvasLayer/Control/";
    public string controlPath = "/root/LevelTemplate/CanvasLayer/Control";

    public string nameOfCurrentScene { get; set; }

    private PlayerData playerData;
    private PlayerUi playerUi;
    public override void _Ready()
    {
        // create the audio files for music and effects
        _musicPlayer = new AudioStreamPlayer();
        AddChild(_musicPlayer);

        for (int i = 0; i < _maxSfxPlayers; i++)
        {
            _sfxPlayers[i] = new AudioStreamPlayer();
            AddChild(_sfxPlayers[i]);
        }

        _curNodeScene = GetTree().CurrentScene;


        switch (_curNodeScene.Name)
        {
            case "LevelTemplate":
                //PlayAudio(_musicPlayer, _sndDreamFactory, -15, 1);
                break;
        }

        PauseMode = PauseModeEnum.Process;

        playerData = GetNode<PlayerData>("/root/PlayerData");
        playerUi = GetNode<PlayerUi>("/root/PlayerUi");


        //Used to TP the player to the level they saved their game on.
        //changeLevel(playerData.currentLevel);
        //comingFromDeath();
    }

    public override void _Process(float delta)
    {
        if (nameOfCurrentScene != "Intro" || nameOfCurrentScene != "Ending" || nameOfCurrentScene != "Title Screen")
        {
            if (Input.IsActionJustPressed("ui_menu"))
            {
                if (!paused)
                {
                    GetTree().Paused = true;
                    GetNode(controlPath).Set("visible", true);
                    paused = true;
                }
                else
                {
                    GetTree().Paused = false;
                    GetNode(controlPath).Set("visible", false);
                    changeScene("res://src/Ui/MainMenu/MasterUI.tscn");
                    paused = false;
                }
            }
        }
    }

    public void unPause()
    {
        GetTree().Paused = false;
        GetNode(controlPath).Set("visible", false);
        paused = false;
    }

    public void changeScene(string path)
    {
        var templateInvSlot = GD.Load<PackedScene>(path);
        
        if(GetNode(controlPath).GetChildCount() > 0)
        {
            var childRemoved = GetNode(controlPath).GetChild(0);
            GetNode(controlPath).RemoveChild(childRemoved);
            childRemoved.QueueFree();
        }
        
        GetNode(controlPath).AddChild(templateInvSlot.Instance());
    }

    public void changeLevel(string sceneName)
    {
        nameOfCurrentScene = sceneName;
        rootPath = "/root/" + sceneName + "/CanvasLayer/Control/";
        controlPath = "/root/" + sceneName + "/CanvasLayer/Control";
        if(nameOfCurrentScene == "Intro" || nameOfCurrentScene == "Ending" || nameOfCurrentScene == "Title Screen")
        {
            playerUi.Hide();
        }
        else
        {
            playerUi.Show();
        }

        if (nameOfCurrentScene == "Tutorial" || nameOfCurrentScene == "LevelTemplate" || nameOfCurrentScene == "ExtraLevelTrevor" || nameOfCurrentScene == "AnotherExtraLevelTrevor")
        {
            PlayAudio(_musicPlayer, _sndDreamFactory, -15, 1);
        }
        else if(nameOfCurrentScene == "CountryLVL")
        {
            PlayAudio(_musicPlayer, _sndEpicDeparture, -15, 1);
        }
        else if (nameOfCurrentScene == "MountainLVL")
        {
            PlayAudio(_musicPlayer, _sndEternalExplorer, -15, 1);
        }
        else if (nameOfCurrentScene == "Mountain-UndergroundLVL")
        {
            PlayAudio(_musicPlayer, _sndLargo, -15, 1);
        }


    }

    public void playerDied()
    {
        GetTree().ChangeSceneTo(GD.Load<PackedScene>("res://src/Ui/GameOverScreen.tscn"));
        rootPath = "/root/" + "GameOverScreen" + "/CanvasLayer/Control/";
        controlPath = "/root/" + "GameOverScreen" + "/CanvasLayer/Control";
        playerUi.Hide();
    }

    public void comingFromDeath()
    {
        if(nameOfCurrentScene == "Tutorial")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/Tutorial.tscn"));
        }
        else if(nameOfCurrentScene == "LevelTemplate")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/LevelTemplate.tscn"));
        }
        else if(nameOfCurrentScene == "ExtraLevelTrevor")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/ExtraLevelTrevor.tscn"));
        }
        else if(nameOfCurrentScene == "AnotherExtraLevel")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/AnotherExtraLevel.tscn"));
        }
        else if (nameOfCurrentScene == "CountryLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/CountryLVL.tscn"));
        }
        else if (nameOfCurrentScene == "MountainLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/MountainLVL.tscn"));
        }
        else if (nameOfCurrentScene == "Mountain-UndergroundLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/Mountain-UndergroundLVL.tscn"));
        }
    }

    public void replayFromBeginning()
    {
        LevelChange(GD.Load<PackedScene>("res://src/Levels/Tutorial.tscn"));
    }

    public void changeBasedOnName(string nameOfChange)
    {
        if (nameOfChange == "Tutorial")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/Tutorial.tscn"));
        }
        else if (nameOfChange == "LevelTemplate")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/LevelTemplate.tscn"));
        }
        else if (nameOfChange == "ExtraLevelTrevor")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/ExtraLevelTrevor.tscn"));
        }
        else if (nameOfChange == "AnotherExtraLevel")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/AnotherExtraLevel.tscn"));
        }
        else if (nameOfChange == "CountryLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/CountryLVL.tscn"));
        }

        else if (nameOfChange == "MountainLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/MountainLVL.tscn"));
        }
        else if (nameOfChange == "Mountain-UndergroundLVL")
        {
            LevelChange(GD.Load<PackedScene>("res://src/Levels/Mountain-UndergroundLVL.tscn"));
        }
    }

    public void LevelChange(PackedScene teleportTo)
    {
        //playerUIConstant = GetNode("/root/" + nameOfCurrentScene + "/PlayerUi");
        //Console.WriteLine("I hit here");
        GetTree().ChangeSceneTo(teleportTo);
    }

    // use the five sfx players to prioritize playing most recent sounds
    public void SfxPlayerManager(int channel, AudioStream stream, float volume, float pitch)
    {
        bool allOpen = true;

        // check if any players are open after we get one we leave
        for (int i = 0; i < _maxSfxPlayers; i++)
        {
            if (channel != -1)
            {
                PlayAudio(_sfxPlayers[channel], stream, volume, pitch);
                break;
            }

            if (_sfxPlayers[i].Playing == false)
            {
                PlayAudio(_sfxPlayers[i], stream, volume, pitch);

                // if on the last player notify that they are all closed
                allOpen = (i == _maxSfxPlayers - 1) ? false : true;
                //GD.Print(" I = " + i + " | AllOpen = " + allOpen + " | Playing = " + stream);
                break;
            }
        }

        // make sure to check which players to cycle through
        //GD.Print("Before SfxPriority = " + _sfxPriority);
        _sfxPriority = (allOpen) ? _sfxPriority = -1 : _sfxPriority + 1;
        _sfxPriority = (_sfxPriority >= _maxSfxPlayers) ? _sfxPriority = 0 : _sfxPriority;
        //GD.Print("After SfxPriority = " + _sfxPriority);

        // override the oldest sfx players for new sounds
        if (!allOpen)
        {
            PlayAudio(_sfxPlayers[_sfxPriority], stream, volume, pitch);
            //GD.Print("Overwride Playing = " + stream);
        }
    }

    public void PlayAudio(AudioStreamPlayer player, AudioStream stream, float volume, float pitch)
    {
        player.Stream = stream;
        player.VolumeDb = volume;
        player.PitchScale = pitch;
        player.Play();
    }


}
