using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent; // Event to notify when a slot is selected

    [Header("Config")]
    [SerializeField] private Image itemIcon; // UI element to display the item icon
    [SerializeField] private Image quantityImg; // UI element to display the quantity of the item
    [SerializeField] private TextMeshProUGUI itemQuantityTMP; // Text element to show the quantity of the item
    public int Index { get; set; } // Index of the slot in the inventory

    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index); // Invoke the event with the index of the selected slot
    }

    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon; // Set the item icon sprite
        itemQuantityTMP.text = item.Quantity.ToString(); // Update the quantity text
    }
    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value); // Show or hide the item icon based on the value
        quantityImg.gameObject.SetActive(value); // Hide the quantity image
    }
}
