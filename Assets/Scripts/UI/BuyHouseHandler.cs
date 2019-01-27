using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHouseHandler : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.TriggerEvent(EventName.Input.BuildHouse(), GameMessage.Write());
    }
}
