using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform arrowParent;
    [SerializeField] BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public ItemContainer ItemContainer;

    public CraftingRecipe craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get { return craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
    }

    private void Start()
    {
        foreach (BaseItemSlot itemSlot in itemSlots)
        {
            itemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlot.OnPointerExitEvent += OnPointerExitEvent;
        }
    }

    public void OnCraftButtonClick()
    {
        if(craftingRecipe != null && ItemContainer != null)
        {
            if (craftingRecipe.CanCraft(ItemContainer))
            {
                if (!ItemContainer.IsFull())
                    craftingRecipe.Craft(ItemContainer);
                else
                    Debug.LogError("Inventory is Full");
            }
            else
                Debug.LogError("Not enough required Items!");
        }
    }

    public void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
    {
        craftingRecipe = newCraftingRecipe;

        if(craftingRecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
            arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

            for (int i = slotIndex; i < itemSlots.Length; i++)
            {
                itemSlots[i].transform.parent.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public int SetSlots(IList<ItemAmount> itemAmounts, int slotIndex)
    {
        for (int i = 0; i < itemAmounts.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmounts[i];
            BaseItemSlot itemSlot = itemSlots[slotIndex];

            itemSlot.Item = itemAmount.Item;
            itemSlot.Amount = itemAmount.Amount;
            itemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }
}
