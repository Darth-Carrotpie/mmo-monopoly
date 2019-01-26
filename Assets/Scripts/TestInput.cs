using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public int spawnCount = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            EventManager.TriggerEvent(EventName.System.SpawnPlayers(), GameMessage.Write().WithCount(spawnCount));
        }
        if (Input.GetKeyDown(KeyCode.B)){
            EventManager.TriggerEvent(EventName.System.UpdateBoard(), GameMessage.Write());
        }

    }
}
