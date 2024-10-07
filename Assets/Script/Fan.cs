using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float jumpForce = 10f; // แรงกระโดดที่เพิ่ม
    public FanPlate plate; // อ้างอิงถึง Plate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Game1")
        {
            
        }
        else if (currentScene == "Game2")
        {
            if (collision.CompareTag("Player") && plate.IsActive()) // ตรวจสอบว่าชนกับผู้เล่นและ Plate ทำงาน
            {
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce); // เพิ่มแรงกระโดด
                }
            }
        }
    }
}
