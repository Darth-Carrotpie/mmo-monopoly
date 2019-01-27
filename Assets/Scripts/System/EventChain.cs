using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour {
    //public Animator stateMachine;
    void Awake () {
        EventManager.Attach(EventName.Player.NewPosition(), UpdateBoard);
        EventManager.Attach(EventName.System.TilesDownloaded(), UpdateBoard);
        EventManager.StartListening(EventName.System.UpdateBoard(), SetMainPlSceneRef);
    }

    void UpdateBoard(GameMessage msg){
        EventManager.TriggerEvent(EventName.System.UpdateBoard(), GameMessage.Write());
    }
    void SetMainPlSceneRef(GameMessage msg){
        EventManager.TriggerEvent(EventName.Player.SetMainPlSceneRef(), GameMessage.Write());
    }
}
