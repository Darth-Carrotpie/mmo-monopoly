using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int tileAddress;
    public int id;

    void Start()
    {
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }

    void NewPostionTrigger(GameMessage msg){
        if (id == msg.id){
            tileAddress = msg.position;
        }
    }
}
