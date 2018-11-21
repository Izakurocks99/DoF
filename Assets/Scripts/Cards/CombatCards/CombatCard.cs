using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatCard : MonoBehaviour {

    public CombatCardScriptableObj cardSO;

    public SpriteRenderer cardImage;
    public TextMeshPro atkTxt;
    public TextMeshPro manaTxt;

    Graveyard grave;
    public PlayerHand hand;
    public int handIndex;
    public Player player;

    public bool reset;

    int attackdmg;
    int mana;

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        atkTxt.GetComponent<TextMeshPro>();
        atkTxt.text = cardSO.attack.ToString();
        manaTxt.text = cardSO.manaCost.ToString();

        grave = FindObjectOfType<Graveyard>();
        player = FindObjectOfType<Player>();

        attackdmg = cardSO.attack;
        mana = cardSO.manaCost;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && UseCard())
        {
            EnemyCard enemy = collision.gameObject.GetComponent<EnemyCard>();
            enemy.health -= attackdmg;
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
        return player.CastSpell(mana);
    }
}
