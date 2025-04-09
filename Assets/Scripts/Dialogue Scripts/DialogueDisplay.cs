using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text nameBox;
    [SerializeField] TMP_Text dialogueBox;

    private QuestTrigger flag;

    private Dialogue currentDialogue;
    private int currentIndex = 0;

    public void PassDialogue(Dialogue current)
    {
        currentDialogue = current;

        nameBox.text = currentDialogue.GetName();
        dialogueBox.text = currentDialogue.GetText()[currentIndex];
        currentIndex++;

        Time.timeScale = 0;
    }

    public void PassDialogue(Dialogue current, QuestTrigger POI)
    {
        currentDialogue = current;

        nameBox.text = currentDialogue.GetName();
        dialogueBox.text = currentDialogue.GetText()[currentIndex];
        currentIndex++;

        flag = POI;

        Time.timeScale = 0;
    }

    public void NextLine()
    {
        if(currentDialogue.GetText().Length > currentIndex)
        {
            dialogueBox.text = currentDialogue.GetText()[currentIndex];
            currentIndex++;
            AdvanceCutscene();
        }
        else
        {
            if(flag != null)
            {
                flag.EndText();
                flag = null;
            }
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    private void AdvanceCutscene()
    {
        FindAnyObjectByType<CutsceneManager>().NextPanel();
    }
}
