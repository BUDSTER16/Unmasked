using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : Enemy
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;

    private float attackTimer = 3f;
    private float attackCD = 3f;
    
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            attackTimer = attackCD;
            Attack();
        }
    }

    private void Attack()
    {
        Instantiate(bullet, muzzle.transform.position + new Vector3(-0.5f, 0.1f, 0), Quaternion.identity)
            .GetComponent<Projectile>().SetTarget(FindFirstObjectByType<ActionPlayer>().gameObject.transform.position);
    }
}
