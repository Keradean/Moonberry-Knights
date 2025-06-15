using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityLoot;


public class LootButton : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI ItemQuantity;

    public LootItem ItemLoaded { get; private set; }

    public void ConfiqLootButton(LootItem lootItem)
    {
        ItemLoaded = lootItem;
        itemIcon.sprite = lootItem.Item.Icon;
        itemName.text = lootItem.Item.Name;
        ItemQuantity.text = $"{lootItem.Quantity.ToString()}";
    }

    public void CollectItem()
    {
        if (ItemLoaded == null) return;
        Inventory.Instance.AddItem(ItemLoaded.Item, ItemLoaded.Quantity);
        ItemLoaded.PickedItem = true;
        Destroy(gameObject);
    }
}
