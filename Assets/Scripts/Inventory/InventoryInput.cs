using System;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode toggleInventoryKey;
    [SerializeField] PauseManager pauseManager;

 
    void Update()
    {
        if (Input.GetKeyDown(toggleInventoryKey))
        {
            inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
            pauseManager.PauseGame();
        }
    }
}
