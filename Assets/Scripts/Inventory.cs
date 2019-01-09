using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates the inventory area
//stores inventory items and its inventory position

public class Inventory : MonoBehaviour {

    [SerializeField]
    GameObject itemObj;
    [SerializeField]
    int numSlots = 10;
    [SerializeField]
    float cardSpacing = 0.1f;
    [SerializeField]
    float padding = 0.5f;
    [SerializeField]
    int rows = 2;
    [SerializeField]
    float itemOffset = 0.5f;

    List<Vector2> cardPositions = new List<Vector2>();
    List<GameObject> items = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //set the size of the hand area
        float scalex = (numSlots/rows) + (numSlots / rows) * cardSpacing + padding * 2;
        float scaley = rows + rows * cardSpacing + padding * 2;
        this.gameObject.transform.localScale = new Vector3(scalex, scaley, 0.1f);

        Vector3 botLeft = gameObject.transform.position + new Vector3((-scalex+ itemObj.transform.localScale.x )/ 2f, (-scaley+ itemObj.transform.localScale .y)/ 2f, -itemOffset);
        //set the card positions into list
        //Instantiate(itemObj, topLeft, Quaternion.identity);
        int rowSize = numSlots / rows;
        for (int x = 0; x < rowSize; ++x)
        {           
            for (int y = 0; y < rows; ++y)
            {
                float posx = (itemObj.transform.localScale.x + cardSpacing) * x + padding;
                float posy = (itemObj.transform.localScale.y + cardSpacing) * y + padding;

                GameObject item= Instantiate(itemObj, new Vector3(posx , posy ) + botLeft, Quaternion.identity);
                item.transform.parent = this.gameObject.transform.parent;

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Rescale();

    }

    void Rescale()
    {
        float scalex = numSlots + numSlots * cardSpacing;
        float scaley = rows + rows * cardSpacing;
        this.gameObject.transform.localScale = new Vector3(scalex, scaley, 1);
    }
}
