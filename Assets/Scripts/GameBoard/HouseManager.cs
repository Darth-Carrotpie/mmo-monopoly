using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public int rangeForward = 30;
    public List<Building> houses = new List<Building>();
    public GameObject housePrefab;
    Player mainPlayer;
    public float tileSideOffset = 0.7f;
    public float spaceBetweenHouses = 0.2f;
    public float forwardOffset = 0.0f;
    BoardManager manager;
    public string eventToListen = " ";
    void Start()
    {
        EventManager.StartListening(eventToListen, SpawnHouses);
        manager = FindObjectOfType<BoardManager>();
    }

    void SpawnHouses(GameMessage msg){
        mainPlayer = FindObjectOfType<PlayersManager>().mainPlayer;
        if (!mainPlayer)
            return;
        DestroyHouses();
        Createhouses(msg);
    }

    void DestroyHouses(){
            for(int i = houses.Count-1; i >= 0; i--){
            if (houses[i].transform.position.z < mainPlayer.tileAddress-20){
                Destroy(houses[i].gameObject);
                houses.RemoveAt(i);
            }
        }
    }

    void Createhouses(GameMessage msg){
        int lastTileAdress = manager.GetLastAdress();
        Debug.Log("lastTileAdress: "+lastTileAdress+" mainPlayer.tileAdd: "+mainPlayer.tileAddress+ " range: "+rangeForward);
        for (int i = mainPlayer.tileAddress; i < rangeForward; i++){
            CreateBuildingsOnTile(NewBuildingsOnAddress(msg.buildings, i), i);
        }
    }
    void CreateBuildingsOnTile(Building[] buildingsOnTile, int tileAddress){
        Vector3 tileVector = new Vector3(0, 0,  tileAddress);
        Vector3 positionOffset = new Vector3 (tileSideOffset + spaceBetweenHouses*BuildingsOnAddress(tileAddress), 0, forwardOffset);
        for (int i = 0; i < buildingsOnTile.Length; i++){
            if (houses.Contains(buildingsOnTile[i]))
                continue;
            Debug.Log("creating a house");
            GameObject newhouse = Instantiate(housePrefab, transform);
            Building house = newhouse.GetComponent<Building>();
            house.Init(buildingsOnTile[i]);
            houses.Add(house);
            newhouse.transform.position = tileVector + positionOffset;
            positionOffset += new Vector3 (spaceBetweenHouses, 0, 0);
        }
    }
    int BuildingsOnAddress(int tileAddress){
        int count = 0;
        for (int i = 0; i < houses.Count; i++){
            if (houses[i].position == tileAddress){
                count++;
            }
        }
        return count;
    }
    Building[] NewBuildingsOnAddress(BuildingData[] allBuildings, int tileAddress){
        List<Building> b = new List<Building>();
        for (int i = 0; i < allBuildings.Length; i++){

            if (allBuildings[i].position == tileAddress){
                b.Add(new Building(allBuildings[i]));
            }
        }
        Debug.Log(b.Count+" buildings at adress "+tileAddress);
        return b.ToArray();
    }
    int GetLastIndex(){
        int index = houses.Count;
        if (index>0){
            index = 0;
            for (int i = 0; i < houses.Count; i ++){
                int z = (int)houses[i].position;
                if (z > index){
                    index = z;
                }
            }
            return index;
        }
        return index;
    }
}
