﻿using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsPanel;
    [SerializeField] EquipmentSlot[] equipmentSlots;
    [SerializeField] Animator animator;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentSlots[i].OnDragEvent += OnDragEvent;
            equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsPanel.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                if (item.EquipmentType == EquipmentType.Sword)
                {
                    animator.SetBool("sword", true);
                    animator.SetBool("spear", false);
                    return true;
                }
                else if (item.EquipmentType == EquipmentType.Spear)
                {
                    animator.SetBool("spear", true);
                    animator.SetBool("sword", false);
                    return true;
                }
                else
                    return true;
            }
        }
        previousItem = null;
        return false;
    }


    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
