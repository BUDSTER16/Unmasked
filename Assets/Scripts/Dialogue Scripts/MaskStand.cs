using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskStand : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Dialogue dialogue;

    private QuestManager qManager;

    private void Start()
    {
        qManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E) && !qManager.VillainFought())
            {
                switch(qManager.CurrentQuest())
                {
                    case QuestManager.Quest.None:
                        break;
                    case QuestManager.Quest.Financier:
                        SceneManager.LoadScene("Financier_Combat");
                        break;
                    case QuestManager.Quest.Pseudo:
                        SceneManager.LoadScene("Pseudo_Combat");
                        break;
                    case QuestManager.Quest.Modder:
                        SceneManager.LoadScene("Modder_Combat");
                        break;
                    case QuestManager.Quest.Desire:
                        SceneManager.LoadScene("Desire_Combat");
                        break;
                }
            }
            else if(Input.GetKey(KeyCode.E) && !dialogueBox.activeInHierarchy)
            {
                Instantiate(dialogueBox).GetComponent<DialogueDisplay>().PassDialogue(dialogue);
            }
        }
    }
}
