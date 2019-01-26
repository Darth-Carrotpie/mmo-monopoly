using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHop : MonoBehaviour
{
    int currentPos = 0;
    int id;
    float timeToMove = 2f;
    float maxTimeToMove = 2f;
    public float timeCounter = 0f;
    SceneMovable movable;
    float actualNewPos;
    //public Player mainPlayer;
    float maxPeudoDelay = 0.5f;
    public float pseudoDelay = 0f;
    bool trigger;
    float newPos;
    void Start()
    {
        //mainPlayer = FindObjectOfType<PlayerNetwork>().player;
        movable = GetComponent<SceneMovable>();
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }

    void NewPostionTrigger(GameMessage msg){
        if (id == msg.id){
            GenerateTimeToMove();
            trigger = true;
            timeCounter = 0;
            newPos = msg.position + movable.totalDif;
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
}
