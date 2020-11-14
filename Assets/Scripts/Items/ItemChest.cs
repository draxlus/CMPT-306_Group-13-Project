using UnityEngine;

public class ItemChest : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Inventory inventory;
	[SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

	public bool isInRange;
	private bool isEmpty = false;
		

    private void OnValidate()
    {
		if (inventory == null)
			inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode))
		{
			bool pickUp = inventory.AddItem(item);
			if (pickUp)
			{
				isEmpty = true;
				Debug.Log("Added Item");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
		Debug.Log("Player in range");
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
		Debug.Log("Player left range");
	}

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			isInRange = state;
		}
	}
}
