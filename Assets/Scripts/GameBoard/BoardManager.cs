using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<BoardTile> tiles = new List<BoardTile>();
    public GameObject tilePrefab;
    public int rangeForward = 30;
    BoardNetwork boardNetwork;
    PlayersManager playersManager;
    void Awake()
    {
        boardNetwork = FindObjectOfType<BoardNetwork>();
        playersManager = FindObjectOfType<PlayersManager>();
        EventManager.StartListening(EventName.System.UpdateBoard(), UpdateBoard);
        //UpdateBoard(GameMessage.Write());
    }

    public void UpdateBoard(GameMessage msg){
        //Debug.Log("Updating Board");
        //clear tiles who are behind player by more than 1
        if (playersManager.mainPlayer == null)
            return;
        for(int i = tiles.Count-1; i >= 0; i--){
            if (tiles[i].transform.position.z < playersManager.mainPlayer.tileAddress-13){
                //Debug.Log("destroying: "+i);
                Destroy(tiles[i].gameObject);
                tiles.RemoveAt(i);
            }
        }
        //create new tiles
        Debug.Log("creating");
        int lastIndex = GetLastIndex();
        int lastAddress = GetLastAdress();
        for (int i = 0; i < rangeForward - lastAddress +  playersManager.mainPlayer.tileAddress; i++){
            GameObject newTile = Instantiate(tilePrefab, transform);
            BoardTile bt = newTile.GetComponent<BoardTile>();
            bt.Init(boardNetwork.tiles[lastAddress+i]);
            newTile.transform.position = new Vector3(0, 0, i+lastIndex);
            tiles.Add(bt);
        }
    }
    public int GetLastAdress(){
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
        //Debug.Log("last"+index);
        return index;
    }
}
