using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoardTileData
{
    public string type;
    public BoardTileType typeId;
    public BoardTileProperties properties;

    public BoardTileData(BoardTile n)
    {
        type = n.type;
        typeId = n.typeId;
        properties = n.properties;
    }
}
