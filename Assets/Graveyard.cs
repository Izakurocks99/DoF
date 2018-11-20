using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour {

    [SerializeField]
    List<GameObject> graveyard = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddtoGrave(GameObject card)
    {
        graveyard.Add(card);
    }

    public List<GameObject> GetGrave()
    {
        List<GameObject> tempList = new List<GameObject>(graveyard);
        graveyard.Clear();
        return tempList;
    }
}
