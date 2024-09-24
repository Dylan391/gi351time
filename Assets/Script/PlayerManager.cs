using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int lives = 3;
    public Slider healthSlider;
    public Text livesText;
    
    private static PlayerManager instance;
    private string currentScene;
    private bool isLoading = false;
    private int currentHealth;
    private Vector3 savePoint;

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
    
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        savePoint = transform.position;
        UpdateUI();
    }
    
    void Update()
    {
        CheckCurrentScene(); // เช็ค scene ทุกครั้งใน Update
        SomeFunction();
        if (Input.GetKey(KeyCode.X))
        {
            TakeDamage(1);
        }
    }

    private void CheckCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene().name; // รับชื่อ scene ปัจจุบัน
    }

    public bool IsInGame1()
    {
        return currentScene == "Game 1"; // เช็คว่าอยู่ใน "Game 1"
    }

    public bool IsInGame2()
    {
        return currentScene == "Game 2"; // เช็คว่าอยู่ใน "Game 2"
    }
    
    void SomeFunction()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
    
        if (playerManager.IsInGame1() && !isLoading)
        {
            if (Input.GetKey(KeyCode.G))
            {
                StartCoroutine(LoadSceneWithDelay(1, 1f)); // หน่วงเวลา 1 วินาที
            }
        }
        else if (playerManager.IsInGame2())
        {
            if (Input.GetKey(KeyCode.G) && !isLoading)
            {
                StartCoroutine(LoadSceneWithDelay(0, 1f)); // หน่วงเวลา 1 วินาที
            }
        }
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        isLoading = true;
        yield return new WaitForSeconds(delay); // หน่วงเวลาตามที่กำหนด
        SceneManager.LoadSceneAsync(sceneIndex); // เปลี่ยน scene
        isLoading = false;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            LoseLife();
        }
        UpdateUI();
    }

    void LoseLife()
    {
        lives--;
        if (lives > 0)
        {
            Respawn();
        }
        else
        {
            Debug.Log("Game Over");
        }
        UpdateUI();
    }

    void Respawn()
    {
        transform.position = savePoint;
        currentHealth = maxHealth;
        UpdateUI();
    }

    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        healthSlider.value = currentHealth; // อัปเดตค่า Slider
    }

    public void SetSavePoint(Vector3 position)
    {
        savePoint = position;
        Debug.Log("Save point set at: " + savePoint);
    }
}
