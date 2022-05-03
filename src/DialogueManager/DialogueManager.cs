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
        "Tooltips in menu will help you, hover over tabs to see them. Click ESC key to access the menu. Make sure to hover over each tab in the menu to see what it does.",
        "Make sure to explore levels in great detail. Items are avaliable in chests and can be drag-n-dropped in the inventory screen,",
        "Jump on enemies to kill them and get XP. It will take lots of jumps, your stats are low right now. Increase your stats in the stat page and by equipping items.",
        "Walk into coins to collect them.",
        "Mario?",
        "Skills can be bought in the skills tab and seen in the inventory tab by clicking the bottom facing arrow in the top right."
    };

    static List<string> miscellanousDialogue = new List<string>
    {
        "Hope you like jumping ;)", //0
        "There's a snake in my boot.",
        "Try left, then try right.",
        "Hit the gym to work on the vertical. Or...be a little crafty.",
        "Bewear of bear. The animals are stubborn, they don't like to look up. Use the ramps to your advantage.",
        "Bear with the jumps", //5
        "You can equip different items in different slots. Hover over the item to see where you can equip it. Try the slots until it drops in.",
        "A little too far...try up",
        "Did you check everywhere?",
        "Equip items by clicking ESC and dragging and dropping the item into its equip slot.",
        "As we get closer to the mountain, madness consumes the animals, making them even stronger. Be extra careful.", //10
        "Bear dens ahead.",
        "The evil of the forest leaks through to the animals. Different colored enemies are stronger. Be careful.",
        "The peak of the mountain surges with evil power. The animals are overcome with the evil.",
        "My feet hurt.",
        "Long walk.", //15
        "Spiders are pesky creatures. Get up close and stomp them to reap your reward.",
        "There are lots of strong enemies, be extra careful. Remember to use potions and level up.",
        "This is the final frontier! You can hear the cries of your family? Are they still alive?",
        "You can faintly hear the cries up above!",
        "You're getting closer! The cries are so loud!", //20
        "You got further away...the sounds died down.",
        "The cries are getting louder.",
        "The cries are heard up above, but faintly...",
        "You can hitch a ride on crows to move through the map"
    };

    //Put intro dialogue here
    static List<string> introDialogue = new List<string>
    {
        "There once was a beaver who lived in a village with his lovely parents. On a cold winter's night, a large wolf came and brutally murdered the rest of the tribe, leaving the lone beaver to fend for himself. With the power of God and anime at his side, he vows to defeat the wolf at any cost necessary."
    };

    static List<string> endingDialogue = new List<string>
    {
        "The beaver's family is alive! The rumors of the wolf eating them weren't true. But the wolf is still out there. Will we get our vengence?....More story coming soon."
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