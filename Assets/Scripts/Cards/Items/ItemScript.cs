using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemScript : MonoBehaviour {

    public Item itembase;
    public Vector3 inventoryPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(Vector3 pos)
    {
        inventoryPos = pos;
    }
}
