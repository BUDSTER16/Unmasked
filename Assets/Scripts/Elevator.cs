using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private CamMovement playerCamera;
    private AudioSource audioSource;
    private bool cooldown = false;

    private void Start()
    {
        playerCamera = FindFirstObjectByType<CamMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && !cooldown)
            {
                destination.GetComponent<AudioSource>().Play();
                collision.gameObject.transform.position = destination.transform.position;
                playerCamera.ReCenter();
                destination.GetComponent<Elevator>().StartCD();
            }
        }
    }

    public void PlayBeep()
    {
        audioSource.Play();
    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }

    public void StartCD()
    {
        StartCoroutine(Cooldown());
    }
}
