using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    
    private bool doorOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        if (!doorOpen)
        {
            doorOpen = true;
            door.gameObject.SetActive(false);
        }
    }

    private void CloseDoor()
    {
        if (doorOpen)
        {
            doorOpen = false;
            door.gameObject.SetActive(true);
        }
    }
}
