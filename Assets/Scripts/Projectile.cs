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

    private void Start()
    {
        if (startDelayed) { StartCoroutine(StartDelay()); }
    }

    private void Update()
    {
        transform.right = target - (Vector2)transform.position;
        if (ready) { transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); }

        if (Vector2.Distance(transform.position, target) < 0.001f)
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
}
