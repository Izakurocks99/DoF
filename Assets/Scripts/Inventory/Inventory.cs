using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates the inventory area
//stores inventory items and its inventory position

public class Inventory : MonoBehaviour {
    [SerializeField] GameObject itemObj;
    [SerializeField] int numSlots = 10;
    [SerializeField] int rows = 2;
    [SerializeField] float cardSpacing = 0.1f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] float itemOffset = 0.5f;

    List<Vector3> cardPositions = new List<Vector3>();
    [SerializeField] List<Item> startingItems = new List<Item>();
    [SerializeField] GameObject[] items;

	// Use this for initialization
	void Start () {
        items = new GameObject[numSlots];
        //set the size of the hand area
        float scalex = (numSlots/rows) + (numSlots / rows) * cardSpacing + padding * 2;
        float scaley = rows + rows * cardSpacing + padding * 2;
        this.gameObject.transform.localScale = new Vector3(scalex, scaley, 0.3f);

        Vector3 botLeft = gameObject.transform.localPosition + new Vector3((-scalex+ itemObj.transform.localScale.x )/ 2f, (-scaley+ itemObj.transform.localScale .y)/ 2f, -itemOffset);
        //set the card positions into list
        int rowSize = numSlots / rows;
        for (int y = 0; y < rows; ++y)
        {
            for (int x = 0; x < rowSize; ++x)
            {
                float posx = (itemObj.transform.localScale.x + cardSpacing) * x + padding;
                float posy = (itemObj.transform.localScale.y + cardSpacing) * y + padding;

                cardPositions.Add(new Vector3(posx, posy) + botLeft);
            }
        }

        //add starting items
        foreach (Item var in startingItems)
        {
            //spawn a game object and add the variables of the scriptable obj into it/ assign the scriptable obj
            AddToInventory(var);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void AddToInventory(Item _item)
    {
        Vector3 position = new Vector3(0,0,0);
        int i;
        for(i =0; i < numSlots; ++i)
        {
            if (items[i] == null)
            {
                position = cardPositions[i];
                break;
            }
            if (i == numSlots - 1)
                return;
        }

        GameObject item = Instantiate(itemObj, position, Quaternion.identity);
        item.transform.parent = this.gameObject.transform.parent;
        item.transform.localPosition = position;
        item.GetComponent<ItemScript>().itembase = _item;
        item.GetComponent<ItemScript>().Init(item.transform.localPosition,i,this);
        items[i] = item;

    }

    public void RemoveFromInventory(int _itemIndex)
    {
        Destroy(items[_itemIndex]);
        items[_itemIndex] = null;
        
    }

    public void ResetInventory()
    {
        for(int i = 0;i < items.Length; ++i)
        {
            if (items[i])
            {
                RemoveFromInventory(i);
            }
        }

        //add starting items
        foreach (Item var in startingItems)
        {
            //spawn a game object and add the variables of the scriptable obj into it/ assign the scriptable obj
            AddToInventory(var);
        }
    }
}
