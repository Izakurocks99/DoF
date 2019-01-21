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

    private void Start()
    {
        healthBar.transform.parent.gameObject.SetActive(false);
        manaBar.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        else
        {
            healthBar.transform.parent.gameObject.SetActive(true);
            manaBar.transform.parent.gameObject.SetActive(true);
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
