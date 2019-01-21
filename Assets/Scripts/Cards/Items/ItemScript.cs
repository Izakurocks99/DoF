﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public Item itembase;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 inventoryPos;
    public int inventoryIndex;

    public bool reset;
    public int lifeTime;

    Inventory theInv;

	// Use this for initialization
	void Start () {
        spriteRenderer.sprite = itembase.art;
        var bounds = spriteRenderer.sprite.bounds;
        var factor = transform.localScale.y / bounds.size.y;
        spriteRenderer.transform.localScale = new Vector3(factor, factor, factor);

        itembase.Init();

        lifeTime = itembase.lifetime;
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
        }
        //Return to hand
        if (reset)
        {
            transform.localPosition = inventoryPos;
            reset = false;
        }
    }
}