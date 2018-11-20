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
    

	// Use this for initialization
	void Start () {
        cardImage.sprite = cardSO.art;
        attackDamage.GetComponent<TextMeshPro>();
        attackDamage.text = cardSO.attack.ToString();
        manaCost.text = cardSO.manaCost.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
