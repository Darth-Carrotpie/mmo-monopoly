﻿using System.Collections;
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
        EventManager.StartListening(EventName.Player.NewPosition(), NewPosition);
        EventManager.StartListening(EventName.Player.SetMainPlayer(), SetMainPlayer);
    }

    void NewPosition(GameMessage msg){
        int id = msg.id;
        int position = msg.position;
        bool playerPresent = false;
        for (int i = 0; i < players.Count; i++){
            if (id == players[i].id)
                playerPresent = true;
        }
        if (!playerPresent){
            Player pl = CreatePlayer(msg);
            players.Add(pl);
            SetUps(pl);
        } else {
            if(mainPlayer.tileAddress - 12 > position || mainPlayer.tileAddress + 45 < position){
                DestroyPlayer(id);
            }
        }
    }

    void SetMainPlayer(GameMessage msg){
        if (mainPlayer == null){
            mainPlayer = CreatePlayer(msg);
            SetUps(mainPlayer);
        }
    }

    Player CreatePlayer(GameMessage msg){
        GameObject newPlayer = Instantiate(playerPrefab, transform);
        Player pl = newPlayer.GetComponent<Player>();
        pl.id = msg.id;
        pl.tileAddress = msg.position;
        newPlayer.transform.position = new Vector3(0, 0, 0);
        GameObject model = Instantiate(playerModel[pl.id%8], newPlayer.transform);
        model.transform.position = Vector3.zero;

        return pl;
    }
    void SetUps(Player pl){
        pl.GetComponent<PlayerHop>().SetUp();
        pl.GetComponent<SceneMovable>().SetUp();
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
}
