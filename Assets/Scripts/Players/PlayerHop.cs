using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHop : MonoBehaviour
{
    int currentPos = 0;
    int newPos = 0;
    int id;
    float timeToMove = 2f;
    float timeCounter = 0f;
    void Start()
    {
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }

    void NewPostionTrigger(GameMessage msg){
        if (id == msg.id){
            timeCounter = 0;
            newPos = msg.position;
        }
    }
    void Update(){
        if (newPos > currentPos){
            timeCounter+=Time.deltaTime;
            Hopping();
        }
    }

    void Hopping(){
        float vecZ = Mathf.Lerp(currentPos, newPos, timeCounter/timeToMove);
        transform.position = new Vector3(0, Mathf.Abs(Mathf.Sin(vecZ*Mathf.PI)), vecZ);
    }
}
