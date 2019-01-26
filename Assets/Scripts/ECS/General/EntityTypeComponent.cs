using System;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.Rendering;

[System.Serializable]
public struct EntityTypeData : ISharedComponentData
{
    public enum EntityType
    {
        Player = 0,
        Tile = 1,
        House = 2,
        Hotel = 3,

        GameplaySpawner,
        BackgroundSpawner,

        EntityTypeCount,
    }
    public EntityType entityType;
}

public class EntityTypeComponent : SharedComponentDataWrapper<EntityTypeData>
{


}