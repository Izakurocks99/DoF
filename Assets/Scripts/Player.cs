using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHunger;
    public int currHunger;

    public int maxHealthPoints;
    public int currHealthPoints;

    public int maxManaPoints;
    public int currManaPoints;


    // Use this for initialization
    void Start () {
        currHunger = maxHunger;
        currHealthPoints = maxHealthPoints;
        currManaPoints = maxManaPoints;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
