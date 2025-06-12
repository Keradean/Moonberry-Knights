using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI; 

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize; // Maximum number of items in the inventory
    [SerializeField] private InventoryItem[] inventoryItems; // Array to hold the items in the inventory

    [Header("Testing")]
    public InventoryItem testItem; // Test item for demonstration purposes

    public int InventorySize => inventorySize; // Property to get the maximum inventory size 

    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize]; // Initialize the inventory items array with the maximum size
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            inventoryItems[0] = testItem.CopyItem(); // Copy the test item into the first slot of the inventory
            inventoryItems[0].Quantity = 10; // Set the quantity of the item
            InventoryUI.Instance.DrawItem(inventoryItems[0], 0); // Draw the item in the inventory UI   
        }
    }
}
