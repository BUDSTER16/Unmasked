using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 target;
    [SerializeField] float speed;
    bool ready = false;

    [SerializeField] int damage;

    [SerializeField] bool startDelayed;

    private float timeToLive = 5f;

    private void Start()
    {
        if (startDelayed) { StartCoroutine(StartDelay()); }
        else { ready = true; }
        transform.right = target - (Vector2)transform.position;
    }

    private void Update()
    {
        
        if (ready) { transform.position += transform.right * speed * Time.deltaTime; }

        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ready = true;
    }
    public int hit()
    {
        Destroy(gameObject);
        return damage;
    }

    private void Smash()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
        {
            Smash();
        }
    }
}
