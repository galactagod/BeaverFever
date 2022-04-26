using Godot;
using System;
using System.Collections.Generic;

public class DialogueManager
{
    //List containing all dialogue lines for the boss
    List<string> bossDialogue = new List<string>
    {
        "Welcome to your doom!",
        "You'll never defeat me!",
        "Nice try, but you'll have to do better.",
        "Arrrgghh! I can't believe I lost to a weak little beaver!"
    };

    //List containing all exposition lines
    List<string> expositionDialogue = new List<string>
    { 
        "There once was a family of beavers",
        "They lived a happy life until...",
        "The brave little beaver set out to rescue his parents.",
        "After our hero's brave journey, the dam was once again at peace."
    };

    List<string> tutorialDialogue = new List<string>
    {
        "Use the up arrow to jump.",
        "Tooltips in menu will help you, hover over tabs to see them.",
        "Click ESC key to access the menu.",
        "Jump on enemies to kill them and get XP.",
        "Walk into coins to collect them."
    };

    //Allows remote access to boss dialogue
    public string getBossLine(int interval)
    {
        return bossDialogue[interval];
    }

    //Allows remote access to exposition dialogue
    public string getExpositionLine(int interval)
    {
        return expositionDialogue[interval];
    }

    public string getTutorialLine(int interval)
    {
        return tutorialDialogue[interval];
    }
}