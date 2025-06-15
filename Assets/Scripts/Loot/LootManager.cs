using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Confiq")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform container;


    public void ShowLoot(EntityLoot entityLoot)
    {
        lootPanel.SetActive(true);
        Time.timeScale = 0f; 
        if (LootPanelWithItems())
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        foreach (EntityLoot.LootItem item in entityLoot.Items)
        {
            if (item.PickedItem) continue;
            LootButton lootbutton = Instantiate(lootButtonPrefab, container);
            lootbutton.ConfiqLootButton(item);
        }
    }
        public void ClosePanel()
        {
            lootPanel.SetActive(false);
            Time.timeScale = 1f;
        }
   

    private bool LootPanelWithItems()
    {
        return container.childCount > 0; 
    }
}
