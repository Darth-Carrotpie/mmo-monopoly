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
        Debug.Log("RECEIVING STATE");
        EventManager.TriggerEvent(EventName.Player.SetMainPlayer(), GameMessage.Write().WithID(state.me.id).WithPosition(state.me.position).WithRoll(state.me.roll));
        EventManager.TriggerEvent(EventName.Player.PossibleAction(), GameMessage.Write().WithPossibleAction(state.me.possibleActions));
        EventManager.TriggerEvent(EventName.UI.UpdTransaction(), GameMessage.Write().WithTransaction(state.me.transactions));

        //distribute new positions for players:
        for(int i=0; i < state.players.Length; i++){
            if(state.me.position - 30 < state.players[i].position && state.me.position + 60 > state.players[i].position){
                EventManager.TriggerEvent(EventName.Player.PlayerState(), GameMessage.Write().WithID(state.players[i].id).WithPosition(state.players[i].position));
            }
        }
        //distribute house and hotel locations:
        List<BuildingData> houses = new List<BuildingData>();
        for(int i=0; i < state.players.Length; i++){
            for (int j=0; j < state.players[i].houses.Length ; j++){
                if (state.me.position - 30 < state.players[i].houses[j] && state.me.position + 60 > state.players[i].houses[j])
                    houses.Add(new BuildingData(state.players[i].houses[j], state.players[i].id));
            }
        }
        EventManager.TriggerEvent(EventName.System.SpawnHouses(), GameMessage.Write().WithBuildings(houses.ToArray()));
        List<BuildingData> hotels = new List<BuildingData>();
        for(int i=0; i < state.players.Length; i++){
            for (int j=0; j < state.players[i].hotels.Length ; j++){
                if (state.me.position - 30 < state.players[i].hotels[j] && state.me.position + 60 > state.players[i].hotels[j])
                    hotels.Add(new BuildingData(state.players[i].hotels[j], state.players[i].id));
            }
        }
        EventManager.TriggerEvent(EventName.System.SpawnHotels(), GameMessage.Write().WithBuildings(hotels.ToArray()));

        EventManager.TriggerEvent(EventName.System.UpdateBoard(), GameMessage.Write());


        EventManager.TriggerEvent(EventName.UI.UpdWealth(), GameMessage.Write().WithCount(state.me.cash));
        EventManager.TriggerEvent(EventName.UI.UpdTransaction(), GameMessage.Write().WithTransaction(state.me.transactions));

        //end of all message shoots
        EventManager.TriggerEvent(EventName.System.Turn(), GameMessage.Write().WithCount(state.turnCount).WithRoll(state.me.roll));

        turnCount = state.turnCount;
    }


    void OnBoardReceived(Messages.Tile[] tiles){
        Debug.Log("RECEIVING BOARD");
        //Debug.Log("tiles"+tiles.Length);
        BoardTile[] boardTiles = Messages.BoardMessage.ToBoard(tiles);
        //Debug.Log("tiles"+boardTiles.Length);
        EventManager.TriggerEvent(EventName.System.TilesDownloaded(), GameMessage.Write().WithBoardTiles(boardTiles));
    }
}
