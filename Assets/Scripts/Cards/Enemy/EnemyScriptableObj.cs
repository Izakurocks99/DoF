using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName ="Cards/Enemies")]
public class EnemyScriptableObj : ScriptableObject{

    public string cardName;
    public string description;

    public Sprite art;
    
    public int attack;
    public int health;
    public List<Item> dropList;
    [Range(0,1)]
    public float dropPercent;

    public virtual Item Harvest()
    {
        if(Random.value < dropPercent)
        {
            return dropList[Random.Range(0, dropList.Count)];
        }
        return null;
    }
}
