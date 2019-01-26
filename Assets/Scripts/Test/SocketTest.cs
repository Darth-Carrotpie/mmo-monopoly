using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketTest : MonoBehaviour
{
	const string wsPath = "ws://localhost:2048";

    WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(Connect(wsPath));
    }

	IEnumerator Connect(string roomName)
	{
		ws = new WebSocket(new System.Uri(wsPath), new string[1] { "echo-protocol" });
		yield return this.StartCoroutine(ws.Connect());
		Debug.Log("Connected");
		this.StartCoroutine(Listen());
        ws.SendString("Unity Connected!");
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
			while((reply = ws.RecvString()) != null)
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

    // Update is called once per frame
    void Update()
    {

    }
}
