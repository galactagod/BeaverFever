using Godot;
using System;
using System.Collections.Generic;

public class NPC : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private List<NPCDialogue> npcDialogue;

    private Button btn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        btn = this.GetNode<Button>("CanvasLayer/Button");
        btn.Connect("pressed", this, nameof(onButtonPressed));

        GD.Print(btn.Text);
        
        InterfaceSelectionObject obj = new InterfaceSelectionObject(1, "Yes please");
        InterfaceSelectionObject obj2 = new InterfaceSelectionObject(2, "No thanks");
        InterfaceSelectionObject obj3 = new InterfaceSelectionObject(-1, "OK");
        npcDialogue = new List<NPCDialogue>
        {
            new NPCDialogue(new List<InterfaceSelectionObject>(){obj, obj2}, "Would you like to buy this?", 0),
            new NPCDialogue(new List<InterfaceSelectionObject>(){obj3}, "Thank you!", 1),
            new NPCDialogue(new List<InterfaceSelectionObject>(){obj3}, "Come again!", 2)
        };
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void onButtonPressed()
    {
        GD.Print("button clicked");
        setNPCDialogue();
        InterfaceManager.dialogueManager.ShowDialogueElement();
    }
    
    /*public void _on_Button_pressed()
    {
        GD.Print("Hey!");
        setNPCDialogue();
        InterfaceManager.dialogueManager.ShowDialogueElement();
    }

    public void _on_Button_toggled()
    {
        GD.Print("Hey!");
        setNPCDialogue();
        InterfaceManager.dialogueManager.ShowDialogueElement();
    }*/

    public void setNPCDialogue()
    {
        InterfaceManager.dialogueManager.npcDialogue = npcDialogue;
    }
}
