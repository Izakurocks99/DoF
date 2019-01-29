using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public int healAmount;

    public void Consume(Player player)
    {
        player.ModifyHealth(healAmount);
    }
}
