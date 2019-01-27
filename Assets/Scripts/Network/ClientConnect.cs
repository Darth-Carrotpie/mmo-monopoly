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
        EventManager.StartListening(EventName.Input.BuildHouse(), BuildHouse);
        EventManager.StartListening(EventName.Input.BuildHotel(), BuildHotel);
    }

    void Update()
    {
        
    }

    void BuildHouse(GameMessage msg){
        //send build msg to server
    }
    void BuildHotel(GameMessage msg){
        //send build msg to server
    }

    void OnStateReceived(Messages.State state){
        EventManager.TriggerEvent(EventName.Player.SetMainPlayer(), GameMessage.Write().WithID(state.me.id).WithPosition(state.me.position).WithRoll(state.me.roll));


        //distribute new positions for players:
        for(int i=0; i < state.players.Length; i++){
            if(state.me.position - 15 > state.players[i].position || state.me.position - 50 < state.players[i].position){
                EventManager.TriggerEvent(EventName.Player.NewPosition(), GameMessage.Write().WithID(state.players[i].id).WithPosition(state.players[i].position));
            }
        }
        //distribute house and hotel locations:
        /*/for(int i=0; i < state.players.Length; i++){
            if(state.me.position - 15 > state.players[i].position || state.me.position - 50 < state.players[i].position){
                EventManager.TriggerEvent(EventName.Player.NewPosition(), GameMessage.Write().WithID(state.players[i].id).WithPosition(state.players[i].position));
            }
        }     */

        EventManager.TriggerEvent(EventName.UI.UpdWealth(), GameMessage.Write().WithCount(state.me.cash));

        //end of all message shoots
        EventManager.TriggerEvent(EventName.System.Turn(), GameMessage.Write().WithCount(state.turnCount).WithRoll(state.me.roll));
    }


    void OnBoardReceived(Messages.Tile[] tiles){
        BoardTile[] boardTiles = Messages.BoardMessage.ToBoard(tiles);
        EventManager.TriggerEvent(EventName.System.TilesDownloaded(), GameMessage.Write().WithBoardTiles(boardTiles));
    }
}
