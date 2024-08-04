using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

public partial struct HealthPickupSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (pickupTransform, pickupData, pickupEntity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<HealthPickupData>>().WithEntityAccess())
        {
            foreach (var (characterTransform, healthData, characterEntity) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<HealthData>>().WithEntityAccess())
            {
                if (math.distance(pickupTransform.ValueRO.Position, characterTransform.ValueRO.Position) < 1.0f) // допустимая дистанция столкновения
                {
                    healthData.ValueRW.health = math.min(healthData.ValueRW.maxHealth, healthData.ValueRW.health + pickupData.ValueRO.healthAmount);
                    ecb.DestroyEntity(pickupEntity);
                }
            }
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
