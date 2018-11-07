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
    public 

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        attackDamage.GetComponent<TextMeshPro>();
        attackDamage.text = cardSO.attack.ToString();
        healthPoints.text = cardSO.health.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
