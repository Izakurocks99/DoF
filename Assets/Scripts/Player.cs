using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public bool godMode = false;
    public Vector2 boardIndex;
    public bool inCombat;
    public bool dead;
    float deadMovePercent = 0;
    [SerializeField]
    Transform deadTransform;

    [SerializeField]
    float moveSpeed = 0f;
    bool moving = false;
    float movepercent = 0;
    Vector3 endPos;
    BoxCollider collisionBox;

    [SerializeField]
    PlayerHand hand;

    [SerializeField]
    TextMeshPro playerText;
    [SerializeField]
    string winText;

    public bool isPlayerTurn = true;

    // Use this for initialization
    void Start()
    {
        //currHunger = maxHunger;
        //currHealthPoints = maxHealthPoints;
        //currManaPoints = maxManaPoints;

        //Move to start
        if(board)
        gameObject.transform.position = board.GetStartingPos();

        collisionBox = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        if (board)
            gameObject.transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            Move();

        if(dead)
        {
            GetComponent<Rigidbody>().useGravity = false;
            //ModifyHealth(-currHealthPoints);
            if (deadMovePercent < 1)
            {
                deadMovePercent += Time.deltaTime;
                Vector3 start = board.BoardToWorldPos(boardIndex);
                DieAnim(start, deadTransform.position+new Vector3(0,0,2), deadMovePercent);
            }
        }
    }

    public void ModifyHealth(float value)
    {
        if (godMode)
            return;
        currHealthPoints += value;
        if (currHealthPoints > maxHealthPoints)
        {
            currHealthPoints = maxHealthPoints;
        }
        else if (currHealthPoints <= 0)
        {
            currHealthPoints = 0;
            dead = true;
            GetComponent<Rigidbody>().useGravity = false;
            deadTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
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
            board.FlipSurroundingRooms();
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

    void DieAnim(Vector3 startPos, Vector3 endPos, float percentage)
    {
        transform.position = Vector3.Lerp(startPos, endPos, percentage);

        Vector3 startAngle = new Vector3(0, 0, 0);
        Vector3 targetAngle = new Vector3(0, 180, 0);
        Vector3 lerpAngle = new Vector3(
             Mathf.LerpAngle(startAngle.x, targetAngle.x, percentage),
             Mathf.LerpAngle(startAngle.y, targetAngle.y, percentage),
             Mathf.LerpAngle(startAngle.z, targetAngle.z, percentage));
        transform.eulerAngles = lerpAngle;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && dead)
        {
            //ModifyHealth(maxHealthPoints);
            //dead = false;
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
            board.ResetBoard(0);
            board.inventory.ResetInventory();
        }
    }

    public void WinGame()
    {
        dead = true;
        GetComponent<Rigidbody>().useGravity = false;
        deadTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerText.text = winText;
    }

    public void PlayDamagedAnim()
    {
        Debug.Log("Player damaged anim");
    }

    public void PlayAttackAnim()
    {
        Debug.Log("Player attack anim");
    }
}
