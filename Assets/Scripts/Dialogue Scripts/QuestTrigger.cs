using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : DialogueTrigger
{

    private QuestManager questManager;

    [SerializeField] QuestManager.Quest quest;

    [SerializeField] private Dialogue stoppedDialogue;
    [SerializeField] private Dialogue releasedDialogue;

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

            if (Input.GetKey(KeyCode.E) && !dialogueDisplaying && questManager.CurrentQuest() == quest)
            {
                if (questManager.VillainStopped(quest))
                {
                    Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(stoppedDialogue);
                }
                else
                {
                    Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(releasedDialogue);
                }

                dialogueDisplaying = true;
                questManager.CompleteQuest();
            }
            else if(Input.GetKey(KeyCode.E) && !dialogueDisplaying && questManager.CurrentQuest() != quest)
            {
                Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
                dialogueDisplaying = true;
            }
        }
    }
}
