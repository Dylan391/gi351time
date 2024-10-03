using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlatform : MonoBehaviour
{
    public float fallDelay = 3f;
    public float respawnDelay = 15f;
    
    private bool isOnStone = false;
    private float stoneTimer = 0f;
    private bool stoneActive = false;
    
    void Update()
    {
        if (isOnStone)
        {
            stoneTimer += Time.deltaTime;

            if (stoneTimer >= fallDelay)
            {
                isOnStone = false;
                stoneTimer = 0f;
                gameObject.SetActive(false);
                Invoke("Respawn", respawnDelay);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOnStone = true;
            stoneTimer = 0f;
        }
    }
    
    private void Respawn()
    {
        gameObject.SetActive(true);
        isOnStone = false;
        stoneTimer = 0f;
    }
}
