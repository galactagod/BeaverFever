using System;
using Godot;

public class SignPost : Area2D
{
    [Export]
    public int whichDialogue;
    [Export]
    public string whichSet;

    DialoguePopUp dialoguePopUp;


    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(OnBodyEntered));
        this.Connect("body_exited", this, nameof(OnBodyExited));
        
    }

    

    public void OnBodyEntered(Node body)
    {
        
        if (body is ObjPlayer)
        {
            AddChild(GD.Load<PackedScene>("res://src/Dialogue/DialoguePopUp.tscn").Instance());
            dialoguePopUp = GetNode<DialoguePopUp>("DialoguePopUp");
            Console.WriteLine("The message is " + whichDialogue);
            if (whichSet == "Tutorial")
                dialoguePopUp.PopUp(DialogueManager.getTutorialLine(whichDialogue));
            else if (whichSet == "Misc")
                dialoguePopUp.PopUp(DialogueManager.getMiscellanousDialogue(whichDialogue));
            else
                dialoguePopUp.PopUp("Hi");
        }
    }

    public void OnBodyExited(Node body)
    {
        if(body is ObjPlayer)
        {
            Console.WriteLine("The message is " + whichDialogue);
            dialoguePopUp.UnPop();
            GetNode("DialoguePopUp").QueueFree();
        } 
    }
}
