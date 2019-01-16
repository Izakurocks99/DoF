using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CraftingRecipe
{
    public Item otherMaterial;
    public Item result;
}


[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Material")]
public class Material : Item
{
    public List<CraftingRecipe> recipes;
    public Dictionary<Item, Item> itemMap;

    public override void Init()
    {
        itemMap = new Dictionary<Item, Item>();
        foreach(CraftingRecipe var in recipes)
        {
            itemMap.Add(var.otherMaterial, var.result);
        }
    }

    public virtual bool Craft(Item other, out Item result)
    {
        if (itemMap.ContainsKey(other))
        {
            result = itemMap[other];
            Debug.Log("Crafted with: " + other.cardName + " Created: " + result.cardName);
            return true;    
        }
        result = null;
        return false;
    }
}