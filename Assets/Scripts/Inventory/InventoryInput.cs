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
            Debug.Log("kEYPRESS");
            inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
        }
    }
}
