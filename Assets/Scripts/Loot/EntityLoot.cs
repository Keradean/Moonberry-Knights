using System.Collections.Generic;
using System; 
using UnityEngine;
using Random = UnityEngine.Random;


public class EntityLoot : MonoBehaviour
{
    [SerializeField] private LootItem[] lootItem; // Array of loot items that can be dropped by the entity
    public List<LootItem> Items { get; private set;}

    private void Start()
    {
      loadLootItems();
    }
    private void loadLootItems()
    {
        Items = new List<LootItem>();
        foreach (LootItem item in lootItem)
        {
            float prob = Random.Range(0f, 100f);
            if (prob <= item.LootChance) 
            {
                Items.Add(item);
            }
        }
    }

    [System.Serializable]
    public class LootItem
    {
        [Header("Loot Settings")]
        public string Name; // Name of the loot item
        public InventoryItem Item; // Reference to the InventoryItem scriptable object
        public int Quantity; // Quantity of the loot item

        [Header("Loot Chance")]
        public float LootChance; // Chance of this item dropping (0 to 1)
        public bool PickedItem { get; set; } // Whether the item has been picked up
    }
}
