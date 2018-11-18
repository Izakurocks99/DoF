using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour {

    public bool flipped = false; //faced down?
    bool flipping = false; //faced down?

    [SerializeField]
    float flipSpeed = 0; //time taken to flip
    float fliptime;

    [SerializeField]
    Transform endTransform = null;
    Quaternion endRotation;

    CardBase cardBase =null;

    private void Start()
    {
        cardBase = gameObject.GetComponent<CardBase>();
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
                fliptime = 0;
                cardBase.Reveal();
            }
        }
    }

    public void Flip()
    {
        endRotation = endTransform.transform.rotation;
        flipping = true;
        fliptime = 0;

        flipped = !flipped;
    }
}
