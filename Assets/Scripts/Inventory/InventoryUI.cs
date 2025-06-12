using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel; // Panel to hold the inventory UI
    [SerializeField] private InventorySlot slotPrefab; // Prefab for the inventory slot UI element
    [SerializeField] private Transform container; // Container to hold the inventory slots

    public InventorySlot CurrentSlot { get; set; } // Currently selected slot


    private List<InventorySlot> slotsList = new List<InventorySlot>(); // List to hold the created inventory slots 

    private void Start()
    {
        InitInventory(); // Initialize the inventory UI
    }
    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++) // Loop through the inventory size
        {
            InventorySlot slot = Instantiate(slotPrefab, container); // Instantiate the slot prefab in the container
            slot.Index = i; // Set the index of the slot
            slotsList.Add(slot); // Add the slot to the list

        }
    }

    public void UseItem() // Method to use an item from the currently selected slot
    {
        Inventory.Instance.UseItem(CurrentSlot.Index); // Use the item in the current slot
    }

    public void DrawItem(InventoryItem item, int index) // Method to draw an item in the inventory UI at a specific index
    {
        InventorySlot slot = slotsList[index]; // Get the slot at the specified index
        if (item == null)
        {
            slot.ShowSlotInformation(false); // Hide the slot information if the item is null
            return;
        }
        slot.ShowSlotInformation(true); // Show the slot information
        slot.UpdateSlot(item); // Update the slot with the item information
    }

    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotsList[slotIndex]; // Set the currently selected slot based on the index
    }

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf); // Toggle the inventory panel visibility
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallback; // Subscribe to the slot selected event
    }

    private void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallback; // Unsubscribe from the slot selected event
    }


}
