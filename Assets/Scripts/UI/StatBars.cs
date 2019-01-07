using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBars : MonoBehaviour {


    [SerializeField] Player player;

    [SerializeField]
    Image healthBar = null;
    [SerializeField]
    Image manaBar = null;
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        else
        {
            UpdateHealthBar();
            UpdateManaBar();
        }
	}

    void UpdateManaBar()
    {
        manaBar.fillAmount = player.currManaPoints / player.maxManaPoints;
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = player.currHealthPoints / player.maxHealthPoints;
    }
}
