using System;
using Godot;

public class SignPost : Area2D
{
    [Export]
    public int whichDialogue;

    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(Node body)
    {
        if(body is ObjPlayer)
            Console.WriteLine("The message is " + whichDialogue);

    }
}
