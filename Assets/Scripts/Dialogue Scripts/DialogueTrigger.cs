using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] protected GameObject dialogueBox;
    [SerializeField] protected Dialogue dialogue;

    [SerializeField] private bool contactTrigger;
    [SerializeField] private int cutsceneIndex; // 0 for none

    protected bool dialogueDisplaying = false;

    

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !contactTrigger)
        {
            if(Input.GetKey(KeyCode.E) && dialogueDisplaying)
            {
                if (!dialogueBox.activeInHierarchy)
                {
                    dialogueDisplaying = false;
                }
            }
            if (Input.GetKey(KeyCode.E) && !dialogueDisplaying)
            {
                Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
                dialogueDisplaying = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && contactTrigger)
        {
            
            if (!dialogueDisplaying)
            {
                Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
                dialogueDisplaying = true;
            }
            if(cutsceneIndex > 0)
            {
                FindObjectOfType<CutsceneManager>().TriggerCutscene(cutsceneIndex);
            }
            Destroy(gameObject);
        }
    }
}
