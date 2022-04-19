using Godot;
using System;

public class InterfaceManager : CanvasLayer
{
    public static DialogueManager dialogueManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dialogueManager = GetNode("DialogueManager") as DialogueManager;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
