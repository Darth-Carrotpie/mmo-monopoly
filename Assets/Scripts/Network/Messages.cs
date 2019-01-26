namespace Messages
{

[System.Serializable]
public class Message {
    public string messageType;
}

public enum Action {
    Nothing = 0,
    BuyHouse = 1,
    BuyHotel = 2
}

[System.Serializable]
public class Intent {
    public Action actionId;
    public int turnCount;
}

[System.Serializable]
public class IntentMessage : Message {
    public Intent intent;
}

[System.Serializable]
public class PossibleAction {
    public string type;
    public Action id;
    public int cost;
}

[System.Serializable]
public class Player {
    public int id;
    public int position;
    public int[] houses;
    public int[] hotels;
    public int cash;
    public int[] roll;
    public PossibleAction[] possibleActions;
}

[System.Serializable]
public class State {
    public int turnCount;
    public Player[] players;
    public Player me;
}

[System.Serializable]
public class StateMessage : Message {
    public State state;
}

}

