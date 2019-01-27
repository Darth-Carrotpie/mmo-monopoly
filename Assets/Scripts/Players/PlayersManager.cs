using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public Player mainPlayer;
    public List<Player> players;
    public GameObject playerPrefab;
    public GameObject[] playerModel;

    void Awake()
    {
        EventManager.StartListening(EventName.Player.PlayerState(), NewPlayerState);
        EventManager.StartListening(EventName.Player.SetMainPlayer(), SetMainPlayer);
    }

    void NewPlayerState(GameMessage msg){
        int id = msg.id;
        int position = msg.position;
        bool playerPresent = false;
        for (int i = 0; i < players.Count; i++){
            if (id == players[i].id ){
                if (id == mainPlayer.id)
                {
                //Debug.Log("SetMainPlayer NewPlayerState");
                    playerPresent = true;
                    break;
                    
                }
                playerPresent = true;
                break;
            }
        }
        if (!playerPresent){
            //Debug.Log("Creating player: "+id);
            Player pl = CreatePlayer(msg);
            players.Add(pl);
            pl.GetComponent<PlayerHop>().SetUp();
        } else {
            if(mainPlayer.tileAddress - 12 > position || mainPlayer.tileAddress + 45 < position){
                DestroyPlayer(id);
            }
        }
    }

    void SetMainPlayer(GameMessage msg){
        if (mainPlayer == null ){
            Player me  = GetPlayer(msg.id);
            if (me){
                mainPlayer = me;
                //Debug.Log("SetMainPlayer");
            }
        }
        if (mainPlayer == null){
            //Debug.Log("SetMainPlayer");
            mainPlayer = CreatePlayer(msg);
            players.Add(mainPlayer);
            mainPlayer.GetComponent<PlayerHop>().SetUp();
            FindObjectOfType<CameraScroll>().SetUp();
            //Debug.Log("Creating mainPlayer: "+msg.id);
        }
    }

    Player CreatePlayer(GameMessage msg){
        GameObject newPlayer = Instantiate(playerPrefab, transform);
        Player pl = newPlayer.GetComponent<Player>();
        pl.id = msg.id;
        pl.tileAddress = msg.position;
        newPlayer.transform.position = new Vector3(0, 0, pl.tileAddress);
        GameObject model = Instantiate(playerModel[pl.id%8], newPlayer.transform);
        model.transform.position = Vector3.zero;

        return pl;
    }

    void DestroyPlayer(int id){
        for(int i = players.Count-1; i >= 0; i--){
            if (id == players[i].id){
                Player pl = players[i];
                players.RemoveAt(i);
                Destroy(pl.gameObject);
            }
        }
    }
    Player GetPlayer(int id){
        for(int i = players.Count-1; i >= 0; i--){
            if (id == players[i].id){
                return players[i];
            }
        }        
        return null;
    }
}
