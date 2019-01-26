using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoardTileDataSerializable
{
    public List<BoardTileData> board = new List<BoardTileData>();
    public void Import(BoardTile[] nArray)
    {
        foreach (BoardTile n in nArray)
        {
            board.Add(new BoardTileData(n));
            Debug.Log(n.properties.color);
        }
    }
    public BoardTile[] Export()
    {
        Debug.Log(board[0]);
        List<BoardTile> ns = new List<BoardTile>();
        foreach (BoardTileData n in board)
        {
            ns.Add(new BoardTile(n));
        }
        return ns.ToArray();
    }
}
