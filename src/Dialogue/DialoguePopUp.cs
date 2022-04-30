using Godot;
using System;

public class DialoguePopUp : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    Popup myPopUp;
    RichTextLabel popUpLabel;
    bool Popped = false;
    public override void _Ready()
    {
        myPopUp = GetNode<Popup>("CanvasLayer/Popup");
        popUpLabel = GetNode<RichTextLabel>("CanvasLayer/Popup/TextureRect/Label");
    }

    public void PopUp(string text)
    {
        popUpLabel.Text = text;
        myPopUp?.Show();
        Popped = true;
    }

    public void UnPop()
    {
        if(Popped)
        {
            myPopUp?.Hide();
            Popped = false;
        }
        
    }

    public bool Status()
    {
        return Popped;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
