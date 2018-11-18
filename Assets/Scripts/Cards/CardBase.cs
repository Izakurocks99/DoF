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

    Board board = null;
    int boardIndex = 0;
    CardFlip flipper = null;
    Player player = null;
    [SerializeField]
    CardType cardType = CardType.NONE;

    public List<GameObject> enemyTokens;

    // Use this for initialization
    void Start()
    {
        flipper = GetComponent<CardFlip>();
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitCard(Board board, int boardindex, Player player, CardType type)
    {
        this.board = board;
        this.player = player;
        boardIndex = boardindex;
        cardType = type;

        if(cardType == CardType.NONE)
        {
            int index = Random.Range(0, 2);
            if(index == 1)
            {
                cardType = CardType.Enemy;
            }
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
        if (player.boardIndex + board.boardSize.y == boardIndex
            || player.boardIndex - board.boardSize.y == boardIndex
            || player.boardIndex + 1 == boardIndex
            || player.boardIndex - 1 == boardIndex)
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
                Instantiate(enemyTokens[index],board.CardPositionList[boardIndex],Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
