using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image manaBar;


    public float maxHunger;
    public float currHunger;
           
    public float maxHealthPoints;
    public float currHealthPoints;
           
    public float maxManaPoints;
    public float currManaPoints;


    // Use this for initialization
    void Start () {
        //currHunger = maxHunger;
        //currHealthPoints = maxHealthPoints;
        //currManaPoints = maxManaPoints;

        healthBar.fillAmount = currHealthPoints / maxHealthPoints;
        manaBar.fillAmount = currManaPoints/ maxManaPoints;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currHealthPoints / maxHealthPoints;
    }
    
    void UpdateManaBar()
    {
        manaBar.fillAmount = currManaPoints / maxManaPoints;
    }

}
