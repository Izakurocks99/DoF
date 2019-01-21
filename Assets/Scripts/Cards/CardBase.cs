using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    NONE,
    Enemy,
    Obstacle,
    Shop,
    Entry,
    Exit,
}

public class CardBase : MonoBehaviour
{

    LevelGeneration board = null;
    [SerializeField]
    Vector2 boardIndex = Vector2.zero;
    CardFlip flipper = null;
    Player player = null;
    [SerializeField]
    CardType cardType = CardType.NONE;

    //public List<GameObject> enemyTokens;
    public GameObject enemyBase;
    public List<EnemyScriptableObj> enemyTokens;

    GameObject enemyObj;
    bool revealed = false;
    // Use this for initialization
    void Start()
    {
        flipper = GetComponent<CardFlip>();
        board = FindObjectOfType<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cardType == CardType.Enemy && revealed && !enemyObj)
        {
            cardType = CardType.NONE;
            player.inCombat = false;
        }
    }

    public void InitCard(LevelGeneration board, Vector2 boardindex, Player player, CardType type)
    {
        this.board = board;
        this.player = player;
        boardIndex = boardindex;
        cardType = type;

        if (cardType == CardType.NONE)
        {
            int index = Random.Range(0, 2);
            if (index == 1)
            {
                cardType = CardType.Enemy;
            }
        }

        if (cardType == CardType.Entry)
        {
            flipper.flipped = true;
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && CheckBesidePlayer() && !player.inCombat)
        {
            if (!flipper.flipped)
                flipper.Flip();
            else
                player.MoveToBoardPos(boardIndex);
        }
    }

    bool CheckBesidePlayer()
    {
        if (!player)
            return false;
        if (player.boardIndex + new Vector2(0,1) == boardIndex
            || player.boardIndex - new Vector2(0, 1) == boardIndex
            || player.boardIndex + new Vector2(1, 0) == boardIndex
            || player.boardIndex - new Vector2(1, 0) == boardIndex)
        {
            return true;
        }
        return false;
    }

    public void Reveal()
    {
        switch (cardType)
        {
            case CardType.Enemy:
                player.inCombat = true;
                int index = Random.Range(0, enemyTokens.Count);
                enemyObj = Instantiate(enemyBase, board.BoardToWorldPos(boardIndex), Quaternion.identity);
                enemyObj.GetComponent<EnemyCard>().cardSO = enemyTokens[index];
                revealed = true;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(cardType == CardType.Exit && collision.gameObject.tag == "Player")
        {
            board.ResetBoard();
        }
    }
}
