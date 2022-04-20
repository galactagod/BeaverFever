using Godot;
using System;

public class DialogueManager : Node
{
    //List containing all dialogue lines for the boss
    List<string> bossDialogue = new List<string>
    (
        "Welcome to your doom!",
        "You'll never defeat me!",
        "Nice try, but you'll have to do better.",
        "Arrrgghh! I can't believe I lost to a weak little beaver!"
    );

    //List containing all exposition lines
    List<string> expositionDialogue = new List<string>
    (
        "There once was a family of beavers",
        "They lived a happy life until...",
        "The brave little beaver set out to rescue his parents.",
        "After our hero's brave journey, the dam was once again at peace."
    );

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        
    }

    //Allows remote access to boss dialogue
    public string getBossLine(int interval)
    {
        return bossDialogue[interval];
    }
}