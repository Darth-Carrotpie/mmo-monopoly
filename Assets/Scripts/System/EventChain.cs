using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour {
    //public Animator stateMachine;
    void Start () {
        //EventManager.Attach(EventName.Input.BuildLocationSettled(), UpdateTargetMarks);
        //EventManager.Attach(EventName.Input.FactionSelected(), UpdateTalentTree);
    }
    
    void UpdateTargetMarks(GameMessage msg)
    {
        //EventManager.TriggerEvent(EventName.UI.UpdateTargetMarks(), GameMessage.Write().WithTargeting(msg.targeting));
    }

    void UpdateTalentTree(GameMessage msg)
    {
        //EventManager.TriggerEvent(EventName.UI.UpdateTalentTree(), msg);
    }
}
