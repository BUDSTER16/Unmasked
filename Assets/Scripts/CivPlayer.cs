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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal < 0)
        {
            sprt.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprt.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}