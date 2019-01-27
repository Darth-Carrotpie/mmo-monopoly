using UnityEngine;
using System.Collections.Generic;
public class GameMessage
{
    public static GameMessage Write()
    {
        return new GameMessage();
    }

    public string message;
    public GameMessage WithMessage(string value)
    {
        message = value;
        return this;
    }

    public Vector3 coordinates;
    public GameMessage WithCoordinates(Vector3 value)
    {
        coordinates = value;
        return this;
    }

    public Vector3 selectionSize;
    public GameMessage WithSelectionSize(Vector3 value)
    {
        selectionSize = value;
        return this;
    }
    public int count;
    public GameMessage WithCount(int value)
    {
        count = value;
        return this;
    }
    public int position;
    public GameMessage WithPosition(int value)
    {
        position = value;
        return this;
    }
    public int id;
    public GameMessage WithID(int value)
    {
        id = value;
        return this;
    }
    public BoardTile[] boardTiles;
    public GameMessage WithBoardTiles(BoardTile[] value)
    {
        boardTiles = value;
        return this;
    }

    public int[] roll;
    public GameMessage WithRoll(int[] value)
    {
        roll = value;
        return this;
    } 

    public Messages.PossibleAction[] possibleAction;
    public GameMessage WithPossibleAction(Messages.PossibleAction[] value)
    {
        possibleAction = value;
        return this;
    }

    public Messages.Transaction[] transaction;
    public GameMessage WithTransaction(Messages.Transaction[] value)
    {
        transaction = value;
        return this;
    }
}
