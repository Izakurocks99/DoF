using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCard : MonoBehaviour {

    public EnemyScriptableObj cardSO;

    public Vector2 boardPos;

    public SpriteRenderer cardImage;
    public TextMeshPro attackDamage;
    public TextMeshPro healthPoints;

    public int attack, health;

    TurnManager manager;

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        attackDamage.GetComponent<TextMeshPro>();

        attack = cardSO.attack;
        health = cardSO.health;

        attackDamage.text = attack.ToString();
        healthPoints.text = health.ToString();

        var bounds = cardSO.art.bounds;
        var factor = (transform.localScale.y / bounds.size.y)/transform.localScale.y;
        cardImage.transform.localScale = new Vector3(factor, factor, factor);

        manager = FindObjectOfType<TurnManager>();
        manager.AddToList(this);
    }

    // Update is called once per frame
    void Update () {
	}

    public void OnDamaged(int damage)
    {
        health -= damage;
        healthPoints.text = health.ToString();
        //Deals Damage
        Debug.Log("ATTACK " + cardSO.cardName);
        //Returns to hand
        if (health <= 0)
        {
            manager.RemoveFromList(this);
        }
        else
        manager.EndTurn();
    }

    public int GetAttack()
    {
        //play attack animation
        return attack;
    }
}
