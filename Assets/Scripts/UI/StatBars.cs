using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBars : MonoBehaviour {

    enum PlayerStats
    {
        NONE,
        HEALTH,
        MANA,
        HUNGER,
        MAX
    }

    [SerializeField] Player player;
    [SerializeField] PlayerStats stat;
    [SerializeField] Image statBar;

	// Use this for initialization
	void Start () {
        switch(stat)
        {
            case PlayerStats.HEALTH:
                break;
            case PlayerStats.MANA:
                break;
            case PlayerStats.HUNGER:
                break;
            default:
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
