using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlatform : MonoBehaviour
{
    public float fallDelay = 3f;
    public float respawnDelay = 15f;
    public AudioClip sSound;
    
    private bool isOnStone = false;
    private float stoneTimer = 0f;
    private bool stoneActive = false;
    private AudioSource stoneSound;
    private bool stoneSoundActive = true;

    private void Awake()
    {
        stoneSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOnStone)
        {
            stoneTimer += Time.deltaTime;
            if (stoneSoundActive)
            {
                stoneSound.PlayOneShot(sSound);
                stoneSoundActive = false;
            }

            if (stoneTimer >= fallDelay)
            {
                isOnStone = false;
                stoneSoundActive = true;
                stoneTimer = 0f;
                stoneSound.Stop();
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
