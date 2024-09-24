using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Vector3 savePosition;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerHealth = other.GetComponent<PlayerManager>();
            if (playerHealth != null)
            {
                playerHealth.SetSavePoint(savePosition);
            }
            Debug.Log(savePosition);
        }
    }*/
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager playerHealth = collision.gameObject.GetComponent<PlayerManager>();
            if (playerHealth != null)
            {
                playerHealth.SetSavePoint(savePosition);
            }
            Debug.Log(savePosition);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager playerHealth = collision.gameObject.GetComponent<PlayerManager>();
            if (playerHealth != null)
            {
                playerHealth.SetSavePoint(savePosition);
            }
            Debug.Log(savePosition);
        }*/
    }
}
