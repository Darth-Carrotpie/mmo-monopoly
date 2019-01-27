using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour {
    //public Animator stateMachine;
    void Awake () {
        EventManager.Attach(EventName.Player.PlayerState(), NewPosition);
        EventManager.Attach(EventName.System.TilesDownloaded(), UpdateBoard);
        EventManager.Attach(EventName.System.UpdateBoard(), SetMainPlSceneRef);
    }

    void NewPosition(GameMessage msg){
        EventManager.TriggerEvent(EventName.Player.NewPosition(), msg);
    }
    void UpdateBoard(GameMessage msg){
        EventManager.TriggerEvent(EventName.System.UpdateBoard(), msg);
    }
    void SetMainPlSceneRef(GameMessage msg){
        EventManager.TriggerEvent(EventName.Player.SetMainPlSceneRef(), msg);
    }
}
