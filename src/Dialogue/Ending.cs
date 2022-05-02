using System;
using Godot;

public class Ending : RichTextLabel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    LevelControl levelControl;

    string endingDialogue;

    public override void _Ready()
    {
        endingDialogue = DialogueManager.getEndingDialogue(0);
        this.AddText(endingDialogue);
        levelControl = GetNode<LevelControl>("/root/LevelControl");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      if(this.RectPosition <= new Vector2(this.RectPosition.x, -138))
      {
          finish();
      }
      this.RectPosition = new Vector2(this.RectPosition.x, this.RectPosition.y - 1);
  }

    /*public void scrollText()
    {
        while(this.position.y > -138)
        {
            this.position.y
        }
    }*/

    public void finish()
    {
        //levelControl.LevelChange(GD.Load<PackedScene>("res://src/Levels/Tutorial.tscn"));
        GetTree().Quit();
    }
}
