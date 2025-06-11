using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemHealthPotion", menuName = "Items/HealthPotion")]
public class ItemHealthPotion : InventoryItem
{
    [Header("Config")]
    public float HealthValue; // The amount of health this potion restores

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerHealth.CanRestoreHealth())
        {
            GameManager.Instance.Player.PlayerHealth.RestoreHealth(HealthValue);
            return true;
        }
        return false;
    }


}
