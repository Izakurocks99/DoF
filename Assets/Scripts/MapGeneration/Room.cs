using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomTypes
{
    Start,
    Normal,
    End,
}

public class Room{
    public Vector2 gridPos;
    public RoomTypes type;
    public Room(Vector2 _roomPos,RoomTypes _type)
    {
        gridPos = _roomPos;
        type = _type;
    }
}
