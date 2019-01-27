using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHop : MonoBehaviour
{
    public int currentPos = 0;
    int id;
    float timeToMove = 2f;
    float maxTimeToMove = 2f;
    float timeCounter = 0f;
    public float actualNewPos;
    //public Player mainPlayer;
    float maxPeudoDelay = 0.5f;
    float pseudoDelay = 0f;
    bool trigger;
    float newPos;
    //public int totalDif;
    public Player mainPlayer;
    void Awake()
    {
        //mainPlayer = FindObjectOfType<PlayerNetwork>().player;
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
        //EventManager.StartListening(EventName.System.MoveBoard(), TriggerSceneMovement);
    }
    public void SetUp(){
        mainPlayer = FindObjectOfType<PlayersManager>().mainPlayer;
        id = GetComponent<Player>().id;
    }
    /* public void TriggerSceneMovement(GameMessage msg){
        //Debug.Log("TriggerSceneMovement hop: "+mainPlayer.tileAddress);
        id = GetComponent<Player>().id;
        if (id != mainPlayer.id)
            totalDif = - mainPlayer.tileAddress;
    }*/
    void NewPostionTrigger(GameMessage msg){
        if (id == msg.id){
            GenerateTimeToMove();
            //Debug.Log("playerID: "+id+" pos: "+msg.position);
            //Debug.Log("trigered hop: Equals! +ID:"+id);
            trigger = true;
            timeCounter = 0;
            newPos = msg.position;
        }
    }

    void Update(){
        if (trigger){
            timeCounter+=Time.deltaTime;
             if (timeCounter > pseudoDelay){
                //Debug.Log("Trigger ended");
                trigger = false;
                timeCounter = 0;
                actualNewPos = newPos;
                currentPos = (int)Mathf.Round(transform.position.z);
            }       
        }

        if (timeToMove > timeCounter ){
            if(!trigger){
                Hopping();
            }
        }
    }

    void Hopping(){
        timeCounter+=Time.deltaTime;
        float vecZ = Mathf.Lerp(currentPos, actualNewPos, timeCounter/timeToMove);
        transform.position = new Vector3(0, Mathf.Abs(Mathf.Sin(vecZ*Mathf.PI))/2, vecZ);
    }

    void GenerateTimeToMove(){
        pseudoDelay = Random.Range(0, maxPeudoDelay/2f);
        timeToMove = maxTimeToMove - pseudoDelay*2f;
    }

    void OnDetroy(){
        //EventManager.StopListening(EventName.System.MoveBoard(), TriggerSceneMovement);
        EventManager.StopListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }
}
