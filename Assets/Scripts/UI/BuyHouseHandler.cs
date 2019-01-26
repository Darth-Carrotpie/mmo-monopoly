using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHouseHandler : MonoBehaviour
{
    WebSocket ws;

    const string wsPath = "ws://localhost:2048";
    string intent;
    
    public void OnClick()
    {
        intent = "buy-house";

        this.StartCoroutine(Connect(wsPath));
    }

    IEnumerator Connect(string roomName)
    {
        ws = new WebSocket(new System.Uri(wsPath), new string[1] { "echo-protocol" });
        yield return this.StartCoroutine(ws.Connect());
        Debug.Log("Connected");
        this.StartCoroutine(Listen());
        ws.SendString(intent);
    }

    IEnumerator Listen()
    {
        while (true)
        {
            if (!ws.IsConnected() || ws.error != null)
            {
                Close();
                yield break;
            }

            string reply;
            while ((reply = ws.RecvString()) != null)
            {
                Debug.Log(reply);
            }

            yield return null;
        }
    }

    void Close()
    {
        Debug.Log("Closing connection");
        ws.Close();
    }
}
