using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public PlayerManager playerManager;
    public int damage = 15;
    
    private bool hitPlayer = false;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (playerManager.currentHealth <= 0)
        {
            ReturnToStart();
        }
    }

    private IEnumerator AttackPlayer()
    {
        while (hitPlayer)
        {
            playerManager.TakeDamage(damage);
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            StartCoroutine(AttackPlayer());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = false;
            StopCoroutine(AttackPlayer());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
            StartCoroutine(AttackPlayer());
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPlayer = false;
            StopCoroutine(AttackPlayer());
        }
    }

    private void ReturnToStart()
    {
        transform.position = startPosition;
    }
}
