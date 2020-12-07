using UnityEngine;

public class ItemChest : MonoBehaviour
{
	[SerializeField] public Item item;
	[SerializeField] float amount;
	[SerializeField] Inventory Inventory;
	[SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
	
	public bool isInRange;
	public bool isEmpty = false;
	private float health;

	private void Awake()
    {
		if (Inventory == null)
			Inventory = FindObjectOfType<Inventory>();
	}

    private void Start()
    {
		health = 1f;
	}

    private void Update()
	{
		if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode))
		{
            bool added = Inventory.AddItem(item);
			amount--;
			
			if (added & amount == 0)
			{
				isEmpty = true;
				health--;
				Debug.Log("Added Item");
				NotificationManager.Instance.SetNewNotification("Added " + item.name);
				this.gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
	}

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			isInRange = state;
		}
	}
}
