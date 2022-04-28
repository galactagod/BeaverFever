using Godot;
using System;

public class DialoguePopUp : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    PopupDialog myPopUp;
    RichTextLabel popUpLabel;
    bool Popped = false;
    public override void _Ready()
    {
        myPopUp = GetNode<PopupDialog>("CanvasLayer/PopupDialog");
        popUpLabel = GetNode<RichTextLabel>("CanvasLayer/PopupDialog/Label");
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
