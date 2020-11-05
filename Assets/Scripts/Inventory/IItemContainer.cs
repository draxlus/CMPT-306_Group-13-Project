public interface IItemContainer
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemId);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
}