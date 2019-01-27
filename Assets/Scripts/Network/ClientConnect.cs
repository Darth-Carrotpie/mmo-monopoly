using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientConnect : MonoBehaviour
{
    private IMessenger messenger;
    int turnCount;

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
        messenger.SelectAction(Messages.Action.BuyHouse, this.turnCount);
    }
    void BuildHotel(GameMessage msg){
        messenger.SelectAction(Messages.Action.BuyHotel, this.turnCount);
    }

    void OnStateReceived(Messages.State state){
        EventManager.TriggerEvent(EventName.Player.SetMainPlayer(), GameMessage.Write().WithID(state.me.id).WithPosition(state.me.position).WithRoll(state.me.roll));
        EventManager.TriggerEvent(EventName.Player.PossibleAction(), GameMessage.Write().WithPossibleAction(state.me.possibleActions));


        //distribute new positions for players:
        for(int i=0; i < state.players.Length; i++){
            if(state.me.position - 15 > state.players[i].position || state.me.position - 50 < state.players[i].position){
                EventManager.TriggerEvent(EventName.Player.NewPosition(), GameMessage.Write().WithID(state.players[i].id).WithPosition(state.players[i].position));
            }
        }
        //distribute house and hotel locations:
        /* for(int i=0; i < state.players.Length; i++){
            if(state.me.position - 15 > state.players[i].position || state.me.position - 50 < state.players[i].position){
                EventManager.TriggerEvent(EventName.Player.NewPosition(), GameMessage.Write().WithID(state.players[i].id).WithPosition(state.players[i].position));
            }
        }   */  

        EventManager.TriggerEvent(EventName.UI.UpdWealth(), GameMessage.Write().WithCount(state.me.cash));

        //end of all message shoots
        EventManager.TriggerEvent(EventName.System.Turn(), GameMessage.Write().WithCount(state.turnCount).WithRoll(state.me.roll));

        turnCount = state.turnCount;
    }


    void OnBoardReceived(Messages.Tile[] tiles){
        //Debug.Log("tiles"+tiles.Length);
        BoardTile[] boardTiles = Messages.BoardMessage.ToBoard(tiles);
        //Debug.Log("tiles"+boardTiles.Length);
        EventManager.TriggerEvent(EventName.System.TilesDownloaded(), GameMessage.Write().WithBoardTiles(boardTiles));
    }
}
