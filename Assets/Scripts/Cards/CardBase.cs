using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour {

    public bool flipped = false; //faced down?
    bool flipping = false; //faced down?
    float flipHeight; //how high it flips, use force instead?

    [SerializeField]
    float flipSpeed; //time taken to flip
    float fliptime;
    
    [SerializeField]
    Transform endTransform;

    Quaternion endRotation;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate()
    {

        if (flipping)
        {
            fliptime += Time.deltaTime * flipSpeed;

            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, fliptime);

            if (fliptime >= 1f)
            {
                transform.rotation = endRotation;
                flipping = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            endRotation = endTransform.transform.rotation;
            flipping = true;
            fliptime = 0;

            flipped = !flipped;
        }
    }
}
