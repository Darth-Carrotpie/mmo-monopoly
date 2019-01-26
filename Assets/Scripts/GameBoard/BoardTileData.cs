using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoardTileData
{
    public string type;
    public BoardTileType typeId;
    public Messages.TileProperties properties;

    public BoardTileData(BoardTile n)
    {
        type = n.type;
        typeId = n.typeId;
        properties = new Messages.TileProperties();
        properties.color = n.color;
        properties.cost = n.cost;
    }
    public class Properties{

    }
}
