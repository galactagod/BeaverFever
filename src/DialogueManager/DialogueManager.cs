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
        "Walk into coins to collect them."
    };

    List<string> miscellanousDialogue = new List<string>
    {
        "Hope you like jumping ;)"
    };

    //Put intro dialogue here
    List<string> introDialogue = new List<string>
    {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris nunc congue nisi vitae suscipit tellus mauris a. Id diam vel quam elementum pulvinar etiam non quam lacus. Nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus. Nec ullamcorper sit amet risus nullam. Amet consectetur adipiscing elit ut aliquam. Rutrum tellus pellentesque eu tincidunt. Aliquam faucibus purus in massa tempor nec feugiat nisl pretium. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies. Leo vel orci porta non pulvinar neque laoreet. Neque aliquam vestibulum morbi blandit cursus risus at ultrices. Porttitor leo a diam sollicitudin tempor id eu nisl. Tristique sollicitudin nibh sit amet. Non consectetur a erat nam. Lobortis mattis aliquam faucibus purus. Libero id faucibus nisl tincidunt eget nullam non. Accumsan lacus vel facilisis volutpat est velit egestas dui id."
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