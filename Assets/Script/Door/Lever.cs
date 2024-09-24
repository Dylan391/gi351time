using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    
    private bool isActivated = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        isActivated = !isActivated;
        if (isActivated)
        {
            door.gameObject.SetActive(false);
        }
        /*else
        {
            door.gameObject.SetActive(true);
        }*/
    }
}
