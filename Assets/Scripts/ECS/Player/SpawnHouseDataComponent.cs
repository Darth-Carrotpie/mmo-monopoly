using Unity.Burst;
using Unity.Jobs;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;

[System.Serializable]
public struct SpawnHouseData : IComponentData
{
    public float3 spawnPosition;
    public float3 spawnDirection;
    public float fireRate;
    public float timeSinceFire;
    public float offset;
}

[ExecuteInEditMode]
public class SpawnHouseDataComponent : ComponentDataWrapper<SpawnHouseData>
{

#if UNITY_EDITOR
    void Update()
    {
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            PositionComponent positionComponent = GetComponent<PositionComponent>();
            if (positionComponent)
            {
                SpawnHouseData spawnData = Value;
                spawnData.spawnPosition = positionComponent.Value.Value;
                Value = spawnData;
            }   
        }            
    }
#endif

    void OnDrawGizmosSelected()
    {            
        SpawnHouseData spawnData = Value;
        Gizmos.color = Color.blue;
        Vector3 position = new Vector3(spawnData.spawnPosition.x,
            spawnData.spawnPosition.y,
            spawnData.spawnPosition.z);
        
        RotationComponent rotationComponent = GetComponent<RotationComponent>();
        if (rotationComponent)
        {
            Vector3 forward = math.forward(rotationComponent.Value.Value);
            position += forward * spawnData.offset;
        }
        
        Gizmos.DrawSphere(position, 0.1f);
    }
}   
