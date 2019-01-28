using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BiomeScript : MonoBehaviour {

    public Biome cardSO;

    public Vector2 boardPos;
    public SpriteRenderer cardImage;

    // Use this for initialization
    void Start () {
        cardImage.sprite = cardSO.art;
        var bounds = cardSO.art.bounds;
        var factor = (transform.localScale.y / bounds.size.y) / transform.localScale.y;
        cardImage.transform.localScale = new Vector3(factor, factor, factor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Item> Interact(Item item)
    {
        Destroy(this.gameObject);
        return cardSO.Interact(item);
    }
}
