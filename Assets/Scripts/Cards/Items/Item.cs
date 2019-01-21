using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Tool,
    Material,
    Weapon,
    Armour,
    Consumable,
}

public enum ItemTier
{
    Wood,
    Stone,
    Iron,
    Gold,
    Diamond
}

public class Item : ScriptableObject {

    public string cardName;
    public string description;
    public ItemType type;
    public ItemTier tier;
    public Sprite art;

    public int lifetime;

    public virtual void Init()
    {

    }
}
