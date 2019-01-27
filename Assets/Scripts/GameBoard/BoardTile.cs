using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class BoardTile : MonoBehaviour
{
    public string boardName;
    public int address;
    public string type;
    public Messages.TileType typeId;
    public int cost;
    public string color;

    public TextMeshPro nameMesh;
    public TextMeshPro priceMesh;
    public Renderer coloredHeader;

    public BoardTile(BoardTileData n, int index){
        //Debug.Log(index);
        boardName = n.boardName;
        address = index;
        type = n.type;
        typeId = n.typeId;
        cost = n.properties.cost;
        color = n.properties.color;
    }
    public BoardTile(Messages.Tile n, int index){
        //Debug.Log(index);
        boardName = "";
        address = index;
        type = n.type;
        typeId = n.typeId;
        cost = n.properties.cost;
        color = n.properties.color;
    }
    public void Init(BoardTile n){
        //Debug.Log(n.type);
        boardName = n.boardName;
        address = n.address;
        type = n.type;
        typeId = n.typeId;
        cost = n.cost;
        color = n.color;
        nameMesh.SetText(boardName);
        priceMesh.SetText("$"+cost);
        coloredHeader.material.color = FindObjectOfType<TileColorGenerator>().GetColorValue(color);
    }
}