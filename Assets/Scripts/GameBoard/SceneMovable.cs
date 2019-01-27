using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovable : MonoBehaviour
{
    public float newPos;
    float timeCounter;
    public float currentPos;
    float timeToMove = 1f;
    public Player mainPlayer;
    
    public int totalDif;

    bool trigger;

    void Start()
    {
        mainPlayer = FindObjectOfType<PlayersManager>().mainPlayer;
        EventManager.StartListening(EventName.System.MoveBoard(), TriggerSceneMovement);
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }
    void NewPostionTrigger(GameMessage msg){
        trigger = false;
    }
    public void TriggerSceneMovement(GameMessage msg){
        trigger = true;
        timeCounter = 0;
        //Debug.Log("moveScene");
        currentPos = transform.position.z;
        Player pl = GetComponent<Player>();
        if (pl)
            newPos = pl.tileAddress - mainPlayer.tileAddress;
        BoardTile bt = GetComponent<BoardTile>();
        if (bt && mainPlayer)
            newPos = bt.address - mainPlayer.tileAddress;
        totalDif = - mainPlayer.tileAddress;
    }

    void Update(){
        if (newPos < currentPos && trigger){
            timeCounter+=Time.deltaTime;
            Moving();
        }
    }

    void Moving(){
        float vecZ = Mathf.Lerp(currentPos, newPos, timeCounter/timeToMove);
        transform.position = new Vector3(0, transform.position.y, vecZ);
    }
    
    void OnDetroy(){
        EventManager.StopListening(EventName.System.MoveBoard(), TriggerSceneMovement);
        EventManager.StopListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }
}
