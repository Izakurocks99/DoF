using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [SerializeField]
    Vector2 worldSize = new Vector2(4, 4);
    Room[,] roomArray;
    List<Vector2> takenPositions = new List<Vector2>();
    public int gridSizeX, gridSizeY, numberOfRooms =20;

    public GameObject roomObj;

    // Use this for initialization
    void Start()
    {
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = (int)((worldSize.x * 2) * (worldSize.y * 2));
        }

        gridSizeX = (int)worldSize.x;
        gridSizeY = (int)worldSize.y;

        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateRooms()
    {
        roomArray = new Room[gridSizeX * 2, gridSizeY * 2];
        roomArray[gridSizeX, gridSizeY] = new Room(Vector2.zero, RoomTypes.Start);
        takenPositions.Insert(0, Vector2.zero); //add starting room into takenpos
        Vector2 checkPos = Vector2.zero;

        //weights
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        for (int i = 0; i < numberOfRooms - 1; ++i)
        {
            float randomPerc = (float)i / (float)numberOfRooms - 1f;
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //grab new position
            checkPos = NewPosition();
            //20% to branch out(using randomCompare) if more than one neighbour
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                //TODO: Not sure why needs to iterate here, check
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }

            //add position into array
            roomArray[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, RoomTypes.Normal);
            takenPositions.Insert(0, checkPos);
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

    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (roomArray[x, y] == null)
                {
                    continue;
                }
                if (y - 1 < 0)
                { //check above
                    roomArray[x, y].doorBot = false;
                }
                else
                {
                    roomArray[x, y].doorBot = (roomArray[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    roomArray[x, y].doorTop = false;
                }
                else
                {
                    roomArray[x, y].doorTop = (roomArray[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    roomArray[x, y].doorLeft = false;
                }
                else
                {
                    roomArray[x, y].doorLeft = (roomArray[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    roomArray[x, y].doorRight = false;
                }
                else
                {
                    roomArray[x, y].doorRight = (roomArray[x + 1, y] != null);
                }
            }
        }
    }

    void DrawMap()
    {
        foreach(Room room in roomArray)
        {
            if(room == null)
            {
                continue;
            }

            Vector2 drawPos = room.gridPos;
            drawPos.x *= roomObj.transform.lossyScale.x;
            drawPos.y *= roomObj.transform.lossyScale.y;
            Instantiate(roomObj, drawPos, Quaternion.identity);
        }
    }
}
