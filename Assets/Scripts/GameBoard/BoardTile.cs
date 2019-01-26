﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{

    public BoardTileType type;
    public BoardTileProperties properties;

    public BoardTile(BoardTileData n){
        type = n.type;
        properties = n.properties;
    }
}

public enum BoardTileType {
    Street,
    Empty,
    Go
}
public struct BoardTileProperties {
    public int cost;
    public string color;
}