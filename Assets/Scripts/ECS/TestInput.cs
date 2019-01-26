using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            EventManager.TriggerEvent(EventName.System.SpawnPlayers(), GameMessage.Write().WithCount(20));
        }
    }
}
