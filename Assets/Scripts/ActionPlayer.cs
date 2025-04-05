using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Player Stats")]
    [SerializeField] float speed = 6f;

    [Header("Powers")]
    [SerializeField] GameObject sword;


    private float vertical, horizontal;

    private Power activePower;

    private bool powerOnCooldown = false;

    enum Power
    {
        Swords
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        activePower = Power.Swords;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }else if(horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            UsePower();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed / 2);
    }

    private void UsePower()
    {
        if(!powerOnCooldown)
        {
            switch (activePower)
            {
                case Power.Swords:
                    Instantiate(sword, transform.position + (Vector3.up * 1.4f), Quaternion.identity).GetComponent<Projectile>().SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    break;
            }
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        powerOnCooldown = true;
        yield return new WaitForSeconds(0.7f);
        powerOnCooldown = false;
    }
}
