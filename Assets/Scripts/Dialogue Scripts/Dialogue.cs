using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] string dialogueName;
    [SerializeField] string[] dialogueText;

    public string GetName()
    {
        return dialogueName;
    }

    public string[] GetText()
    {
        return dialogueText;
    }

    
}
