using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BiomeScript : MonoBehaviour {

    public Biome cardSO;

    public LevelGeneration board;
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
        if(cardSO.dropList.Count == 0)
        {
            board.ResetBoard();
            Destroy(this.gameObject);
            return null;
        }
        Destroy(this.gameObject);
        return cardSO.Interact(item);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && board.CheckBesidePlayer(boardPos))
        {
            if (cardSO.dropList.Count == 0)
            {
                board.ResetBoard();
                Destroy(this.gameObject);
                return;
            }

            List<Item> drops = cardSO.Interact(null);
            foreach (Item drop in drops)
            {
                board.inventory.AddToInventory(drop);
            }
            Destroy(this.gameObject);
        }
    }
}
