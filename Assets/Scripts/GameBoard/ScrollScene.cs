using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScene : MonoBehaviour
{
    public Player mainPlayer;
    public int totalDif;
    void Start()
    {
        EventManager.StartListening(EventName.System.MoveBoard(), OnMoveBoard);
    }

    void OnMoveBoard(GameMessage msg){
        List<SceneMovable> movables = new List<SceneMovable>(Resources.FindObjectsOfTypeAll(typeof(SceneMovable)) as SceneMovable[]);

        //if(msg.)
    }
}
