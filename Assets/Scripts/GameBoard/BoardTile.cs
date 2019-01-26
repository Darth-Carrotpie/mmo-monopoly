using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public int address;
    public string type;
    public BoardTileType typeId;
    public BoardTileProperties properties;

    public TextMesh nameMesh;
    public TextMesh priceMesh;

    public BoardTile(BoardTileData n, int index){
        //Debug.Log(index);
        address = index;
        type = n.type;
        typeId = n.typeId;
        properties.cost = n.properties.cost;
        properties.color = n.properties.color;
    }
    public void Init(BoardTile n){
        //Debug.Log(n.type);
        address = n.address;
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
