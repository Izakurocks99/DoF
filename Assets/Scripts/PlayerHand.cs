using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    [SerializeField]
    PlayerDeck deck;
    [SerializeField]
    int handSizeMax;
    [SerializeField]
    int handSize;
    [SerializeField]
    int drawAmount;
    [SerializeField]
    Graveyard grave;

    [SerializeField]
    GameObject handCardArea;

    List<GameObject> handCards = new List<GameObject>();
    List<Vector2> handCardPos = new List<Vector2>();

    public bool drawCard = false;
    public float cardSpacing = 0;

    float cardWidth = 0;
    float cardHeight =0;
    
   
    // Use this for initialization
    void Start ()
    {
        cardWidth = handCardArea.transform.lossyScale.x;
        cardHeight = handCardArea.transform.lossyScale.y;

        float totalWidth = (cardWidth + cardSpacing * 2) * handSizeMax;

        Vector2 startposition = (Vector2)transform.position + new Vector2(-totalWidth * 0.5f + cardWidth * 0.5f + cardSpacing,
                                    0); //- half of width + cardsize
        for (int i = 0; i < handSizeMax; ++i)
        {
            Vector2 cardPos = startposition + new Vector2(i * (cardWidth + cardSpacing*2),0); //get pos using x and y
            Instantiate(handCardArea, cardPos, Quaternion.identity);
            handCardPos.Add(cardPos);
        }
        StartCoroutine(DrawCard(handSize));
    }
	
	// Update is called once per frame
	void Update () {

        if (drawCard)
        {
            handSize++;
            StartCoroutine(DrawCard(drawAmount));
            drawCard = false;
        }
    }

    IEnumerator DrawCard(int amount)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i=0;i < amount;++i)
        {
            GameObject go = deck.DrawCard();
            if (handCards.Count < handSizeMax)
            {
                handCards.Add(go);
                go.transform.position = handCardPos[handCards.Count - 1];
                go.SetActive(true);
                CombatCard cc = go.GetComponent<CombatCard>();
                cc.hand = this;
                cc.handIndex = handCards.Count - 1;

            }
            else
            {
                grave.AddtoGrave(go);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    public void RemoveCard(int index)
    {
        handSize--;
        handCards.RemoveAt(index);

        for (int i =0;i < handSize;++i)
        {
            handCards[i].transform.position = handCardPos[i];
            handCards[i].GetComponent<CombatCard>().handIndex = i;
        }
    }
}
