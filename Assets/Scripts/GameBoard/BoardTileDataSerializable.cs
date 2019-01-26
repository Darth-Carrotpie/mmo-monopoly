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
        List<BoardTile> ns = new List<BoardTile>();
        for (int i = 0; i < board.Count;i++)
        {
            ns.Add(new BoardTile(board[i], i));
        }
        return ns.ToArray();
    }
}
