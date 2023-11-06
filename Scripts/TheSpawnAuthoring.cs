using UnityEngine;
using Unity.Entities;

class TheSpawnAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
    public float NextSpawnTime;
}

class SpawnerBaker : Baker<TheSpawnAuthoring>
{
    public override void Bake(TheSpawnAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new Spawn
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            SpawnPosition = authoring.transform.position,
            NextSpawnTime = authoring.NextSpawnTime,
            SpawnRate = authoring.SpawnRate,
        });
    }
}
