using System;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode toggleInventoryKey;

 
    void Update()
    {
        if (Input.GetKeyDown(toggleInventoryKey))
        {
            inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
            if (inventoryGameObject.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
