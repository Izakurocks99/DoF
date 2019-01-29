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
    public Vector2 boardIndex = Vector2.zero;
    CardFlip flipper = null;
    Player player = null;
    [SerializeField]
    CardType cardType = CardType.NONE;

    //public List<GameObject> enemyTokens;
    public GameObject enemyBase;
    public List<EnemyScriptableObj> enemyTokens;
    public GameObject biomeBase;
    public List<Biome> biomes;
    public Biome endZone;

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
            //player.inCombat = false;
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
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 1:
                    cardType = CardType.Enemy;
                    break;
                case 2:
                    cardType = CardType.Obstacle;
                    break;
                case 3:
                    cardType = CardType.NONE;
                    break;
                default:
                    cardType = CardType.NONE;
                    break;

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
        if (Input.GetMouseButtonDown(0) && board.CheckBesidePlayer(boardIndex) && !player.inCombat)
        {
            if (!flipper.flipped)
                flipper.Flip();
            else
            {
                player.MoveToBoardPos(boardIndex);
                //map get around and flip them
                //board
            }
        }
    }

    //bool CheckBesidePlayer()
    //{
    //    if (!player)
    //        return false;
    //    if (player.boardIndex + new Vector2(0,1) == boardIndex
    //        || player.boardIndex - new Vector2(0, 1) == boardIndex
    //        || player.boardIndex + new Vector2(1, 0) == boardIndex
    //        || player.boardIndex - new Vector2(1, 0) == boardIndex)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void Reveal()
    {
        int index;
        switch (cardType)
        {
            case CardType.Enemy:
                player.inCombat = true;
                index = Random.Range(0, enemyTokens.Count);
                enemyObj = Instantiate(enemyBase, board.BoardToWorldPos(boardIndex), Quaternion.identity);
                enemyObj.GetComponent<EnemyCard>().cardSO = enemyTokens[index];
                enemyObj.GetComponent<EnemyCard>().boardPos = boardIndex;
                enemyObj.GetComponent<EnemyCard>().board = board;
                revealed = true;
                break;
            case CardType.Obstacle:
                index = Random.Range(0, biomes.Count);
                enemyObj = Instantiate(biomeBase, board.BoardToWorldPos(boardIndex), Quaternion.identity);
                enemyObj.GetComponent<BiomeScript>().cardSO = biomes[index];
                enemyObj.GetComponent<BiomeScript>().boardPos = boardIndex;
                enemyObj.GetComponent<BiomeScript>().board = board;
                revealed = true;
                break;
            case CardType.Exit:
                enemyObj = Instantiate(biomeBase, board.BoardToWorldPos(boardIndex), Quaternion.identity);
                enemyObj.GetComponent<BiomeScript>().cardSO = endZone;
                enemyObj.GetComponent<BiomeScript>().boardPos = boardIndex;
                enemyObj.GetComponent<BiomeScript>().board = board;
                revealed = true;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (revealed && collision.gameObject.tag == "Player")
        {

        }
    }
}
