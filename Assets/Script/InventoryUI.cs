using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // อ้างอิงถึง Inventory
    public GameObject itemSlotPrefab; // Prefab สำหรับแสดงไอเท็ม
    public Transform itemsParent; // ตำแหน่งสำหรับจัดเรียง Item Slots
    public GameObject inventoryPanel; // Panel สำหรับแสดง Inventory
    
    private List<string> itemIds = new List<string>();

    void Start()
    {
        inventoryPanel.SetActive(false); // ซ่อน UI เมื่อเริ่มเกม
        UpdateInventoryUI();
    }
    
    void Update()
    {
        // ตรวจสอบการกดปุ่ม Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        
        for (int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
            {
                UseItemFromInventory(i);
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf); // เปลี่ยนสถานะการแสดงผล
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI(); // อัปเดต UI ทุกครั้งที่เปิด
        }
    }

    public void UpdateInventoryUI()
    {
        /*if (itemSlotPrefab == null || itemsParent == null)
        {
            Debug.LogError("One or more references are not set in InventoryUI.");
            return;
        }
        
        var items = inventory.GetItems();
        if (items == null || items.Count == 0)
        {
            Debug.Log("Inventory is empty.");
            return; // ถ้าไม่มีไอเท็มใน Inventory ให้กลับ
        }

        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var kvp in inventory.GetItems())
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemsParent);
            slot.GetComponent<Image>().sprite = kvp.Value.itemIcon; // ตั้งค่าไอคอน
            slot.GetComponentInChildren<TextMeshProUGUI>().text = kvp.Value.itemName + " x" + kvp.Value.quantity; // ตั้งชื่อ 
        }*/
        
        if (itemSlotPrefab == null || itemsParent == null)
        {
            Debug.LogError("One or more references are not set in InventoryUI.");
            return;
        }
    
        var items = inventory.GetItems();
        if (items == null || items.Count == 0)
        {
            Debug.Log("Inventory is empty.");
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }
        
            return; // ถ้าไม่มีไอเท็มใน Inventory ให้กลับ
        }

        // ลบ item slots ที่มีอยู่
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        itemIds.Clear(); // ล้างรายการ item IDs

        int index = 0;
        foreach (var kvp in items)
        {
            if (kvp.Value.quantity > 0) // เช็คว่าจำนวนไอเท็มมากกว่า 0 หรือไม่
            {
                GameObject slot = Instantiate(itemSlotPrefab, itemsParent);
                slot.GetComponent<Image>().sprite = kvp.Value.itemIcon; // ตั้งค่าไอคอน
                slot.GetComponentInChildren<TextMeshProUGUI>().text = kvp.Value.itemName + " x" + kvp.Value.quantity; // ตั้งชื่อ
                itemIds.Add(kvp.Key); // เก็บ item ID สำหรับใช้
                index++;
            }
        }
    }
    
    private void UseItemFromInventory(int index)
    {
        if (index < itemIds.Count)
        {
            string itemId = itemIds[index];
            bool itemUsed = inventory.UseItem(itemId); // ใช้ไอเท็มตาม ID
        
            if (itemUsed)
            {
                UpdateInventoryUI(); // อัปเดต UI หลังจากใช้ไอเท็ม
            }
            else
            {
                Debug.Log($"Cannot use {itemId}."); // แจ้งว่าไม่สามารถใช้ไอเท็ม
            }
        }
        else
        {
            Debug.Log("ไม่มีไอเท็มให้ใช้ในช่องนี้.");
        }
    }
}
