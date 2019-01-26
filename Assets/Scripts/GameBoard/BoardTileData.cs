using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileData : MonoBehaviour
{
    public BoardTileType type;
    public BoardTileProperties properties;

    public BoardTileData(BoardTile n)
    {
        type = n.type;
        properties = n.properties;
    }
}
