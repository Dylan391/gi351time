using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPlate : MonoBehaviour
{
    private bool isActive = false; // ตรวจสอบว่าพัดลมทำงานอยู่หรือไม่

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable")) // ตรวจสอบว่าชนกับกล่อง
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            isActive = false;
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
}
