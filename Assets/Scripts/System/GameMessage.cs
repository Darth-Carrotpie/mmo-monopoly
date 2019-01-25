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
}