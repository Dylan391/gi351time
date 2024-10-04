using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezeWall : MonoBehaviour
{
    public Transform wall;
    public float squeezeSpeed;
    public float squeezeDistance;
    
    private Vector3 startPosition;
    private bool playerInSqueeze;
    void Start()
    {
        startPosition = wall.position;
    }
    
    void Update()
    {
        if (playerInSqueeze)
        {
            wall.position = Vector3.MoveTowards(wall.position, wall.position + Vector3.left * squeezeDistance, squeezeSpeed * Time.deltaTime);
        }
        else
        {
            wall.position = Vector3.MoveTowards(wall.position, startPosition, squeezeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSqueeze = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSqueeze = false;
        }
    }
}
