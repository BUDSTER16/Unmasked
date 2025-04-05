using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField] Dialogue dialogue;

    private bool dialogueDisplaying = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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
                Debug.Log("A player collided and pressed E!");
                Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
                dialogueDisplaying = true;
            }
        }
    }
}
