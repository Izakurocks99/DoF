﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public Item itembase;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 inventoryPos;
    public int inventoryIndex;

    public bool selected;

    Inventory theInv;

	// Use this for initialization
	void Start () {
        spriteRenderer.sprite = itembase.art;
        var bounds = spriteRenderer.sprite.bounds;
        var factor = transform.localScale.y / bounds.size.y;
        spriteRenderer.transform.localScale = new Vector3(factor, factor, factor);

        itembase.Init();
	}

    // Update is called once per frame
    void Update () {
		
	}

    public void Init(Vector3 pos,int index, Inventory inv)
    {
        inventoryPos = pos;
        theInv = inv;
        inventoryIndex = index;
    }

    public void SelectThis()
    {
        selected = true;
    }
    public void LetGo()
    {
        selected = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // move this to inside scriptable obj
        if (itembase is Weapon && collision.gameObject.tag == "Enemy")
        {
            //get enemy component
            EnemyCard enemy = collision.gameObject.GetComponent<EnemyCard>();
            //cast to weapon type
            Weapon weapon = (Weapon)itembase;
            //use weapon
            weapon.Attack(enemy);
            //Return to hand
            transform.localPosition = inventoryPos;
        }
        if (itembase is Material && collision.gameObject.tag == "Pickable")
        {
            if (collision.gameObject.GetComponent<ItemScript>().GetInstanceID() > this.GetInstanceID())
                return;

            Item result;
            //get other component
            Item other = collision.gameObject.GetComponent<ItemScript>().itembase;
            //cast to material type
            Material material = (Material)itembase;
            //use weapon
            if (material.Craft(other, out result))
            {

                //create from result
                theInv.RemoveFromInventory(collision.gameObject.GetComponent<ItemScript>().inventoryIndex);
                theInv.RemoveFromInventory(inventoryIndex);
                theInv.AddToInventory(result);
            }
            else
                //Return to hand
                transform.localPosition = inventoryPos;
        }
    }
}
