using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHotelHandler : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.TriggerEvent(EventName.Input.BuildHotel(), GameMessage.Write());
    }
}
