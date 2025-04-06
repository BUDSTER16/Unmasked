using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTrigger : DialogueTrigger
{
    [SerializeField] Vector2 destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<ActionPlayer>().LockPlayerInput();
            collision.GetComponent<ActionPlayer>().Sequence(destination);
        }
    }
}
