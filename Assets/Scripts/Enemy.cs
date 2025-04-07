using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int HP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player Projectile")
        {
            HP -= collision.GetComponent<Projectile>().hit();

            if(HP <= 0)
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
        Destroy(gameObject);
    }
}
