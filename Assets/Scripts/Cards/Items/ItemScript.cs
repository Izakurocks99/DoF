﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public Item itembase;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color spriteColor;
    public Vector3 inventoryPos;
    public int inventoryIndex;
    public ItemDescription descriptionBox;

    public bool reset;
    public int lifeTime;

    Inventory theInv;
    bool canCraft = true;

	// Use this for initialization
	void Start () {
        spriteRenderer.sprite = itembase.art;
        spriteRenderer.color = itembase.color;
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
            DecreaseDurability();
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            BiomeScript biome = collision.gameObject.GetComponent<BiomeScript>();
            List<Item> drops = biome.Interact(itembase);
            foreach(Item drop in drops)
            {
                theInv.AddToInventory(drop);
            }
            DecreaseDurability();
        }
        else if (itembase is Material && collision.gameObject.tag == "Pickable")
        {
            if (collision.gameObject.GetComponent<ItemScript>().GetInstanceID() > this.GetInstanceID() || !canCraft || !collision.gameObject.GetComponent<ItemScript>().canCraft)
            { }
            else
            {
                Item result;
                //get other component
                Item other = collision.gameObject.GetComponent<ItemScript>().itembase;
                //cast to material type
                Material material = (Material)itembase;
                if (material.Craft(other, out result))
                {
                    canCraft = false;
                    collision.gameObject.GetComponent<ItemScript>().canCraft = false;
                    //create from result
                    theInv.RemoveFromInventory(collision.gameObject.GetComponent<ItemScript>().inventoryIndex);
                    theInv.RemoveFromInventory(inventoryIndex);
                    theInv.AddToInventory(result);
                }
            }
        }

        //Return to hand
        if (reset)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.localPosition = inventoryPos;
            reset = false;
        }
    }

    void DecreaseDurability()
    {
        lifeTime--;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
            reset = false;
        }
    }

    private void OnMouseOver()
    {
        if(descriptionBox)
        {
            descriptionBox.gameObject.SetActive(true);
            descriptionBox.itemName.text = itembase.cardName;
            descriptionBox.itemDescription.text = itembase.cardDescription;
            descriptionBox.itemType.text = itembase.cardType;

            if(itembase is Weapon)
            {
                Weapon weapon = (Weapon)itembase;
                descriptionBox.weaponDamage.text = "Damage: " + weapon.damage.ToString();
                descriptionBox.weaponDurability.text = "Durability: " + lifeTime + " / " + weapon.lifetime;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            descriptionBox.gameObject.SetActive(false);
            theInv.RemoveFromInventory(inventoryIndex);
        }
        if (Input.GetMouseButtonDown(0) && itembase is Consumable)
        {
            Consumable consumable= (Consumable)itembase;
            consumable.Consume(FindObjectOfType<Player>());
            descriptionBox.gameObject.SetActive(false);
            theInv.RemoveFromInventory(inventoryIndex);
        }
    }

    private void OnMouseExit()
    {
        descriptionBox.gameObject.SetActive(false);
    }
}
