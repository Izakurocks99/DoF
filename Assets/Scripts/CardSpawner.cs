using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour {

    [SerializeField] float cardSpacing;
    [SerializeField] Vector2 boardSize;
    [SerializeField] GameObject cardGO;
    [SerializeField] GameObject combatCardGO;

    Vector2 cardSize = Vector2.zero;
    Vector2 startposition = Vector2.zero;
    float totalWidth, totalHeight;
    //float maxBoardX, moxBoardY, minBoardY;

    List<Vector2> CardPositionList;

    [SerializeField]
    int entryPosIndex, exitPosIndex;

    [SerializeField]
    int defaultWeight, forwardWeight; //moving forward is lower so that more cards will spawn

    // Use this for initialization
    void Start () {
        //initialize list
        CardPositionList = new List<Vector2>();

        cardSize = new Vector2(cardGO.transform.localScale.x +(cardSpacing * 2), cardGO.transform.localScale.y + (cardSpacing * 2));

        //get the total width and height
        totalWidth = (cardGO.transform.localScale.x + (cardSpacing * 2)) * boardSize.x;
        totalHeight = (cardGO.transform.localScale.y + (cardSpacing * 2)) * boardSize.y;
        //create from left to right
        for (int x = 0; x < boardSize.x; ++x)
        {
            for (int y = 0; y < boardSize.y; ++y)
            {
                startposition = new Vector2(-totalWidth * 0.5f + cardSize.x * 0.5f, -totalHeight * 0.5f + cardSize.y * 0.5f); //- half of width/height + cardsize
                Vector2 cardPos = startposition + new Vector2(x * cardSize.x, y * cardSize.y); //get pos using x and y
                //add position into list
                CardPositionList.Add(cardPos);
                
            }
        }

        //get entry index
        entryPosIndex = Random.Range(0, (int)boardSize.y);
        Instantiate(combatCardGO, CardPositionList[entryPosIndex], Quaternion.identity); //spawn card

        //move to starting point and start creating path
        transform.position = (CardPositionList[entryPosIndex]);
        StartCoroutine(CreatePath());
    }


    IEnumerator CreatePath()
    {
        Vector2 newPos;
        int upDirWeight = defaultWeight, downDirWeight = defaultWeight;
        int dir;

        while (true)
        {
            dir = Random.Range(0, upDirWeight + downDirWeight + forwardWeight); //get random from weights
            if (dir < upDirWeight)//if can go up go up
            {
                //check if up got spawn
                if (transform.position.y + cardSize.y < totalHeight * 0.5f)
                {
                    Debug.Log("UP");
                    newPos = new Vector2(transform.position.x, transform.position.y + cardSize.y);
                    transform.position = newPos;
                    //set down weight to 0 so that you dont move backwards
                    downDirWeight = 0;
                }
                else //go right
                {
                    //check if right got space
                    if (transform.position.x + cardSize.x < totalWidth * 0.5f)
                    {
                        Debug.Log("RIGHT");
                        newPos = new Vector2(transform.position.x + cardSize.x, transform.position.y);
                        transform.position = newPos;
                        //reset weights
                        downDirWeight = upDirWeight = defaultWeight;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
            else if (dir < upDirWeight + downDirWeight) //go down
            {
                //check if down got space
                if (transform.position.y - cardSize.y > -totalHeight * 0.5f)
                {
                    Debug.Log("DOWN");
                    newPos = new Vector2(transform.position.x, transform.position.y - cardSize.y);
                    transform.position = newPos;
                    //set up weight to 0 so that you dont move backwards
                    upDirWeight = 0;
                }
                else //go right
                {
                    //check if right got space
                    if (transform.position.x + cardSize.x < totalWidth * 0.5f)
                    {
                        Debug.Log("RIGHT");
                        newPos = new Vector2(transform.position.x + cardSize.x, transform.position.y);
                        transform.position = newPos;
                        //reset weights
                        downDirWeight = upDirWeight = defaultWeight;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
            else //go right
            {
                //check if right got space
                if (transform.position.x + cardSize.x < totalWidth * 0.5f)
                {
                    Debug.Log("RIGHT");
                    newPos = new Vector2(transform.position.x + cardSize.x, transform.position.y );
                    transform.position = newPos;
                    //reset weights
                    downDirWeight = upDirWeight = defaultWeight;
                }
                else
                {
                    Instantiate(combatCardGO, CardPositionList[entryPosIndex], Quaternion.identity); //spawn card
                    yield break;
                }
            }

            Instantiate(cardGO, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
