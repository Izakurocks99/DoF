﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckCards
{
    public CombatCardScriptableObj card;
    public int count;
}

public class PlayerDeck : MonoBehaviour {

    public List<DeckCards> deckInit;

    [SerializeField]
    GameObject combatCard;
    [SerializeField]
    List<GameObject> deck;
    [SerializeField]
    Graveyard graveyard;

    // Use this for initialization
    void Start () {
        deck = new List<GameObject>();

        foreach (DeckCards var in deckInit)
        {
            for(int i =0; i <var.count;++i)
            {
                GameObject go = Instantiate(combatCard);
                go.GetComponent<CombatCard>().cardSO = var.card;
                deck.Add(go);
            }
        }

        ShuffleDeck(deck);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShuffleDeck(List<GameObject> in_deck)
    {
        for(int i = 0; i< in_deck.Count;++i)
        {
            GameObject temp = in_deck[i];
            int randomIndex = Random.Range(i, in_deck.Count);
            in_deck[i] = in_deck[randomIndex];
            in_deck[randomIndex] = temp;
        }
    }

    public GameObject DrawCard()
    {
        if (deck.Count > 0)
        {
            GameObject go = deck[0];
            deck.RemoveAt(0);
            return go;
        }
        return null;
    }

    public void ResetDeck()
    {
        List<GameObject> temp = graveyard.GetGrave();        
        ShuffleDeck(temp);
        foreach(GameObject var in temp)
        {
            deck.Add(var);
        }
    }
}
