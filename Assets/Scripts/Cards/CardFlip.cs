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

    public void Flip()
    {
        endRotation = endTransform.transform.rotation;
        flipping = true;
        fliptime = 0;

        flipped = !flipped;
    }
}
