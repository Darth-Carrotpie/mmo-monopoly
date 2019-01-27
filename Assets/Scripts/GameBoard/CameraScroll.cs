using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float newPos;
    float timeCounter;
    public float currentPos;
    float timeToMove = 1f;
    public Player mainPlayer;
    
    bool trigger;
    void Awake()
    {
        EventManager.StartListening(EventName.System.MoveBoard(), TriggerSceneMovement);
        EventManager.StartListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }
    void Start(){

    }
    void NewPostionTrigger(GameMessage msg){
        trigger = false;
    }
    public void TriggerSceneMovement(GameMessage msg){
        Debug.Log("move cam");
        trigger = true;
        timeCounter = 0;
        currentPos = transform.position.z;
        newPos = mainPlayer.tileAddress -1.24f;
    }
    public void SetMainPlSceneRef(GameMessage msg){
        SetUp();
    }
    public void SetUp(){
        mainPlayer = FindObjectOfType<PlayersManager>().mainPlayer;
    }

    void Update(){
        if (newPos > currentPos && trigger){
            timeCounter+=Time.deltaTime;
            Moving();
        }
    }

    void Moving(){
        float vecZ = Mathf.Lerp(currentPos, newPos, timeCounter/timeToMove);
        transform.position = new Vector3(transform.position.x, transform.position.y, vecZ);
    }
    
    void OnDetroy(){
        EventManager.StopListening(EventName.System.MoveBoard(), TriggerSceneMovement);
        EventManager.StopListening(EventName.Player.NewPosition(), NewPostionTrigger);
    }
}
