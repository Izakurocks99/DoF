﻿using System.Collections;
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

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        attackDamage.GetComponent<TextMeshPro>();
        attackDamage.text = cardSO.attack.ToString();
        healthPoints.text = cardSO.health.ToString();

        attack = cardSO.attack;
        health = cardSO.health;


        var bounds = cardSO.art.bounds;
        var factor = (transform.localScale.y / bounds.size.y)/transform.localScale.y;
        cardImage.transform.localScale = new Vector3(factor, factor, factor);

    }

    // Update is called once per frame
    void Update () {
		if(health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
