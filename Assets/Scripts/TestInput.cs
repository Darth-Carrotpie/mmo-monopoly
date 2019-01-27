using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public int spawnCount = 20;
    public int position;
    int step = 4;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)){
            EventManager.TriggerEvent(EventName.System.SpawnPlayers(), GameMessage.Write().WithCount(spawnCount));
        }
        if (Input.GetKeyDown(KeyCode.B)){
            EventManager.TriggerEvent(EventName.System.UpdateBoard(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)){
            position+=step;
            EventManager.TriggerEvent(EventName.Player.NewPosition(), GameMessage.Write().WithPosition(position).WithID(0));
        }
        if (Input.GetKeyDown(KeyCode.L)){
            EventManager.TriggerEvent(EventName.System.NetworkUpdateReceived(), GameMessage.Write());
        }
        */
    }
}
