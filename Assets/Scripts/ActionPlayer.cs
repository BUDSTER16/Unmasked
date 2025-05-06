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
    [SerializeField] GameObject slam;


    private float vertical, horizontal;

    private Power activePower;

    private bool throwOnCooldown = false;
    private bool blastOnCooldown = false;
    private bool dashOnCooldown = false;

    private bool blasting = false;
    private bool slamming = false;

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
            UsePower();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            UpwardBlast();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Dash();
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
        else if(!slamming) 
        {
            rb.velocity = Vector2.zero; 
            
        }
    }

    private void UsePower()
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
        yield return new WaitForSeconds(3f);
        blastOnCooldown = false;
    }

    IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(1.5f);
        dashOnCooldown = false;
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
        if(!blastOnCooldown && !blasting)
        {
            StartCoroutine(ResetGravity());
            StartCoroutine(UpBlastCooldown());
            rb.AddForce(new Vector2(0, 600));
            blasting = true;
        }else if(blasting)
        {
            slamming = true;
            blasting = false;
            rb.velocity.Set(0, 0);
            if(spriteRenderer.flipX)
            {
                rb.AddForce(new Vector2(-75, -75), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-75, -75), ForceMode2D.Impulse);
            }
        }
    }

    private void Dash()
    {
        RaycastHit2D target;
        if (spriteRenderer.flipX)
        {
            target = Physics2D.Raycast(transform.position, Vector2.left, 3);
        }
        else
        {
            target = Physics2D.Raycast(transform.position, Vector2.right, 3, LayerMask.GetMask("Terrain", "Default"));
        }
        

        if(target && !dashOnCooldown)
        {
            Debug.Log(target.transform.gameObject.name);
            if (target.transform.tag == "Enemy")
            {
                Vector3 targetPos = target.transform.position;
                target.transform.GetComponent<Enemy>().DieToDash();
                transform.position = targetPos;
            }
            
        }
        else if (!dashOnCooldown)
        {
            if (spriteRenderer.flipX)
            {
                transform.position -= new Vector3(3, 0, 0);
            }
            else
            {
                transform.position += new Vector3(3, 0, 0);
            }
            StartCoroutine(DashCooldown());
        }
    }

    IEnumerator ResetGravity()
    {
        rb.gravityScale = 0.5f;
        blasting = true;
        yield return new WaitForSeconds(1);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(blasting && Physics2D.Raycast(transform.position, -Vector2.up, 1, 6))
        {
            blasting = false;
        }
        if(slamming)
        {
            Slam();
            slamming = false;
            Debug.Log("slammed the ground");
        }
    }

    private void Slam()
    {
        Instantiate(slam, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
    }
}
