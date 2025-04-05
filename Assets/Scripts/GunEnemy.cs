using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : Enemy
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;

    private float attackTimer = 3f;
    [SerializeField] private float attackCD = 3f;

    private Transform player;

    private void Start()
    {
        player = FindAnyObjectByType<ActionPlayer>().gameObject.transform;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                muzzle.transform.localPosition = new Vector3(-0.5f, 0.12f, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                muzzle.transform.localPosition = new Vector3(0.4f, 0.12f, 0);
            }
        }

        if(attackTimer <= 0)
        {
            attackTimer = attackCD;
            Attack();
        }
    }

    private void Attack()
    {
        if (player != null)
        {
            Instantiate(bullet, muzzle.transform.position + new Vector3(-0.5f, 0.1f, 0), Quaternion.identity)
               .GetComponent<Projectile>().SetTarget(player.position);
        }
    }
}
