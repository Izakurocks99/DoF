using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Biome")]
public class Biome : ScriptableObject {

    public Sprite art;
    public Item preferredTool;
    public List<Item> dropList;
    public int dropCount;
    public float dropMultiplier;

    public virtual List<Item> Interact(Item item)
    {
        List<Item> droppedItems = new List<Item>();
        float dropAmount = dropCount;
        if (item == preferredTool)
            dropAmount *= dropMultiplier;

        for (int i = 0; i < (int)dropAmount; ++i)
        {
            droppedItems.Add(dropList[Random.Range(0, dropList.Count)]);
        }
        return droppedItems;
    }
}
