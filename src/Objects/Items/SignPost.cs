using System;
using Godot;

public class SignPost : Area2D
{
    [Export]
    public int whichDialogue;
    [Export]
    public string whichSet;

    DialoguePopUp dialoguePopUp;

    DialogueManager dialogueManager = new DialogueManager();


    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(OnBodyEntered));
        this.Connect("body_exited", this, nameof(OnBodyExited));
        dialoguePopUp = GetNode<DialoguePopUp>("/root/DialoguePopUp");
    }

    

    public void OnBodyEntered(Node body)
    {
        if(body is ObjPlayer)
        {
            Console.WriteLine("The message is " + whichDialogue);
            if (whichSet == "Tutorial")
                dialoguePopUp.PopUp(dialogueManager.getTutorialLine(whichDialogue));
            else if (whichSet == "Misc")
                dialoguePopUp.PopUp(dialogueManager.getMiscellanousDialogue(whichDialogue));
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
        }
    }
}
