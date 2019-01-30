using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour {

    Animator animator;
    [SerializeField]
    Sprite startingSprite;
    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();

        float factor = (transform.parent.localScale.y / startingSprite.bounds.size.y) / transform.parent.localScale.y;
        transform.localScale = new Vector3(factor, factor, factor);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayDamagedAnim()
    {
        animator.SetTrigger("Damaged");
    }
}
