using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Messages;

public class BoardNetwork : MonoBehaviour
{
    public BoardTile[] tiles;
    public string[] cityNames;
    private string boardDataFilename = "board-message.json";
    private string cityFilename = "cities.json";
    string filePath;
    private void Awake()
    {
        MakeCityNameList();
        EventManager.StartListening(EventName.System.TilesDownloaded(), OnTilesDownloaded);
        filePath = Path.Combine(Application.streamingAssetsPath, boardDataFilename);
        //Debug.Log("Start, Loading mockup json");
        //OnTilesDownloaded(GameMessage.Write());
    }
    void OnTilesDownloaded(GameMessage msg){
        if (msg.boardTiles.Length > 0)
            LoadDownloadedTiles(msg);
        else
            LoadLocalData(msg);
        AssignRandomName();
    }
    void LoadDownloadedTiles(GameMessage msg){
        Debug.Log("Loading board data received from server");
        tiles = msg.boardTiles;
    }
    private void LoadLocalData(GameMessage msg)
    {
        Debug.Log("Loading local board data");
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
    void MakeCityNameList(){
        string cityFilePath = Path.Combine(Application.streamingAssetsPath, cityFilename);
        if (File.Exists(cityFilePath))
        {
            string dataAsJson = File.ReadAllText(cityFilePath);
            //Debug.Log("json: " + dataAsJson);
            //CityDataSerializable loadedData = JsonUtility.FromJson<CityDataSerializable>(dataAsJson);
            dataAsJson = JsonHelper.fixJson(dataAsJson);
            CityData[] loadedData = JsonHelper.FromJson<CityData>(dataAsJson);
            cityNames = CityDataSerializable.Export(loadedData);
        }
        else
        {
            Debug.Log(cityFilePath);
            Debug.LogError("Cannot load game data! - no file found");
        }
    }
    void AssignRandomName(){
        int nameCount = cityNames.Length;
        foreach(BoardTile tile in tiles){
            int index = Random.Range(0, nameCount);
            tile.boardName = cityNames[index];
            //Debug.Log("new name: "+tile.boardName);
        }
    }
}
