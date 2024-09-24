using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChecker : MonoBehaviour
{
    public List<GameObject> enemies; // กำหนด List สำหรับเก็บ GameObject ที่เป็น Enemy

    void Start()
    {
        // เรียกฟังก์ชันเพื่อตรวจสอบ Scene และตั้งสถานะ Enemy
        ManageEnemies();
    }

    void ManageEnemies()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Game 1")
        {
            // เปิด Enemy ทั้งหมด
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
            //Debug.Log("Enemies are activated in Game 1");
        }
        else if (currentScene == "Game 2")
        {
            // ปิด Enemy ทั้งหมด
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
            //Debug.Log("Enemies are deactivated in Game 2");
        }
    }
}
