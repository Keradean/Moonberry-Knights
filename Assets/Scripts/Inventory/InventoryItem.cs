using UnityEngine;

public enum ItemType // Enum to define different types of items in the inventory
{
    Weapon,
    Armor,
    Consumable,
    QuestItem,
    Scroll,
    Potion,
    Ingredient,
    Treasure
}
[CreateAssetMenu(menuName = "Items/Item")] // Create a menu item in Unity's asset creation menu for creating inventory items
public class InventoryItem : ScriptableObject
{
    [Header("Item Properties")]
    public string ID; // Unique identifier for the item
    public string Name; // Name of the item
    public Sprite Icon; // Icon representing the item
    [TextArea] public string Description;// Description of the item

    [Header("Item Information")]
    public ItemType ItemType; // Type of the item (Weapon, Armor, etc.)
    public bool IsConsumable; // Whether the item can be consumed
    public bool isStackable; // Whether the item can be stacked in the inventory
    public int MaxStack; // Maximum stack size for stackable items

    [HideInInspector] public int Quantity; // Current quantity of the item in the inventory

    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this); // Create a copy of the item
        return instance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual void EquipItem()
    {
        // Logic for equipping the item
    }

    public virtual void RemoveItem()
    {
        // Logic for removing the item
    }
}
