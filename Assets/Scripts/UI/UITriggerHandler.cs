using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITriggerHandler : MonoBehaviour
{
    bool isMute;

    public Button BuyHotelButton;
    public Button BuyHouseButton;

    void Start()
    {
        BuyHotelButton.onClick.AddListener(SendToServer);
        BuyHouseButton.onClick.AddListener(SendToServer);
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }

    public void SendToServer()
    {
        string intent = "nothing";

        if (this.gameObject.tag == "Hotel")
        {
            intent = "buy-hotel";
        }
        else if (this.gameObject.tag == "House")
        {
            intent = "buy-house";
        }

    }

    public void OnClick()
    {
        print('1');
    }
}
