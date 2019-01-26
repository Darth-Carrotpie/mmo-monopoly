using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHotelHandler : MonoBehaviour
{
    string intent;

    public void OnClick()
    {
        string intent = "buy-hotel";

        print(intent);
    }
}
