using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCrafting : MonoBehaviour
{
    [SerializeField] GameObject[] towers;
    [SerializeField] KeyCode[] TowerPlacementKey;
    [SerializeField] Inventory inventory;
    public List<ItemAmount> materials;
    private Transform playerPos;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CraftTower(inventory);
    } 

    void CraftTower(ItemContainer itemContainer)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < TowerPlacementKey.Length; i++)
        {
            if (Input.GetKeyDown(TowerPlacementKey[i]))
            {
                foreach (ItemAmount itemAmount in materials)
                {
                    if (itemContainer.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
                    {
                        Debug.LogWarning("You don't have the required materials.");
                        NotificationManager.Instance.SetNewNotification("Not enough material!");
                    }
                    else
                    {
                        for (int j = 0; j < itemAmount.Amount; j++)
                        {
                            Item oldItem = itemContainer.RemoveItem(itemAmount.Item.ID);
                            oldItem.Destroy();
                        }
                        GameObject p = Instantiate(towers[i], playerPos.position + (transform.forward * 2), playerPos.rotation);
                        p.SetActive(true);
                        NotificationManager.Instance.SetNewNotification("Removed items and built a tower");
                        print("Built tower");
                    }
                } 
            }
        }
    }
}

