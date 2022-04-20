using Godot;
using System;

public class SampleEventArgs
{
    public SampleEventArgs(string text) { Text = text; }
    public string Text { get; } // readonly
}

public class ReturnToGame : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    //public delegate void SampleEventHandler(object sender, SampleEventArgs e);

    //// Declare the event.
    //public event SampleEventHandler SampleEvent;

    //// Wrap the event in a protected virtual method
    //// to enable derived classes to raise the event.
    //protected virtual void RaiseSampleEvent()
    //{
    //    // Raise the event in a thread-safe manner using the ?. operator.
    //    SampleEvent?.Invoke(this, new SampleEventArgs("Hello"));
    //}

    private LevelControl levelControl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelControl = (LevelControl)GetNode("/root/LevelControl");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  

    public override void _Pressed()
    {
        //Raise Event here
        levelControl.unPause();
        
    }
}
