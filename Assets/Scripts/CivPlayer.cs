using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivPlayer : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float speed = 4f;

    private float vertical, horizontal;

    Rigidbody2D rb;
    SpriteRenderer sprt;
    Animator animator;

    [SerializeField] GameObject tTip;

    GameObject activeTTip = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal < 0)
        {
            sprt.flipX = true;
            animator.SetBool("Walking", true);
        }
        else if (horizontal > 0)
        {
            sprt.flipX = false;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<DialogueTrigger>() != null || collision.GetComponent<MaskStand>() != null || collision.GetComponent<Elevator>() != null)
        {
            if(activeTTip == null) { activeTTip = Instantiate(tTip, transform.position + (Vector3.up * 1.4f), Quaternion.identity, gameObject.transform); }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (activeTTip != null)
        {
            Destroy(activeTTip);
            activeTTip = null;
        }
    }
}