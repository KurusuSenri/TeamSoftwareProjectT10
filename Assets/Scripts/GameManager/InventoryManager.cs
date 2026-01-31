using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private List<InventorySlot> slots = new();
    public List<InventorySlot> quickSlots = new();
    [SerializeField] private int selectedQuickSlotIndex = 0;
    public event Action OnQuickSlotsChanged;                // change of quick slots content
    public event Action<int> OnQuickSlotIndexChanged;       // change of selected quick slot index
    public event Action OnInventoryChanged;                 // change of inventory content

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
        // 5 available slots
        for (int i = 0; i < 5; i++)
        {
            quickSlots.Add(null);
        }
    }

    public int GetSelectedQuickSlotIndex()
    {
        return selectedQuickSlotIndex;
    }

    public int GetItemCount(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
                return slot.count;
        }
        return 0;
    }

    public InventorySlot GetSelectedQuickSlot()
    {
        return quickSlots[selectedQuickSlotIndex];
    }

    public bool HasItem(Item item, int amount)
    {
        return GetItemCount(item) >= amount;
    }

    public List<InventorySlot> GetSlots()
    {
        return slots;
    }

    public List<InventorySlot> GetQuickSlots()
    {
        return quickSlots;
    }

    public void ChangeSelectedQuickSlot(InventorySlot slot)
    {
        quickSlots[selectedQuickSlotIndex] = slot;
        OnQuickSlotsChanged?.Invoke();
    }

    public void SetQuickSlotByIndex(int quickSlotIndex, InventorySlot slot)
    {
        quickSlots[quickSlotIndex] = slot;
        OnQuickSlotsChanged?.Invoke();
    }
    public void UseSelectedQuickSlotItem()
    {
        if (quickSlots[selectedQuickSlotIndex] != null && quickSlots[selectedQuickSlotIndex].count > 0)
        {
            quickSlots[selectedQuickSlotIndex].Use();
            if (quickSlots[selectedQuickSlotIndex].count == 0)
            {
                slots.Remove(quickSlots[selectedQuickSlotIndex]);
                quickSlots[selectedQuickSlotIndex] = null;
            }
            // item may used so count changed
            // OnQuickSlotsChanged?.Invoke();
            OnInventoryChanged?.Invoke();
        }
    }

    public void SetQuickSlotIndex(int index)
    {
        selectedQuickSlotIndex = index;
        OnQuickSlotIndexChanged?.Invoke(index);
    }

    public void AddItem(Item item, int amount = 1)
    {
        var slot = slots.Find(s => s.item == item);
        if (slot != null)
            slot.count += amount;
        else
            slots.Add(new InventorySlot(item, amount));
        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                if (slots[i].count < amount)
                    return false;

                slots[i].count -= amount;

                if (slots[i].count == 0)
                    slots.RemoveAt(i);

                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        return false;
    }
}

[Serializable]
public class InventorySlot
{
    public Item item;
    public int count;

    public InventorySlot(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public bool Use()
    {
        if (item != null)
        {
            bool wasUsedUp = item.Use();
            if (wasUsedUp)
            {
                count--;
            }
            return wasUsedUp;
        }
        return false;
    }
}
