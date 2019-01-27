using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public int rangeForward = 5;
    public GameObject tablePrefab;
    Player mainPlayer;
    public List<GameObject> tables = new List<GameObject>();
    void Awake()
    {
        EventManager.StartListening(EventName.Player.NewPosition(), OnMoveBoard);
    }

    void OnMoveBoard(GameMessage msg)
    {
        mainPlayer = FindObjectOfType<PlayersManager>().mainPlayer;
        if (!mainPlayer)
            return;
        DestroyTables();
        CreateTables();
    }

    void DestroyTables(){
            for(int i = tables.Count-1; i >= 0; i--){
            if (tables[i].transform.position.z < mainPlayer.tileAddress-15){
                Destroy(tables[i].gameObject);
                tables.RemoveAt(i);
            }
        }
    }

    void CreateTables(){
        int lastIndex = GetLastIndex();
        for (int i = 0; i < rangeForward - tables.Count; i++){
            GameObject newTables = Instantiate(tablePrefab, transform);
            newTables.transform.position = new Vector3(0, 0,  (i )*10 + lastIndex);
            tables.Add(newTables);
        }
    }

    int GetLastIndex(){
        int index = tables.Count;
        if (index>0){
            index = 0;
            for (int i = 0; i < tables.Count; i ++){
                int z = (int)tables[i].transform.position.z;
                if (z > index){
                    index = z;
                }
            }
            return index;
        }
        return index;
    }
}
