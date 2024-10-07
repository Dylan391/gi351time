using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlatformDDO : MonoBehaviour
{
    private static StonePlatformDDO instance;
    
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
}
