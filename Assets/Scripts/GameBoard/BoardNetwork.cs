using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardNetwork : MonoBehaviour
{
    public BoardTile[] tiles;
    private string nodeDataFileName = "board-message.json";
    public GameObject nodePrefab;
    public Transform spawnLocation;
    string filePath;
    private void Awake()
    {
        //EventManager.StartListening(EventName.Input.Gem.Save(), SaveNodeData);
    }
    void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, nodeDataFileName);

        LoadNodeData();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            EventManager.TriggerEvent(EventName.System.NetworkUpdateReceived(), GameMessage.Write());
        }
    }
    private void LoadNodeData()
    {
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            //Debug.Log("json: " + dataAsJson);
            BoardTileDataSerializable loadedData = JsonUtility.FromJson<BoardTileDataSerializable>(dataAsJson);

            tiles = loadedData.Export();
        }
        else
        {
            Debug.Log(filePath);
            Debug.LogError("Cannot load game data! - no file found");
        }
    }


}
