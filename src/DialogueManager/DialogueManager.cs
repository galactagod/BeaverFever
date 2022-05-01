using Godot;
using System;
using System.Collections.Generic;

public static class DialogueManager
{
    //List containing all dialogue lines for the boss
    static List<string> bossDialogue = new List<string>
    {
        "Welcome to your doom!",
        "You'll never defeat me!",
        "Nice try, but you'll have to do better.",
        "Arrrgghh! I can't believe I lost to a weak little beaver!"
    };
    static List<string> tutorialDialogue = new List<string>
    {
        "Use the up arrow to jump.",
        "Tooltips in menu will help you, hover over tabs to see them.",
        "Click ESC key to access the menu.",
        "Jump on enemies to kill them and get XP.",
        "Walk into coins to collect them.",
        "Have you not played Mario before lol. Get to the flag."
    };

    static List<string> miscellanousDialogue = new List<string>
    {
        "Hope you like jumping ;)",
        "There's a snake in my boot.",
        "Try left, then try right.",
        "Hit the gym to work on the vertical. Or...be a little crafty.",
        "Bewear of bear",
        "Bear with the jumps",
        "You can equip different items in different slots. Hover over the item to see where you can equip it. Try the slots until it drops in.",
        "A little too far...try up",
        "Did you check everywhere?",
        "Equip items by clicking ESC and dragging and dropping the item into its equip slot."
    };

    //Put intro dialogue here
    static List<string> introDialogue = new List<string>
    {
        "There once was a beaver who lived in a village with his lovely parents. On a cold winter's night, a large wolf came and brutally murdered the rest of the tribe, leaving the lone beaver to fend for himself. With the power of God and anime at his side, he vows to defeat the wolf at any cost necessary."
    };

    static List<string> endingDialogue = new List<string>
    {
        "The beaver finally got his vengenance, and he is now at peace. Until a foe crosses his path again..."
    };

    //Allows remote access to boss dialogue
    public static string getBossLine(int interval)
    {
        return bossDialogue[interval];
    }

    public static string getTutorialLine(int interval)
    {
        return tutorialDialogue[interval];
    }

    public static string getMiscellanousDialogue(int interval)
    {
        return miscellanousDialogue[interval];
    }

    public static string getIntroDialogue(int interval)
    {
        return introDialogue[interval];
    }

    public static string getEndingDialogue(int interval)
    {
        return endingDialogue[interval];
    }
}