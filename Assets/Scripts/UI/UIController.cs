using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Game Object References")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerHungerText;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Transform inventorySlotsGrid;
    [SerializeField] private Transform quickSlotsGrid;
    [SerializeField] private InventorySlotUI inventorySlotPrefab;
    [SerializeField] private QuickSlotUI quickSlotPrefab;
    [Header("Foldable Panel Setting")]
    [SerializeField] private float panelVisibleHeight = 230f;
    [SerializeField] private float panelMoveSpeed = 5000f;
    [SerializeField] private bool isInventoryShown = false;
    public bool IsInventoryShown => isInventoryShown;
    private float panelHiddenY;
    private float panelShownY = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // calculate folded panel height
        float panelHeight = panel.rect.height;
        panelHiddenY = - (panelHeight - panelVisibleHeight);
        // init quick slots
        int index = 0;
        foreach (InventorySlot quickSlot in InventoryManager.Instance.GetQuickSlots())
        {
            QuickSlotUI quickSlotUI = Instantiate(quickSlotPrefab, quickSlotsGrid);
            quickSlotUI.SetSlot(quickSlot, index);
            index ++;
        }

        // for (int i = 0; i < 5; i++)
        // {
        //     Debug.Log(i);
        //     var singleQuickSlot = Instantiate(quickSlotPrefab, quickSlotsGrid);
        //     Debug.Log(i + "1");
        //     singleQuickSlot.SetIndex(i);
        //     Debug.Log(i + "2");
        // }
    }

    void Update()
    {
        UpdateFoldableInventoryAnimation();
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        int currentHealth = PlayerStatsManager.Instance.currentHealth;
        int maxHealth = PlayerStatsManager.Instance.maxHealth;
        int currentHunger = PlayerStatsManager.Instance.currentHunger;
        int maxHunger = PlayerStatsManager.Instance.maxHunger;
        String health = currentHealth + "/" + maxHealth;
        String hunger = currentHunger + "/" + maxHunger;
        playerHealthText.text = health;
        playerHungerText.text = hunger;
    }

    private void UpdateFoldableInventoryAnimation()
    {
        float targetY = isInventoryShown ? panelShownY : panelHiddenY;
        Vector2 pos = panel.anchoredPosition;
        float moveStep = panelMoveSpeed * Time.unscaledDeltaTime;
        pos.y = Mathf.MoveTowards(pos.y, targetY, moveStep);
        panel.anchoredPosition = pos;
    }

    public void ToggleFoldablePanel()
    {
        isInventoryShown = !isInventoryShown;
        RefreshInventoryContent();

        // freeze game time 
        Time.timeScale = isInventoryShown ? 0f : 1f;
    }

    public void RefreshInventoryContent()
    {
        foreach (Transform child in inventorySlotsGrid)
            Destroy(child.gameObject);

        List<InventorySlot> inventorySlots = InventoryManager.Instance.GetSlots();

        foreach (var slot in inventorySlots)
        {
            var ui = Instantiate(inventorySlotPrefab, inventorySlotsGrid);
            ui.SetSlot(slot);
        }
    }
}
