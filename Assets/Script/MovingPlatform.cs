using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่
    public float moveDistance = 3f; // ระยะทางที่เลื่อนไป
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0); // ตั้งค่าตำแหน่งเป้าหมาย
        journeyLength = Vector3.Distance(startPosition, targetPosition); // คำนวณระยะทาง
        startTime = Time.time; // บันทึกเวลาที่เริ่ม
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * moveSpeed; // ระยะทางที่เคลื่อนที่ไป
        float fractionOfJourney = distCovered / journeyLength; // สัดส่วนของระยะทางที่เคลื่อนที่

        // สลับระหว่างตำแหน่งเริ่มต้นและตำแหน่งเป้าหมาย
        if (fractionOfJourney >= 1f)
        {
            // สลับตำแหน่ง
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp;

            // รีเซ็ตเวลา
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, targetPosition); // คำนวณระยะทางใหม่
        }

        // เคลื่อนที่แพลตฟอร์ม
        transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
    }
}
