using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawn : MonoBehaviour
{
    void Start()
    {
        EventManager.StartListening(EventName.System.SpawnPlayers(), SpawnPlayers);
    }


    void SpawnPlayers(GameMessage msg){
        
    }
}
