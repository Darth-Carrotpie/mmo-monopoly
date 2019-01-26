using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHop : MonoBehaviour
{
    int currentPos = 0;
    int id;
    float timeToMove = 2f;
    float timeCounter = 0f;
    SceneMovable movable;
    float actualNewPos;
    //public Player mainPlayer;
    void Start()
    {
        //mainPlayer = FindObjectOfType<PlayerNetwork>().player;
        movable = GetComponent<SceneMovable>();
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }

    void NewPostionTrigger(GameMessage msg){
        if (id == msg.id){
            timeCounter = 0;
            actualNewPos = msg.position + movable.totalDif;
            currentPos = (int)Mathf.Round(transform.position.z);
        }
    }
    void Update(){
        if (actualNewPos > currentPos){
            timeCounter+=Time.deltaTime;
            Hopping();
        }
    }

    void Hopping(){
        float vecZ = Mathf.Lerp(currentPos, actualNewPos, timeCounter/timeToMove);
        transform.position = new Vector3(0, Mathf.Abs(Mathf.Sin(vecZ*Mathf.PI)), vecZ);
    }
}
