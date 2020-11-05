using UnityEditor;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    [Range(1, 99)]
    public int MaxStacks = 1;
    public Sprite Icon;

    private void OnValidate()
    {

        id = Random.Range(1, 50).ToString();
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }
}
