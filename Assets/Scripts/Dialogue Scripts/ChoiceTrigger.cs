using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceTrigger : DialogueTrigger
{
    private QuestManager questManager;

    [SerializeField] QuestManager.Quest quest;
    [SerializeField] bool stopVillain;

    [SerializeField] private Dialogue releasedDialogue;

    private bool choiceMade = false;

    private void Start()
    {
        questManager = FindFirstObjectByType<QuestManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && dialogueDisplaying)
            {
                if (!dialogueBox.activeInHierarchy)
                {
                    dialogueDisplaying = false;
                }
            }

            if (Input.GetKey(KeyCode.E) && !dialogueDisplaying)
            {
                if (stopVillain)
                {
                    Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
                    questManager.StopVillain();
                }
                else
                {
                    Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(releasedDialogue);
                    questManager.LeaveVillain();
                }
                choiceMade = true;
                dialogueDisplaying = true;
            }

            if(choiceMade && !dialogueDisplaying)
            {
                choiceMade = false;
                SceneManager.LoadScene("Test_Tasks");
            }
            if (!dialogueBox.activeInHierarchy)
            {
                dialogueDisplaying = false;
            }
        }
    }
}
