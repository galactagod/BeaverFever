using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public class DialogueManager : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public List<NPCDialogue> npcDialogue;

    [Export]
    public PackedScene InterfaceSelectableObject;

    public List<InterfaceSelection> Selections = new List<InterfaceSelection>();

    //Is the dialogue menu open?
    private bool isDialogueUp;

    //Which item in the selection list is being hovered
    private int currentSelectionIndex = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public async override void _Process(float delta)
    {
        //checking dialogue menu is open
        if(isDialogueUp)
        {
            //moving left in the selections menu
            if(Input.IsActionJustPressed("ui_left"))
            {
                foreach(var item in Selections)
                {
                    item.SetSelected(false);
                }

                currentSelectionIndex -= 1;
                if(currentSelectionIndex < 0)
                {
                    currentSelectionIndex = 0;
                }

                Selections[currentSelectionIndex].SetSelected(true);
            }
            //moving right in the selections menu
            else if(Input.IsActionJustPressed("ui_right"))
            {
                foreach(var item in Selections)
                {
                    item.SetSelected(false);
                }

                currentSelectionIndex += 1;
                if(currentSelectionIndex > Selections.Count - 1)
                {
                    currentSelectionIndex = Selections.Count - 1;
                }

                Selections[currentSelectionIndex].SetSelected(true);
            }
            //selecting an item in the selections menu
            else if(Input.IsActionJustPressed("ui_accept"))
            {
                await ToSignal(GetTree(), "idle_frame");
                displayNextDialogueElement(Selections[currentSelectionIndex].interfaceSelectionObject.SelectionIndex);
            }
        }
    }

    public void ShowDialogueElement()
    {
        GetNode<Popup>("Popup").Popup_();
        GetNode<Label>("Popup/CharacterName").Text = "test name";
        WriteDialogue(npcDialogue[0]);
    }

    public void WriteDialogue(NPCDialogue dialogue)
    {
        foreach(Node item in GetNode<Node>("Popup/Options").GetChildren())
        {
            item.QueueFree();
        }
        Selections = new List<InterfaceSelection>();

        GetNode<RichTextLabel>("Popup/DialogueLine").Text = dialogue.DisplayText;
        foreach(var item in dialogue.InterfaceSelectionObjects)
        {
            InterfaceSelection interfaceSelection = InterfaceSelectableObject.Instance() as InterfaceSelection;
            interfaceSelection.interfaceSelectionObject = item;
            GetNode<HBoxContainer>("Popup/Options").AddChild(interfaceSelection);
            Selections.Add(interfaceSelection);
            interfaceSelection.SetSelected(false);
        }

        Selections[0].SetSelected(true);
        isDialogueUp = true;
    }

    private void shutdownDialogue()
    {
        GetNode<Popup>("Popup").Hide();
        isDialogueUp = false;
    }

    private void displayNextDialogueElement(int index)
    {
        //if the index is bad for some reason
        if(npcDialogue.ElementAtOrDefault(index) == null || index == -1)
        {
            shutdownDialogue();
        }
        //if index is valid
        else
        {
            WriteDialogue(npcDialogue[index]);
        }
    }
}
