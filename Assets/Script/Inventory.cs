using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, Item> items = new Dictionary<string, Item>();

    public bool AddItem(string key, Item item)
    {
        if (items.ContainsKey(key))
        {
            // เช็คว่าจำนวนไอเท็มใน Inventory มีมากกว่า 6 หรือไม่
            if (items[key].quantity + item.quantity > 10)
            {
                Debug.Log($"Cannot add {item.itemName}. Maximum quantity is 10.");
                return false; // ไม่เพิ่มไอเท็ม
            }
        
            // ถ้าไอเท็มมีอยู่แล้ว เพิ่มจำนวนขึ้น
            items[key].quantity += item.quantity;
            Debug.Log($"Increased quantity of {item.itemName} to {items[key].quantity}.");
            return true; // เพิ่มไอเท็มสำเร็จ
        }
        else
        {
            // เช็คว่ามีจำนวนของไอเท็มใน Inventory อยู่แล้วหรือไม่
            if (items.Count >= 6)
            {
                Debug.Log($"Cannot add {item.itemName}. Maximum item types is 6.");
                return false; // ไม่เพิ่มไอเท็ม
            }
        
            // ถ้าไอเท็มยังไม่มีใน Inventory ให้เพิ่มใหม่
            items.Add(key, item);
            Debug.Log($"Added {item.itemName} to inventory.");
            return true; // เพิ่มไอเท็มสำเร็จ
        }
    }

    public void RemoveItem(string key, int qty)
    {
        if (items.ContainsKey(key))
        {
            if (items[key].quantity > qty)
            {
                // ถ้าจำนวนมากกว่า qty ให้ลดจำนวนลง
                items[key].quantity -= qty;
                Debug.Log($"Decreased quantity of {key} to {items[key].quantity}.");
            }
            else if (items[key].quantity <= qty)
            {
                // ถ้าจำนวนเท่ากับ qty ให้ลบไอเท็มออกจาก Inventory
                items.Remove(key);
                Debug.Log($"Removed {key} from inventory.");
            }
            else
            {
                Debug.Log($"Not enough quantity of {key} to remove.");
            }
        }
        else
        {
            Debug.Log($"Item {key} not found in inventory.");
        }
    }

    public Item GetItem(string key)
    {
        if (items.TryGetValue(key, out Item item))
        {
            return item;
        }
        else
        {
            Debug.Log($"Item {key} not found in inventory.");
            return null;
        }
    }
    
    public Dictionary<string, Item> GetItems()
    {
        return items;
    }

    public void DisplayInventory()
    {
        foreach (var kvp in items)
        {
            Debug.Log($"Item: {kvp.Value.itemName}, Quantity: {kvp.Value.quantity}");
        }
    }
    
    public bool UseItem(string key)
    {
        if (items.ContainsKey(key))
        {
            if (items[key].isUsable) // ตรวจสอบว่าไอเท็มนี้ใช้ได้หรือไม่
            {
                RemoveItem(key, 1); // ลดจำนวนไอเท็มลง 1 ชิ้น
                Debug.Log($"Used 1 {key}.");
                return true; // ใช้ไอเท็มสำเร็จ
            }
            else
            {
                Debug.Log($"Item {key} cannot be used.");
                return false; // ไอเท็มไม่สามารถใช้ได้
            }
        }
        else
        {
            Debug.Log($"Item {key} not found in inventory.");
            return false; // ไม่พบไอเท็ม
        }
    }
}
