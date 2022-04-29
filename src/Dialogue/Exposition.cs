using System;
using Godot;

public class Exposition : RichTextLabel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    DialogueManager dialogueManager = new DialogueManager();

    LevelControl levelControl;

    string introDialogue;

    public override void _Ready()
    {
        introDialogue = dialogueManager.getIntroDialogue(0);
        this.AddText(introDialogue);
        this.ScrollToLine(-138);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      if(this.RectPosition >= new Vector2(this.RectPosition.x, -138))
      {
          levelControl.changeLevel("Tutorial");
      }
      //this.RectPosition = new Vector2(0, 0);
  }

    /*public void scrollText()
    {
        while(this.position.y > -138)
        {
            this.position.y
        }
    }*/
}
