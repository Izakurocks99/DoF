using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    [SerializeField]
    Vector3 posOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player");
                //posOffset = gameObject.transform.position - player.transform.position;
                Vector3 newPos = player.transform.position + posOffset;
                gameObject.transform.position = newPos;
            }
        }
        else
        {
            if (!player.GetComponent<Player>().dead)
            {
                Vector3 newPos = player.transform.position + posOffset;
                gameObject.transform.position = new Vector3(newPos.x, newPos.y, gameObject.transform.position.z);
            }
        }

    }
}
