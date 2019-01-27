using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScene : MonoBehaviour
{
    //public Player mainPlayer;
    //public int totalDif;
    float delay = 2f;
    float counter;
    public bool trigger;
    void Awake()
    {
        EventManager.StartListening(EventName.Player.NewPosition(), OnMoveBoard);
    }

    void OnMoveBoard(GameMessage msg){
        trigger = true;
    }

    void Update(){
        if (trigger){
            counter += Time.deltaTime;
            if (counter > delay){
                trigger = false;
                counter = 0;
                EventManager.TriggerEvent(EventName.System.MoveBoard(), GameMessage.Write());
            }
        }
    }
}
