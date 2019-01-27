using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoardTileData
{
    public string boardName;
    public string type;
    public Messages.TileType typeId;
    public Messages.TileProperties properties;

    public BoardTileData(BoardTile n)
    {
        boardName = n.boardName;
        type = n.type;
        typeId = n.typeId;
        properties = new Messages.TileProperties();
        properties.color = n.color;
        properties.cost = n.cost;
    }
}
