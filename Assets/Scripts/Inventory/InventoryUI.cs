using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel; // Panel to hold the inventory UI
    [SerializeField] private InventorySlot slotPrefab; // Prefab for the inventory slot UI element
    [SerializeField] private Transform container; // Container to hold the inventory slots

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;  // Panel to display item description
    [SerializeField] private Image itemIcon; // Image to display the item icon in the description panel
    [SerializeField] private TextMeshProUGUI itemNameTMP; // Text element to display the item name in the description panel
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP; // Text element to display the item description in the description panel

    public InventorySlot CurrentSlot { get; set; } // Currently selected slot


    private List<InventorySlot> slotsList = new List<InventorySlot>(); // List to hold the created inventory slots 

    protected override void Awake()
    {
        base.Awake(); // Call the base class Awake method
        InitInventory(); // Initialize the inventory UI
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++) // Loop through the inventory size
        {
            InventorySlot slot = Instantiate(slotPrefab, container); // Instantiate the slot prefab in the container
            slot.Index = i; // Set the index of the slot
            slotsList.Add(slot); // Add the slot to the list
            slot.ShowSlotInformation(false); // Slot-UI direkt nach dem Erstellen leeren
        }
    }

    public void UseItem() // Method to use an item from the currently selected slot
    {
        Inventory.Instance.UseItem(CurrentSlot.Index); // Use the item in the current slot
        if (CurrentSlot == null) return; // Check if the current slot is null
    }

    public void RemoveItem() // Method to remove an item from the currently selected slot
    {
        if (CurrentSlot == null) return; // Check if the current slot is null
        Inventory.Instance.RemoveItem(CurrentSlot.Index); // Remove the item in the current slot
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
        ShowItemDescription(slotIndex); // Show the item description for the selected slot
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return; // Check if the item at the index is null
        descriptionPanel.SetActive(true); // Show the description panel
        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon; // Set the item icon in the description panel
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name.ToString(); // Set the item name in the description panel
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description.ToString(); // Set the item description in the description panel
    }
    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf); // Toggle the inventory panel visibility

        if (inventoryPanel.activeSelf)
        {
            Time.timeScale = 0f; // Pause the game when inventory is open
        }
        else
        {
            Time.timeScale = 1f; // Resume the game when inventory is closed
            descriptionPanel.SetActive(false); // Hide the description panel when the inventory is closed
            CurrentSlot = null; // Clear the current slot when the inventory is closed
        }
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
