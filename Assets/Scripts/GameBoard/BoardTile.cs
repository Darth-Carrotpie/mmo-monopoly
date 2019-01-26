using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public string type;
    public BoardTileType typeId;
    public BoardTileProperties properties;

    public BoardTile(BoardTileData n){
        Debug.Log(n.type);
        type = n.type;
        typeId = n.typeId;
        properties.cost = n.properties.cost;
        properties.color = n.properties.color;
    }
    public void Init(BoardTile n){
        Debug.Log(n.type);
        type = n.type;
        typeId = n.typeId;
        properties.cost = n.properties.cost;
        properties.color = n.properties.color;
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