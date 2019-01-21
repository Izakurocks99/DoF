using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    Player player;
    List<EnemyCard> enemyList = new List<EnemyCard>();

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
        }

    }

    public void AddToList(EnemyCard enemyCard)
    {
        enemyList.Add(enemyCard);
    }

    public void EndTurn()
    {
        foreach(EnemyCard var in enemyList)
        {
            int attack = var.GetAttack();
            player.ModifyHealth(-attack);
        }
    }
}
