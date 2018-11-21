using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatCard : MonoBehaviour {

    public CombatCardScriptableObj cardSO;

    public Vector2 boardPos;

    public SpriteRenderer cardImage;
    public TextMeshPro attackDamage;
    public TextMeshPro manaCost;

    Graveyard grave;
    public PlayerHand hand;
    public int handIndex;
    public Player player;

    public bool reset;

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        attackDamage.GetComponent<TextMeshPro>();
        attackDamage.text = cardSO.attack.ToString();
        manaCost.text = cardSO.manaCost.ToString();

        grave = FindObjectOfType<Graveyard>();
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && UseCard())
        {
            EnemyCard enemy = collision.gameObject.GetComponent<EnemyCard>();
            enemy.health -= cardSO.attack;
            grave.AddtoGrave(gameObject);
            gameObject.SetActive(false);
            hand.RemoveCard(handIndex);
            hand.drawCard = true;
            
        }
        else
        {
            if (reset)
            {
                hand.ResetPos(handIndex);
                reset = false;
            }
        }
    }

    bool UseCard()
    {
        if (player.currManaPoints >= cardSO.manaCost)
        {
            player.currManaPoints -= cardSO.manaCost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
