using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Board board = null;
    [SerializeField]
    Image healthBar = null;
    [SerializeField]
    Image manaBar = null;


    [SerializeField] float maxHunger;
    [SerializeField] float currHunger;
    [SerializeField] float maxHealthPoints;
    [SerializeField] float currHealthPoints;
    [SerializeField] float maxManaPoints;
    [SerializeField] float currManaPoints;

    public int boardIndex;
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

        UpdateHealthBar();
        UpdateManaBar();

        collisionBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            Move();
    }

    public void TakeDamage(float damage)
    {
        currHealthPoints -= damage;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (currHealthPoints > maxHealthPoints)
        {
            currHealthPoints = maxHealthPoints;
        }
        else if (currHealthPoints < 0)
        {
            currHealthPoints = 0;
        }
        healthBar.fillAmount = currHealthPoints / maxHealthPoints;
    }

    public bool CastSpell(float manaCost)
    {
        if (currManaPoints >= manaCost)
        {

            currManaPoints -= manaCost;
            UpdateManaBar();
            return true;
        }
        return false;
    }

    void UpdateManaBar()
    {
        if (currManaPoints > maxManaPoints)
        {
            currManaPoints = maxManaPoints;
        }
        else if (currManaPoints < 0)
        {
            currManaPoints = 0;
        }
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
