using BayatGames.SaveGameFree; // Importing the SaveGame namespace for saving functionality
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// The Inventory class manages the player's inventory as a Singleton.
public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private GameContent gameContent; // Reference to the GameContent scriptable object containing available items
    [SerializeField] private int inventorySize; // Maximum number of items in the inventory
    [SerializeField] private InventoryItem[] inventoryItems; // Array to hold the items in the inventory

    [Header("Testing")]
    public InventoryItem testItem; // Test item for demonstration purposes
    public int InventorySize => inventorySize; // Public property to access the inventory size
    public InventoryItem[] InventoryItems => inventoryItems; // Public property to access the inventory items

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY"; // Key for saving the inventory data

    // Initializes the inventory array with the specified size
    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        LoadInventory();
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
                    SaveInventory(); // Save the inventory state after adding the item
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

        SaveInventory(); // Save the inventory state after adding the item
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return; // Check if the item exists in the inventory
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index); // Decrease the stack of the item 
        }

        SaveInventory(); // Save the inventory state after using an item

    }

    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return; // Check if the item exists in the inventory
        inventoryItems[index].RemoveItem(); // Remove the item from the inventory
        inventoryItems[index] = null; // Set the slot to null
        InventoryUI.Instance.DrawItem(null, index); // Update the UI to reflect the change

        SaveInventory(); // Save the inventory state after removing an item
    }

    /* public void EquipItem()
     {
         if (inventoryItems[index] ==null) return; // Check if the item exists in the inventory
         if (inventoryItems[index].ItemType != ItemType.Weapon) return; // Check if the item is a weapon
         inventoryItems[Index].EquipItem(); // Call the EquipItem method on the item 
     }
     */
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

    private InventoryItem ItemExistsInGameContent(string itemID)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i]; // Return the item if it exists in the game content
            }
        }
        return null; // Return null if the item does not exist in the game content
    }

    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA); // Load the inventory data from the save file
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.ItemContent[i] == null)
                {
                    inventoryItems[i] = null; // If the slot is empty, set it to null
                }
                else
                {
                    InventoryItem item = ItemExistsInGameContent(loadData.ItemContent[i]); // Check if the item exists in the game content
                    if (item != null)
                    {
                        inventoryItems[i] = item.CopyItem(); // Create a copy of the item
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i]; // Set the quantity of the item
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i); // Draw the item in the UI
                    }
                    else
                    {
                        inventoryItems[i] = null;
                    }
                }
            }
        }
    }

    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData(); // Create a new InventoryData object to save the inventory state
        saveData.ItemContent = new string[inventorySize]; // Initialize the ItemContent array
        saveData.ItemQuantity = new int[inventorySize]; // Initialize the ItemQuantity array
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null) 
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }

            else
            {
                saveData.ItemContent[i] = inventoryItems[i].ID; 
                saveData.ItemQuantity[i] = inventoryItems[i].Quantity;    
            }
        }

        SaveGame.Save(INVENTORY_KEY_DATA, saveData); // Save the inventory data using the SaveGame system
    }
}
