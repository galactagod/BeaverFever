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

    List<string> tutorialDialogue = new List<string>
    {
        "Use the up arrow to jump.",
        "Tooltips in menu will help you, hover over tabs to see them.",
        "Click ESC key to access the menu.",
        "Jump on enemies to kill them and get XP.",
        "Walk into coins to collect them.",
        "Have you not played Mario before lol. Walk into the flag."
    };

    List<string> miscellanousDialogue = new List<string>
    {
        "Hope you like jumping ;)",
        "There's a snake in my boot.",
        "Try left, then try right."
    };

    //Put intro dialogue here
    List<string> introDialogue = new List<string>
    {
        "There once was a beaver who lived in a village with his lovely parents. On a cold winter's night, a large wolf came and brutally murdered the rest of the tribe, leaving the lone beaver to fend for himself. With the power of God and anime at his side, he vows to defeat the wolf at any cost necessary."
    };

    //Allows remote access to boss dialogue
    public string getBossLine(int interval)
    {
        return bossDialogue[interval];
    }

    public string getTutorialLine(int interval)
    {
        return tutorialDialogue[interval];
    }

    public string getMiscellanousDialogue(int interval)
    {
        return miscellanousDialogue[interval];
    }

    public string getIntroDialogue(int interval)
    {
        return introDialogue[interval];
    }
}