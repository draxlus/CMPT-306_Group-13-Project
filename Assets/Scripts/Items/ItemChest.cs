using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int Amount = 1;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKey = KeyCode.E;

    private bool IsInRange;
    private bool IsEmpty;

    private void Update()
    {
        if (IsInRange && IsEmpty && Input.GetKeyDown(itemPickupKey))
        {
            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy))
            {
                Amount--;
                if (Amount == 0)
                    IsEmpty = true;
            }
            else
            {
                itemCopy.Destroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
             IsInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            IsInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            IsInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            IsInRange = false;
    }
}
