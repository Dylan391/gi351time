using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ObjectUsedDOO : MonoBehaviour
{
    public List<GameObject> ObjectsG1;
    public List<GameObject> ObjectsG2;
    
    private static ObjectUsedDOO instance;
    
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

        if (currentScene == "Game 1")
        {
            // เปิด Enemy ทั้งหมด
            foreach (GameObject enemy in ObjectsG1)
            {
                enemy.SetActive(true);
            }
            foreach (GameObject enemy in ObjectsG2)
            {
                enemy.SetActive(false);
            }
            //Debug.Log("Enemies are activated in Game 1");
        }
        else if (currentScene == "Game 2")
        {
            // ปิด Enemy ทั้งหมด
            foreach (GameObject Object in ObjectsG1)
            {
                Object.SetActive(false);
            }
            foreach (GameObject Object in ObjectsG2)
            {
                Object.SetActive(true);
            }
            //Debug.Log("Enemies are deactivated in Game 2");
        }
    }
}
