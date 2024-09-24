using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRadius = 5f;
    public Transform player;
    public float patrolDistance = 10f;
    public float patrolSpeed = 2f;
    public PlayerManager playerManager;
    public int damage = 15;

    private Vector2 startingPosition;
    private bool isChasing = false;
    private float patrolDirection = 1f;
    private bool hitPlayer = false;
    private bool hitClimbable = false;
    private bool hitWall = false;

    void Start()
    {
        startingPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // ตรวจสอบว่าผู้เล่นอยู่ด้านหน้าและศัตรูหันหน้าไปทางผู้เล่น
        bool isPlayerInFront = (player.position.x > transform.position.x && transform.localScale.x > 0) ||
                               (player.position.x < transform.position.x && transform.localScale.x < 0);
        
        bool isPlayerBehind = (player.position.x < transform.position.x && transform.localScale.x > 0) ||
                              (player.position.x > transform.position.x && transform.localScale.x < 0);

        if (distanceToPlayer < detectionRadius && (isPlayerInFront || isPlayerBehind))
        {
            // ถ้าอยู่ในระยะการตรวจจับและอยู่ด้านหน้าศัตรู ให้ไล่ตาม Player
            isChasing = true;
        }
        else if (isChasing && (distanceToPlayer > detectionRadius || !isPlayerInFront))
        {
            // ถ้า Player ออกห่างจากระยะไล่หรือไม่อยู่ด้านหน้า ให้กลับไปตำแหน่งเริ่มต้น
            isChasing = false;
            ReturnToStartingPosition();
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (hitClimbable || hitWall)
        {
            patrolDirection *= -1; // เปลี่ยนทิศทาง
        }
        
        transform.position += new Vector3(patrolDirection * patrolSpeed * Time.deltaTime, 0, 0);
            
        transform.localScale = new Vector3(patrolDirection, 1, 1);
        
        // เช็คว่าถึงขอบเขตที่กำหนดหรือยัง
        /*if (Mathf.Abs(transform.position.x - startingPosition.x) >= patrolDistance)
        {
            patrolDirection *= -1; // เปลี่ยนทิศทาง
        }*/
    }
    
    private bool IsObstacleInFront()
    {
        // ใช้ Raycast เพื่อตรวจสอบการชน
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * patrolDirection, 0.5f); // 0.5f คือระยะที่ตรวจสอบ
        return hit.collider != null && (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Climbable")); // เปลี่ยน Tag ตามที่คุณใช้
    }
    
    private bool IsGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        return hit.collider != null && (hit.collider.CompareTag("Ground")|| hit.collider.CompareTag("Untagged")); // เปลี่ยน Tag ตามที่คุณใช้
    }

    void ChasePlayer()
    {
        // ไล่ตาม Player
        Vector2 newPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        
        // ให้ศัตรูหันหน้าไปทางผู้เล่น
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // หันหน้าไปทางซ้าย
        else
            transform.localScale = new Vector3(1, 1, 1); // หันหน้าไปทางขว
    }
    
    void ReturnToStartingPosition()
    {
        if (Vector2.Distance(transform.position, startingPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // เมื่อกลับถึงจุดเริ่มต้นแล้วให้เริ่มเดินไปซ้ายและขวา
            patrolDirection = 1f; // ตั้งค่าให้กลับมาเดินไปทางขวา
        }
    }
    
    private void OnDrawGizmos()
    {
        // วาดแสงสีแดงข้างหน้า
        Vector3 frontPosition = transform.position + transform.localScale * 1.5f; // ปรับระยะห่างตามต้องการ
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(frontPosition, 0.2f); // แสดงแสงที่จุดนั้น
    }
    
    private IEnumerator AttackPlayer()
    {
        while (hitPlayer) // ทำการโจมตีทุก 3 วินาที
        {
            playerManager.TakeDamage(damage);
            yield return new WaitForSeconds(3f);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // หยุดการเคลื่อนที่ของศัตรูเมื่อชนกับผู้เล่น
            isChasing = false; // หยุดการไล่ตาม
            patrolDirection = 0; // หยุดการเดิน
            hitPlayer = true;
            StartCoroutine(AttackPlayer());
        }

        if (collision.gameObject.CompareTag("Climbable"))
        {
            hitClimbable = true;
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            hitClimbable = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = true;
            patrolDirection = 1;
            hitPlayer = false;
            StopCoroutine(AttackPlayer());
        }
        
        if (collision.gameObject.CompareTag("Climbable"))
        {
            hitClimbable = false;
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            hitClimbable = false;
        }
    }
}
