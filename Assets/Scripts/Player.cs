using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Board board = null;
    [SerializeField]
    Image healthBar = null;
    [SerializeField]
    Image manaBar = null;


    public float maxHunger;
    public float currHunger;
           
    public float maxHealthPoints;
    public float currHealthPoints;
           
    public float maxManaPoints;
    public float currManaPoints;

    public int boardIndex;

    [SerializeField]
    float moveSpeed = 0f;
    bool moving = false;
    float movepercent = 0;
    Vector3 endPos;
    BoxCollider collisionBox;

    // Use this for initialization
    void Start () {
        //currHunger = maxHunger;
        //currHealthPoints = maxHealthPoints;
        //currManaPoints = maxManaPoints;

        UpdateHealthBar();
        UpdateManaBar();

        collisionBox = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
            Move();
	}

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currHealthPoints / maxHealthPoints;
    }
    
    void UpdateManaBar()
    {
        manaBar.fillAmount = currManaPoints / maxManaPoints;
    }

    public void MoveToBoardPos(int boardPos)
    {
        if (!moving)
        {
            boardIndex = boardPos;
            endPos = board.CardPositionList[boardIndex];
            moving = true;
            collisionBox.enabled = false;
        }
    }

    void Move()
    {
        movepercent += moveSpeed * Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, movepercent);
        if (movepercent >= 1f)
        {
            collisionBox.enabled = true;
            movepercent = 0;
            moving = false;
        }
    }
}
