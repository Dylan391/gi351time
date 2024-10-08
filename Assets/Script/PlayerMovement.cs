using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    public float jumpForce = 15f; // แรงกระโดด
    public float runSpeed = 10f;
    public float fallMultiplier = 5f;
    public float climbSpeed = 5f;
    public Inventory inventory;
    public InventoryUI inventoryUI;
    public Slider energyBar;
    public float maxEnergy = 100f; // พลังงานสูงสุด
    public float currentEnergy; // พลังงานปัจจุบัน
    public float energyConsumptionRate = 1f; // อัตราการลดพลังงานต่อวินาที
    public float energyRecoveryRate = 5f; // อัตราการเพิ่มพลังงานต่อวินาที
    public Animator animator;
    public AudioClip jumpSound;
    public AudioClip runSound;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isBox;
    private bool isPushOrPull;
    private bool isClimbing;
    private bool isClimb;
    private bool isPlatform;
    private GameObject currentBox;
    private Collider2D currentWall;
    private BoxCollider2D playerCollider;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        energyBar.value = maxEnergy;
        currentEnergy = maxEnergy;
        playerCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Jump();
        PushOrPullBox();
        Climb();
        ApplyFallMultiplier();
        CheckForItemPickup();
        DescendFromPlatform();
        
        // ฟื้นฟูพลังงานเมื่อไม่วิ่ง
        if (!Input.GetKey(KeyCode.LeftShift) && currentEnergy < maxEnergy)
        {
            currentEnergy += energyRecoveryRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy); // ไม่ให้เกินค่ามากสุด
        }

        // อัปเดต EnergyBar
        if (currentEnergy < maxEnergy)
        {
            energyBar.value = currentEnergy / maxEnergy;
        }
        
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isWalk", true);
            animator.SetBool("isIdle", false);
        }
        
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
        }
        
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);
        }

        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);
        }
        
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJump", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isJump", false);
        }
        
        else if (isClimbing)
        {
            animator.SetBool("isClimb", true);
            animator.SetBool("isIdle", false);
        }
        else if (!isClimbing)
        {
            animator.SetBool("isClimb", false);
            animator.SetBool("isIdle", true);
        }
        
        

        // ซ่อน EnergyBar เมื่อไม่วิ่ง
        energyBar.gameObject.SetActive(currentEnergy < maxEnergy || Input.GetKey(KeyCode.LeftShift));
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 ? runSpeed : moveSpeed;

        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y); // เคลื่อนที่

        // ลดพลังงานเมื่อวิ่ง
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && !isClimbing)
        {
            currentEnergy -= energyConsumptionRate * Time.deltaTime;
            currentEnergy = Mathf.Max(currentEnergy, 0); // ไม่ให้ต่ำกว่า 0
        }
        
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
        
        if (moveInput != 0 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(runSound);
        }
    }

    private void Jump()
    {
        bool jumpCondition = Input.GetKey(KeyCode.Space) && !isPushOrPull;

        if (jumpCondition && (isGrounded || isBox || isClimb || isPlatform))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpSound);
        }
    }
    
    private void ApplyFallMultiplier()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    private void Climb()
    {
        if (isClimbing)
        {
            float vericalInput = Input.GetAxis("Vertical");
            
            rb.velocity = new Vector2(0, vericalInput * climbSpeed);

            if (Input.GetButtonUp("Vertical") || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                isClimbing = false; // ออกจากการปีน
                rb.velocity = new Vector2(rb.velocity.x, 0); // หยุดการเคลื่อนที่ในแนวตั้ง
            }
        }
    }

    private void PushOrPullBox()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (currentBox != null)
            {
                Rigidbody2D boxRb = currentBox.GetComponent<Rigidbody2D>();

                if (boxRb != null)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        currentBox.transform.position = transform.position + Vector3.right * 1f;
                        isPushOrPull = true;
                        animator.SetBool("isIdle", true);
                        animator.SetBool("isJump", false);
                        animator.SetBool("isRun", false);
                        animator.SetBool("isWalk", false);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        currentBox.transform.position = transform.position + Vector3.left * 1f;
                        isPushOrPull = true;
                        animator.SetBool("isIdle", true);
                        animator.SetBool("isJump", false);
                        animator.SetBool("isRun", false);
                        animator.SetBool("isWalk", false);
                    }
                }
            }
        }
    }

    private void DescendFromPlatform()
    {
        if (Input.GetKeyDown(KeyCode.S) && isPlatform && !isGrounded)
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.S) || isGrounded)
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = true;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = true;
            currentWall = other;
        }

        if (other.CompareTag("Ground"))
        {
            if (playerCollider != null && playerCollider.enabled == false)
            {
                playerCollider.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable")) // ออกจากกำแพงที่สามารถปีนได้
        {
            isClimbing = false;
            rb.velocity = new Vector2(rb.velocity.x, 0); // หยุดการเคลื่อนที่ในแนวตั้ง
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // ตรวจสอบการชนกับพื้น
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("Pushable")) // เช็คว่าติดต่อกับกล่อง
        {
            currentBox = collision.gameObject; // เก็บกล่องที่อยู่ใกล้
            isBox = true;
        }

        if (collision.gameObject.CompareTag("Climbable"))
        {
            isClimb = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // ออกจากพื้น
        }
        
        if (collision.gameObject.CompareTag("Pushable")) // เมื่อออกจากกล่อง
        {
            currentBox = null; // ล้างกล่อง
            isBox = false;
            isPushOrPull = false;
        }
        
        if (collision.gameObject.CompareTag("Climbable"))
        {
            isClimb = false;
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isPlatform = false;
            //playerCollider.enabled = true;
        }
    }
    
    void CheckForItemPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // เช็คว่ามีไอเท็มอยู่ใกล้เคียงหรือไม่
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (var hitCollider in hitColliders)
            {
                ItemPickup itemPickup = hitCollider.GetComponent<ItemPickup>();
                if (itemPickup != null)
                {
                    // ลองเพิ่มไอเท็มลงใน Inventory
                    bool addedSuccessfully = inventory.AddItem(itemPickup.itemKey, itemPickup.item);
                    if (addedSuccessfully)
                    {
                        Destroy(hitCollider.gameObject); // ลบไอเท็มจาก Scene
                        inventoryUI.UpdateInventoryUI();
                    }
                    else
                    {
                        Debug.Log($"Cannot add {itemPickup.item.itemName}. Inventory limit reached.");
                    }
                    break;
                }
            }
        }
    }
}
