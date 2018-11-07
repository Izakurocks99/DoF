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
}
