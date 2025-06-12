using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private InventorySlot slotPrefab; // Prefab for the inventory slot UI element
    [SerializeField] private Transform container; // Container to hold the inventory slots

    private List<InventorySlot> slotsList = new List<InventorySlot>(); // List to hold the created inventory slots 

    private void Start()
    {
       InitInventory(); // Initialize the inventory UI
    }
    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container); // Instantiate the slot prefab in the container
            slot.Index = i; // Set the index of the slot
            slotsList.Add(slot); // Add the slot to the list

        }
    }
    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotsList[index]; // Get the slot at the specified index
        slot.ShowSlotInformation(true); // Show the slot information
        slot.UpdateSlot(item); // Update the slot with the item information+




    }


}
