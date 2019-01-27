using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<BoardTile> tiles = new List<BoardTile>();
    public GameObject tilePrefab;
    public int rangeForward = 30;
    BoardNetwork boardNetwork;
    PlayerNetwork playerNetwork;
    void Awake()
    {
        boardNetwork = FindObjectOfType<BoardNetwork>();
        playerNetwork = FindObjectOfType<PlayerNetwork>();
        EventManager.StartListening(EventName.System.UpdateBoard(), UpdateBoard);
        //UpdateBoard(GameMessage.Write());
    }

    public void UpdateBoard(GameMessage msg){
        Debug.Log("Updating Board");
        //clear tiles who are behind player by more than 1
        for(int i = tiles.Count-1; i >= 0; i--){
            if (tiles[i].transform.position.z < 0){
                //Debug.Log("destroying: "+i);
                Destroy(tiles[i].gameObject);
                tiles.RemoveAt(i);
            }
        }
        //create new tiles
        Debug.Log("creating");
        int lastIndex = GetLastIndex();
        int lastAddress = GetLastAdress();
           // Debug.Log(lastAddress);
        for (int i = 0; i < rangeForward-lastIndex; i++){
            GameObject newTile = Instantiate(tilePrefab, transform);
            BoardTile bt = newTile.GetComponent<BoardTile>();
            //Debug.Log(bt.type);
            //Debug.Log(playerNetwork.player.tileAddress+i);
            bt.Init(boardNetwork.tiles[lastAddress+i]);
            newTile.transform.position = new Vector3(0, 0, i+lastIndex);
            tiles.Add(bt);
        }
    }
    int GetLastAdress(){
        int address = 0;
        for (int i = 0; i < tiles.Count; i ++){
            int z = (int)tiles[i].address;
            if (z > address){
                address = z;
            }
        }
        return address;
    }
    int GetLastIndex(){
        int index = tiles.Count;
        if (index>0){
            index = 0;
            for (int i = 0; i < tiles.Count; i ++){
                int z = (int)tiles[i].transform.position.z;
                if (z > index){
                    index = z;
                }
            }
            return index;
        }
        Debug.Log("last"+index);
        return index;
    }
}
