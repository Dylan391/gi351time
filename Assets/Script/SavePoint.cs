using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Vector3 savePosition;

    void Awake()
    {
        savePosition = transform.position;
    }
    
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
