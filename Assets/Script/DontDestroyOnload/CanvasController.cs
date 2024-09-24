using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ทำให้ไม่ถูกทำลายเมื่อเปลี่ยน Scene
        }
        else
        {
            Destroy(gameObject); // ทำลาย Instance ที่ซ้ำกัน
        }
    }
}
