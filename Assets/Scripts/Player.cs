using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public LevelGeneration board = null;

    //Player stats
    public float maxHunger;
    public float currHunger;
    public float maxHealthPoints;
    public float currHealthPoints;
    public float maxManaPoints;
    public float currManaPoints;

    public Vector2 boardIndex;
    public bool inCombat;

    [SerializeField]
    float moveSpeed = 0f;
    bool moving = false;
    float movepercent = 0;
    Vector3 endPos;
    BoxCollider collisionBox;

    [SerializeField]
    PlayerHand hand;

    // Use this for initialization
    void Start()
    {
        //currHunger = maxHunger;
        //currHealthPoints = maxHealthPoints;
        //currManaPoints = maxManaPoints;

        //Move to start
        gameObject.transform.position = board.GetStartingPos();

        collisionBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            Move();
    }

    public void ModifyHealth(float value)
    {
        currHealthPoints += value;
        if (currHealthPoints > maxHealthPoints)
        {
            currHealthPoints = maxHealthPoints;
        }
        else if (currHealthPoints < 0)
        {
            currHealthPoints = 0;
        }
    }

    public bool CastSpell(float manaCost)
    {
        if (currManaPoints >= manaCost) //can cast
        {

            ModifyMana(-manaCost);
            return true;
        }
        return false; //cannot cast
    }

    public void ModifyMana(float value)
    {
        currManaPoints += value;
        if (currManaPoints > maxManaPoints)
        {
            currManaPoints = maxManaPoints;
        }
        else if (currManaPoints < 0)
        {
            currManaPoints = 0;
        }
    }

    public void MoveToBoardPos(Vector2 boardPos)
    {
        if (!moving)
        {
            boardIndex = boardPos;
            endPos = board.BoardToWorldPos(boardIndex);
            moving = true;
            collisionBox.enabled = false;
            //hand.drawCard = true;
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
