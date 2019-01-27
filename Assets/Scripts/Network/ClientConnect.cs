using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientConnect : MonoBehaviour
{

    public IMessenger messenger;
    void Start()
    {
        messenger = new DummyMessenger(this);
        messenger.Connect(OnStateReceived, OnBoardReceived);
        //EventManager.StartListening(EventName.Input.)
    }

    void Update()
    {
        
    }

    void OnStateReceived(Messages.State state){
        



        EventManager.TriggerEvent(EventName.UI.UpdWealth(), GameMessage.Write().WithCount(state.me.cash));

        //end of all message shoots
        EventManager.TriggerEvent(EventName.System.Turn(), GameMessage.Write().WithCount(state.turnCount));
    }


    void OnBoardReceived(Messages.Tile[] tiles){
        BoardTile[] boardTiles = Messages.BoardMessage.ToBoard(tiles);
        EventManager.TriggerEvent(EventName.System.TilesDownloaded(), GameMessage.Write().WithBoardTiles(boardTiles));
    }
}
