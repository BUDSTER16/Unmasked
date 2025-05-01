using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Player Stats")]
    [SerializeField] float speed = 10f;

    [Header("Powers")]
    [SerializeField] GameObject sword;


    private float vertical, horizontal;

    private Power activePower;

    private bool throwOnCooldown = false;
    private bool blastOnCooldown = false;

    private bool blasting = false;

    private int HP = 14;

    private bool lockMovement = false;
    private Vector2 dest;


    [SerializeField] GameObject tTip;

    GameObject activeTTip = null;

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
            UseThrow();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            UpwardBlast();
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
        if (!lockMovement) { rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); }
        else 
        {
            rb.velocity = Vector2.zero; 
            
        }
    }

    private void UseThrow()
    {
        if(!throwOnCooldown)
        {
            switch (activePower)
            {
                case Power.Swords:
                    Instantiate(sword, transform.position + (Vector3.up * 1.4f), Quaternion.identity).GetComponent<Projectile>().SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    GetComponent<Animator>().SetTrigger("Attack");
                    break;
            }
            StartCoroutine(SwordThrowCooldown());
        }
    }

    IEnumerator SwordThrowCooldown()
    {
        throwOnCooldown = true;
        yield return new WaitForSeconds(0.7f);
        throwOnCooldown = false;
    }

    IEnumerator UpBlastCooldown()
    {
        blastOnCooldown = true;
        yield return new WaitForSeconds(5f);
        blastOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        }else if (collision.GetComponent<DialogueTrigger>() != null || collision.GetComponent<MaskStand>() != null || collision.GetComponent<Elevator>() != null)
        {
            if (activeTTip == null) { 
                if(collision.GetComponent<DialogueTrigger>() != null && !collision.GetComponent<SequenceTrigger>())
                activeTTip = Instantiate(tTip, transform.position + (Vector3.up * 1.4f), Quaternion.identity, gameObject.transform); 
            }
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

    private void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LockPlayerInput()
    {
        lockMovement = true;
    }

    public void Sequence(Vector2 destination)
    {
        dest = destination;
    }

    private void UpwardBlast()
    {
        if(!blastOnCooldown)
        {
            StartCoroutine(ResetGravity());
            StartCoroutine(UpBlastCooldown());
            rb.AddForce(new Vector2(0, 1000));
        }
    }

    IEnumerator ResetGravity()
    {
        rb.gravityScale = 0;
        blasting = true;
        yield return new WaitForSeconds(1);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(blasting)
        {
            blasting = false;
        }
    }
}
