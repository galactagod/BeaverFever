using System;
using System.Collections.Generic;

public class NPCDialogue
{
    public int Index;
    public List<InterfaceSelectionObject> InterfaceSelectionObjects;
    public List<NPCDialogue> NPCDialogues;
    public string DisplayText;

    public NPCDialogue(List<InterfaceSelectionObject> interfaceSelectionObjects, string displayText, int index, List<NPCDialogue> dialogues = null)
    {
        InterfaceSelectionObjects = interfaceSelectionObjects;
        DisplayText = displayText;
        Index = index;
        if(dialogues != null)
        {
            NPCDialogues = dialogues;
        }
    }
}
