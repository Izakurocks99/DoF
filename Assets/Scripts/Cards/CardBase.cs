using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour {
    
    public Board board = null;

    public int boardIndex = 0;
    CardFlip flipper = null;
    public Player player = null;
    // Use this for initialization
    void Start () {
        flipper = GetComponent<CardFlip>();
        board = FindObjectOfType<Board>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate()
    {
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && CheckBesidePlayer())
        {
            if (!flipper.flipped)
                flipper.Flip();
            else
                player.MoveToBoardPos(boardIndex);
        }
    }

    bool CheckBesidePlayer()
    {
        if(player.boardIndex + board.boardSize.y == boardIndex 
            || player.boardIndex - board.boardSize.y == boardIndex
            || player.boardIndex + 1 == boardIndex
            || player.boardIndex -1 == boardIndex)
        {
            return true;
        }
        return false;
    }
}
