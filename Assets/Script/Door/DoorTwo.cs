using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTwo : MonoBehaviour
{
    public GameObject door;
    public float doorSpeed = 1f;
    
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool doorOpen = true;

    private void Start()
    {
        closedPosition = door.transform.position;
        openPosition = closedPosition + new Vector3(0, 6f, 0);
    }

    private void Update()
    {
        if (!doorOpen)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, closedPosition, doorSpeed * Time.deltaTime);
        }
        else
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, openPosition, doorSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        if (doorOpen)
        {
            doorOpen = false;
        }
    }
}
