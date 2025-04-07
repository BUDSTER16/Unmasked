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

    private int HP = 14;

    private bool lockMovement = false;
    private Vector2 dest;

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

        if(Input.GetMouseButtonDown(0) && !lockMovement)
        {
            UsePower();
        }

        if(lockMovement)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, dest) < 0.01)
            {
                lockMovement = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!lockMovement) { rb.velocity = new Vector2(horizontal * speed, vertical * speed / 1.5f); }
        else 
        {
            rb.velocity = Vector2.zero; 
            
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Enemy Projectile")
        {
            HP -= collision.GetComponent<Projectile>().hit();

            if (HP <= 0)
            {
                Kill();
            }

            StartCoroutine(Hit());
            IEnumerator Hit()
            {
                GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    private void Kill()
    {
        Debug.Log("PLAYER DIED");
        Destroy(gameObject);
    }

    public void LockPlayerInput()
    {
        lockMovement = true;
    }

    public void Sequence(Vector2 destination)
    {
        dest = destination;
    }
}
