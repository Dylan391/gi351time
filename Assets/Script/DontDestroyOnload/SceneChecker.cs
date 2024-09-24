using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : MonoBehaviour
{
    private string currentScene;
    
    void Start()
    {
        // เรียกใช้งานฟังก์ชันเพื่อเช็คชื่อ Scene
        CheckCurrentScene();
    }

    void CheckCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    
    public bool IsInGame1()
    {
        return currentScene == "Game 1"; // เช็คว่าอยู่ใน "Game 1"
    }

    public bool IsInGame2()
    {
        return currentScene == "Game 2"; // เช็คว่าอยู่ใน "Game 2"
    }
}
