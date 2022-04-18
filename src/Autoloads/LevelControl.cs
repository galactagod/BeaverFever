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
                PlayAudio(_musicPlayer, _sndDreamFactory, -15, 1);
                break;
        }
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_menu"))
        {
            if(!paused)
            {
                GetTree().Paused = true;
                GetNode("/root/LevelTemplate/CanvasLayer/Control").Set("visible", true);
                paused = true;
            }
            else
            {
                GetTree().Paused = false;
                GetNode("/root/LevelTemplate/CanvasLayer/Control").Set("visible", false);
                paused = false;
            }
        }

    }

    public void unPause()
    {
        GetTree().Paused = false;
        GetNode("/root/LevelTemplate/CanvasLayer/Control").Set("visible", false);
        paused = false;
    }

    public void changeScene(string path)
    {
        var templateInvSlot = GD.Load<PackedScene>(path);
        var childRemoved = GetNode("/root/LevelTemplate/CanvasLayer/Control").GetChild(0);
        GetNode("/root/LevelTemplate/CanvasLayer/Control").RemoveChild(childRemoved);
        GetNode("/root/LevelTemplate/CanvasLayer/Control").AddChild(templateInvSlot.Instance());
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