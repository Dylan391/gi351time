using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyDDO : MonoBehaviour
{
    public List<GameObject> enemiesG1;
    public List<GameObject> enemiesG2;
    
    private static EnemyDDO instance;
    
    void Awake()
    {
        // ตรวจสอบว่ามี PlayerManager อยู่แล้วหรือไม่
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ทำให้ GameObject นี้ไม่หายไป
        }
        else
        {
            Destroy(gameObject); // หากมีอยู่แล้ว ให้ลบตัวเอง
        }
    }

    private void Update()
    {
        
        ManageEnemies();
    }

    void ManageEnemies()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Game1")
        {
            foreach (GameObject enemy in enemiesG1)
            {
                enemy.SetActive(true);
            }
            foreach (GameObject enemy in enemiesG2)
            {
                enemy.SetActive(false);
            }
            //Debug.Log("Enemies are activated in Game 1");
        }
        else if (currentScene == "Game2")
        {
            // ปิด Enemy ทั้งหมด
            foreach (GameObject enemy in enemiesG1)
            {
                enemy.SetActive(false);
            }
            foreach (GameObject enemy in enemiesG2)
            {
                enemy.SetActive(true);
            }
            //Debug.Log("Enemies are deactivated in Game 2");
        }
    }
}
