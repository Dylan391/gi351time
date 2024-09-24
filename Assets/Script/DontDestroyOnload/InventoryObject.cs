using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public static InventoryObject Instance;

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
