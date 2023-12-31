using Unity.Entities;
using Unity.Mathematics;

public struct Spawn : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;
}
