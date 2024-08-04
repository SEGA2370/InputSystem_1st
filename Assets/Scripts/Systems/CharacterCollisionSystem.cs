using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct CharacterCollisionSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (characterTransform, healthData, characterEntity) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<HealthData>>().WithEntityAccess())
        {
            foreach (var (pickupTransform, pickupData, pickupEntity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<HealthPickupData>>().WithEntityAccess())
            {
                if (math.distance(characterTransform.ValueRO.Position, pickupTransform.ValueRO.Position) < 1.0f) // допустимая дистанция столкновения
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