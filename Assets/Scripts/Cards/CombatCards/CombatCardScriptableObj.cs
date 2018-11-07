using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName ="Cards/Combat")]
public class CombatCardScriptableObj : ScriptableObject{

    public string cardName;
    public string description;

    public Sprite art;

    public int manaCost;
    public int attack;
}
