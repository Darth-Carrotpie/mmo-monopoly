using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace FLT.ECS
{
    public class GameEntityManager : MonoBehaviour
    {
        EntityManager manager;
        public static class GM {
            public static float topBound;
            public static float bottomBound;
            public static float leftBound;
            public static float rightBound;
        }

        public float enemySpeed;
        public GameObject enemyShipPrefab;
        public int enemyShipCount;
        public int enemyShipIncremement;

        void Start()
        {
            manager = World.Active.GetOrCreateManager<EntityManager>();
            AddShips(enemyShipCount);
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
                AddShips(enemyShipIncremement);
        }

        void AddShips(int amount)
        {
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            manager.Instantiate(enemyShipPrefab, entities);

            for (int i = 0; i < amount; i++)
            {
                float xVal = UnityEngine.Random.Range(GM.leftBound, GM.rightBound);
                float zVal = UnityEngine.Random.Range(0f, 10f);
                manager.SetComponentData(entities[i], new Position { Value = new float3(xVal, 0f, GM.topBound + zVal) });
                manager.SetComponentData(entities[i], new Rotation { Value = new quaternion(0, 1, 0, 0) });
                manager.SetComponentData(entities[i], new MoveSpeed { Value = enemySpeed });
            }
            entities.Dispose();
        }
    }
}

