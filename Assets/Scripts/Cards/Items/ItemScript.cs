using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public Item itembase;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 inventoryPos;

    public bool selected;

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

    public void Init(Vector3 pos)
    {
        inventoryPos = pos;
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
            Item result;
            //get other component
            Item other = collision.gameObject.GetComponent<ItemScript>().itembase;
            //cast to material type
            Material material = (Material)itembase;
            //use weapon
            material.Craft(other,out result);
            //Return to hand
            transform.localPosition = inventoryPos;
            //create from result
        }
    }
}
