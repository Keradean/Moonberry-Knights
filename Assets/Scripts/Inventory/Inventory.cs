using System.Collections.Generic;
using UnityEngine;

// The Inventory class manages the player's inventory as a Singleton.
public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize; // Maximum number of items in the inventory
    [SerializeField] private InventoryItem[] inventoryItems; // Array to hold the items in the inventory

    [Header("Testing")]
    public InventoryItem testItem; // Test item for demonstration purposes

    // Public property to get the maximum inventory size
    public int InventorySize => inventorySize;

    // Initializes the inventory array with the specified size
    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw(); // Verify items for drawing in the UI
    }

    // For testing: Adds a test item to the inventory when 'H' is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 1);
        }
    }

    // Adds an item to the inventory, stacking if possible
    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;
        List<int> itemIndexes = CheckItemStock(item.ID);
        if (item.isStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int dif = inventoryItems[index].Quantity - maxStack;
                        AddItem(item, dif); // Add the remaining amount recursively
                    }
                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    return;
                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity; // Calculate the amount to add
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);

        }
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return; // Check if the item exists in the inventory
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index); // Decrease the stack of the item 
        }

    }

    // Finds a free slot and adds the item there
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    private void DecreaseItemStack(int index)
    {
        inventoryItems[index].Quantity--; // Decrease the quantity of the item at the specified index
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index].RemoveItem(); // Remove the item if its quantity is zero
            inventoryItems[index] = null; // Set the slot to null
            InventoryUI.Instance.DrawItem(null, index); // Update the UI to reflect the change
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index); // Update the UI with the new quantity
        }
    }

    // Checks if an item with a specific ID is already in the inventory and returns the indices
    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }

    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i); // If the slot is empty, draw it as null
            }
        }
    }
}
