using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
public struct PlayerMoveData : IComponentData
{
    public float speed;
}


public class PlayerMoveDataComponent : ComponentDataWrapper<PlayerMoveData>
{
}
