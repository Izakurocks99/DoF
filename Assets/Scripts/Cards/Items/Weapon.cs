using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public int damage;

    public virtual void Attack(EnemyCard enemy)
    {
        enemy.health -= damage;
        //Deals Damage
        Debug.Log("ATTACK " + enemy.cardSO.cardName);
        //Returns to hand
    }
}
