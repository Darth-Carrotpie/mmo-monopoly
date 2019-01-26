using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<BoardTile> tiles = new List<BoardTile>();
    public GameObject tilePrefab;
    public int rangeForward = 5;
    BoardNetwork boardNetwork;
    PlayerNetwork playerNetwork;
    void Start()
    {
        boardNetwork = FindObjectOfType<BoardNetwork>();
        playerNetwork = FindObjectOfType<PlayerNetwork>();
        EventManager.StartListening(EventName.System.UpdateBoard(), UpdateBoard);
    }

    public void UpdateBoard(GameMessage msg){
        //clear tiles who are behind player by more than 1
        for(int i = tiles.Count; i > 0; i--){
            if (tiles[i].transform.position.z < 0){
                Destroy(tiles[i].gameObject);
                tiles.RemoveAt(i);
            }
        }
        //create new tiles
        for (int i = tiles.Count; i < rangeForward; i++){
            GameObject newTile = Instantiate(tilePrefab, transform);
            BoardTile bt = newTile.GetComponent<BoardTile>();
            Debug.Log(bt.type);
            Debug.Log(playerNetwork.player.tileAddress+i);
            Debug.Log(boardNetwork.tiles[playerNetwork.player.tileAddress+i].type);
            bt.Init(boardNetwork.tiles[playerNetwork.player.tileAddress+i]);
            newTile.transform.position = new Vector3(0, 0, i);
            tiles.Add(newTile.GetComponent<BoardTile>());
        }
    }
}
