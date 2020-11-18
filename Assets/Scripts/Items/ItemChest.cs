using UnityEngine;

public class ItemChest : MonoBehaviour
{
	[SerializeField] public Item item;
	[SerializeField] float amount;
	[SerializeField] Inventory Inventory;
	[SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
	[SerializeField] ObjectHealthBar healthBar;

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
		healthBar.SetSize(health);
	}

    private void Update()
	{
		if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode))
		{
            bool added = Inventory.AddItem(item);
			amount--;
			

            if (health > 0)
            {
				health -= .5f ;
				healthBar.SetSize(health);
			}

			if (added & amount == 0)
			{
				isEmpty = true;
				Debug.Log("Added Item");
				this.gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
        if (collision.gameObject.CompareTag("Player"))
		{
			healthBar.gameObject.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
		healthBar.gameObject.SetActive(false);
	}

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			isInRange = state;
		}
	}
}
