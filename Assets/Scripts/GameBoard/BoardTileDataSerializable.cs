using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoardTileDataSerializable
{
    public List<BoardTileData> nodes = new List<BoardTileData>();
    public void Import(BoardTile[] nArray)
    {
        foreach (BoardTile n in nArray)
        {
            nodes.Add(new BoardTileData(n));
            Debug.Log(n.properties.color);
        }
    }
    public BoardTile[] Export()
    {
        List<BoardTile> ns = new List<BoardTile>();
        foreach (BoardTileData n in nodes)
        {
            ns.Add(new BoardTile(n));
        }
        return ns.ToArray();
    }
}
