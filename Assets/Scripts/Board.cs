using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //exit pos index not set
    //spawned cards no board index set

    [SerializeField] float cardSpacing = 0;
    [SerializeField] public Vector2 boardSize = Vector2.zero;
    [SerializeField] GameObject cardGO = null;
    [SerializeField] GameObject playerCardGO = null;

    Vector2 cardSize = Vector2.zero;
    Vector2 startposition = Vector2.zero;
    float totalWidth, totalHeight;

    public List<Vector2> CardPositionList;

    [SerializeField]
    int entryPosIndex, exitPosIndex;

    [SerializeField]
    int defaultWeight = 0, forwardWeight = 0; //moving forward is lower so that more cards will spawn

    // Use this for initialization
    void Start()
    {
        //initialize list
        CardPositionList = new List<Vector2>();

        cardSize = new Vector2(cardGO.transform.localScale.x + (cardSpacing * 2), cardGO.transform.localScale.y + (cardSpacing * 2));

        //get the total width and height
        totalWidth = (cardGO.transform.localScale.x + (cardSpacing * 2)) * boardSize.x;
        totalHeight = (cardGO.transform.localScale.y + (cardSpacing * 2)) * boardSize.y;
        //create from left to right
        for (int y = 0; y < boardSize.y; ++y)
        {
            for (int x = 0; x < boardSize.x; ++x)
            {
                startposition = (Vector2)transform.position + new Vector2(-totalWidth * 0.5f + cardSize.x * 0.5f,
                                            -totalHeight * 0.5f + cardSize.y * 0.5f); //- half of width/height + cardsize

                Vector2 cardPos = startposition + new Vector2(x * cardSize.x, y * cardSize.y); //get pos using x and y
                //add position into list
                CardPositionList.Add(cardPos);

            }
        }

        //get entry index
        entryPosIndex = Random.Range(0, (int)boardSize.x);
        GameObject go = Instantiate(cardGO, CardPositionList[entryPosIndex], Quaternion.identity); //spawn card
        CardBase cardBase = go.GetComponent<CardBase>();
        cardBase.InitCard(this, entryPosIndex, playerCardGO.GetComponent<Player>(), CardType.Entry);
        //move to starting point and start creating path
        transform.position = (CardPositionList[entryPosIndex]);
        //MovePlayer
        if (playerCardGO)
        {
            playerCardGO.transform.position = transform.position + new Vector3(0, 0, -1);
            playerCardGO.SetActive(true);

            Player player = playerCardGO.GetComponent<Player>();
            player.board = this;
            player.boardIndex = entryPosIndex;
        }
        else
            Debug.Log("PlayerCard is missing");
        StartCoroutine(CreatePath(entryPosIndex));
    }


    IEnumerator CreatePath(int entryIndex)
    {
        int cardIndex = entryIndex;
        int leftDirWeight = defaultWeight, rightDirWeight = defaultWeight;
        int dir;
        CardBase lastCard = null;

        while (true)
        {
            dir = Random.Range(0, leftDirWeight + rightDirWeight + forwardWeight); //get random from weights
            if (dir < leftDirWeight)
            {
                if (MoveHorizontal(false))
                {
                    //set down weight to 0 so that you dont move backwards
                    cardIndex -= 1;
                    rightDirWeight = 0;
                }
                else //go right
                {
                    //check if right got space
                    if (MoveVertical(true))
                    {
                        cardIndex += (int)boardSize.x;
                        //reset weights
                        rightDirWeight = leftDirWeight = defaultWeight;
                    }
                    else
                    {
                         break;
                    }
                }
            }
            else if(dir < rightDirWeight + leftDirWeight)
            {
                if (MoveHorizontal(true))
                {
                    //set up weight to 0 so that you dont move backwards
                    cardIndex += 1;
                    leftDirWeight = 0;
                }
                else //go right
                {
                    //check if right got space
                    if (MoveVertical(true))
                    {
                        cardIndex += (int)boardSize.x;
                        //reset weights
                        rightDirWeight = leftDirWeight = defaultWeight;
                    }
                    else
                    {
                         break;
                    }
                }
            }
            else
            {
                if (MoveVertical(true))
                {
                    cardIndex += (int)boardSize.x;
                    //reset weights
                    rightDirWeight = leftDirWeight = defaultWeight;
                }
                else
                {
                     break;
                }
            }

            GameObject go = Instantiate(cardGO, transform.position, Quaternion.identity);
            CardBase cardBase = go.GetComponent<CardBase>();
            cardBase.InitCard(this, cardIndex, playerCardGO.GetComponent<Player>(), CardType.NONE);
            lastCard = cardBase;
            yield return new WaitForSeconds(0.25f);
        }
        lastCard.InitCard(this, cardIndex, playerCardGO.GetComponent<Player>(), CardType.Exit);
        yield break;
    }

    bool MoveVertical(bool up) //returns true if there is space
    {
        if (up)
        {
            if (transform.position.y + cardSize.y < totalHeight * 0.5f)
            {
                transform.position = transform.position + new Vector3(0, cardSize.y);
                return true;
            }
        }
        else
        {

            if (transform.position.y - cardSize.y > -totalHeight * 0.5f)
            {
                transform.position = transform.position + new Vector3(0, -cardSize.y);
                return true;
            }
        }
        return false;
    }

    bool MoveHorizontal(bool right) //returns true if there is space
    {
        if (right)
        {
            if (transform.position.x + cardSize.x < totalWidth * 0.5f)
            {
                transform.position = transform.position + new Vector3(cardSize.x, 0);
                return true;
            }
        }
        else
        {

            if (transform.position.x - cardSize.x > -totalWidth * 0.5f)
            {
                transform.position = transform.position + new Vector3(-cardSize.x, 0);
                return true;
            }
        }
        return false;
    }
}
