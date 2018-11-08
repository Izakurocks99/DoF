using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour {

    bool flipped = false; //faced down?
    bool flipping = false; //faced down?
    float flipHeight; //how high it flips, use force instead?

    [SerializeField]
    float flipSpeed; //time taken to flip
    public float fliptime;
    public float percentage;

    [SerializeField]
    Transform startTransform;
    [SerializeField]
    Transform endTransform;

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
            fliptime += Time.deltaTime;
            percentage = fliptime / flipSpeed;

            transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, percentage);

            if (percentage >= 1f)
            {
                transform.rotation = endTransform.rotation;
                flipping = false;
                fliptime = 0;
                percentage = 0;
            }
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !flipped)
        {
            flipping = true;
        }
    }
}
