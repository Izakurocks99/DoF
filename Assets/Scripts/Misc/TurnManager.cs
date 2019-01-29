using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    Player player;
    List<EnemyCard> enemyList = new List<EnemyCard>();
    List<BiomeScript> biomeList = new List<BiomeScript>();

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

    public void AddToList(BiomeScript biomeCard)
    {
        biomeList.Add(biomeCard);
    }

    public void RemoveFromList(EnemyCard enemyCard)
    {
        enemyList.Remove(enemyCard);
        Destroy(enemyCard.gameObject);

        if (enemyList.Count == 0)
            player.inCombat = false;
    }

    public void RemoveFromList(BiomeScript biomeCard)
    {
        biomeList.Remove(biomeCard);
        Destroy(biomeCard.gameObject);
    }

    public void EndTurn()
    {
        foreach(EnemyCard var in enemyList)
        {
            int attack = var.GetAttack();
            player.ModifyHealth(-attack);
        }
    }

    public void RemoveAll()
    {
        foreach(EnemyCard var in enemyList)
        {
            Destroy(var.gameObject);
        }
        enemyList.Clear();

        foreach (BiomeScript var in biomeList)
        {
            Destroy(var.gameObject);
        }
        biomeList.Clear();
    }
}
