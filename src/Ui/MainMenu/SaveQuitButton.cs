using Godot;
using System;

public class SaveQuitButton : TextureButton
{
    private PlayerData playerData;
    private PlayerStats playerStats;
    public override void _Ready()
    {
        playerData = GetNode<PlayerData>("/root/PlayerData");
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }

    public override void _Pressed()
    {
        playerData.Save();
    }
}
