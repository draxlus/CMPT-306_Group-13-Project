using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Armor,
    Leggings,
    Boots,
    Sword,
    Spear,
    Sheild,
    Accessory
}


[CreateAssetMenu]
public class EquippableItem : Item
{
    public int HealthBonus;
    public int StrengthBonus;
    public int SpeedBonus;
    [Space]
    public float HealthPercentBonus;
    public float StrengthPercentBonus;
    public float SpeedPercentBonus;

    public EquipmentType EquipmentType;


    [SerializeField]
    private GearSocket gearSocket;

    public static object AnimationClips { get; internal set; }

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        DestroyImmediate(this);
    }

    public void Equip(Character c)
    {
        if (HealthBonus != 0)
            c.Health.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, this));
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (SpeedBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        
        if (HealthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(HealthPercentBonus, StatModType.PercentMult, this));
        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (SpeedPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        if (gearSocket != null)
        {
            gearSocket.Equip(c.AnimationClip);
        }
        
    }


    public void Unequip(Character c)
    {
        c.Health.RemoveAllModifiersFromSource(this);
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Speed.RemoveAllModifiersFromSource(this);
        if (gearSocket != null)
        {
            gearSocket.Dequip();
        }
    }
    
}

