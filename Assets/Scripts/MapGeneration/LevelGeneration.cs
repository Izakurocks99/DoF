using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [SerializeField]
    Vector2 worldCenter = new Vector2(4, 4);
    Room[,] roomArray;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY;
    [SerializeField]
    int numberOfRooms = 20;
    [SerializeField]
    float cardSpacing = 0.1f;

    public GameObject roomObj;
    public GameObject playerPrefab;
    Player player;

    List<GameObject> rooms = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        if (numberOfRooms >= (worldCenter.x * 2) * (worldCenter.y * 2))
        {
            numberOfRooms = (int)((worldCenter.x * 2) * (worldCenter.y * 2));
        }

        gridSizeX = (int)worldCenter.x;
        gridSizeY = (int)worldCenter.y;

        CreateRooms();
        StartCoroutine(DrawMap());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateRooms()
    {
        roomArray = new Room[gridSizeX * 2, gridSizeY * 2];
        roomArray[gridSizeX, gridSizeY] = new Room(Vector2.zero, RoomTypes.Start);
        takenPositions.Add(Vector2.zero); //add starting room into takenpos
        Vector2 checkPos = Vector2.zero;

        //weights
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        for (int i = 0; i < numberOfRooms - 1; ++i)
        {
            //more chance to branch as i increases
            float randomPerc = (float)i / (float)numberOfRooms - 1f;
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //grab new position
            checkPos = NewPosition();
            //20% to branch out(using randomCompare) if more than one neighbour
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                checkPos = SelectiveNewPosition();
            }

            //add position into array
            roomArray[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, RoomTypes.Normal);
            takenPositions.Add(checkPos);
        }
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); //get a random taken pos
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //move a random directions
            float dir = Random.value;
            if (dir <= 0.25f)
                y += 1;
            else if (dir <= 0.5f)
                y -= 1;
            else if (dir <= 0.75f)
                x += 1;
            else
                x -= 1;

            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); // if position is taken or out of bounds find another position
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0;
        int inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos= Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100); // if there is more than one neighbour find again up to a max of 100x to prevent endless loops

            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //move a random directions
            float dir = Random.value;
            if (dir <= 0.25f)
                y += 1;
            else if (dir <= 0.5f)
                y -= 1;
            else if (dir <= 0.75f)
                x += 1;
            else
                x -= 1;

            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); // if position is taken or out of bounds find another position
        if (inc >= 100)
        { // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
            print("Error: could not find position with only one neighbor");
        }

        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0; // start at zero, add 1 for each side there is already a room
        if (usedPositions.Contains(checkingPos + Vector2.right))
        { //using Vector.[direction] as short hands, for simplicity
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    IEnumerator DrawMap()
    {
        foreach(Vector2 room in takenPositions)
        { 
            Vector2 drawPos = room;
            drawPos.x *= (roomObj.transform.lossyScale.x + cardSpacing);
            drawPos.y *= (roomObj.transform.lossyScale.y + cardSpacing);
            rooms.Add(Instantiate(roomObj, drawPos, Quaternion.identity));
            yield return new WaitForSeconds(0.1f);

        }

        if (!player)
        {
            GameObject go = Instantiate(playerPrefab, GetStartingPos(), Quaternion.identity);
            player = go.GetComponent<Player>();
            go.GetComponent<Player>().board = this;
        }
        else
        {
            player.gameObject.SetActive(true);
            player.transform.position = GetStartingPos();
        }
        player.boardIndex = Vector2.zero;

        for (int i =0;i < rooms.Count; ++i)
        {
            if (i == 0)
                rooms[i].GetComponent<CardBase>().InitCard(this, takenPositions[i], player, CardType.Entry);
            else if (i == rooms.Count - 1)
                rooms[i].GetComponent<CardBase>().InitCard(this, takenPositions[i], player, CardType.Exit);
            else
                rooms[i].GetComponent<CardBase>().InitCard(this, takenPositions[i], player, CardType.NONE);
        }

        yield break;

    }

    public Vector2 GetStartingPos()
    {
        return BoardToWorldPos(takenPositions[0]);
    }

    public Vector2 BoardToWorldPos(Vector2 gridPos)
    {
        Vector2 worldPos = gridPos;
        worldPos.x *= (roomObj.transform.lossyScale.x + cardSpacing);
        worldPos.y *= (roomObj.transform.lossyScale.y + cardSpacing);
        return worldPos;
    }

    public void ResetBoard()
    {
        player.gameObject.SetActive(false);
        foreach (GameObject var in rooms)
        {
            Destroy(var);
        }
        takenPositions.Clear();
        rooms.Clear();
        CreateRooms();
        StartCoroutine(DrawMap());
    }
}
