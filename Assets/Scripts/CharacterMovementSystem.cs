using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct CharacterMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (data, inputs, transform) in SystemAPI.Query<RefRO<CharacterData>, RefRO<InputsData>, RefRW<LocalTransform>>())
        {
            float3 position = transform.ValueRO.Position;
            position.x += inputs.ValueRO.move.x * data.ValueRO.speed * SystemAPI.Time.DeltaTime;
            position.z += inputs.ValueRO.move.y * data.ValueRO.speed * SystemAPI.Time.DeltaTime;
            transform.ValueRW.Position = position;
        }
    }
}
public partial struct CharacterShootSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float currentTime = (float)SystemAPI.Time.ElapsedTime;

        foreach (var (inputs, shootData, transform) in SystemAPI.Query<RefRW<InputsData>, RefRW<ShootData>, RefRO<LocalTransform>>())
        {
            if (inputs.ValueRO.shoot > 0 && currentTime > shootData.ValueRO.lastShootTime + shootData.ValueRO.shootCooldown)
            {
                var bulletPrefab = Resources.Load<GameObject>("BulletPrefab"); // Ensure the path is correct
                if (bulletPrefab != null)
                {
                    Object.Instantiate(bulletPrefab, transform.ValueRO.Position, Quaternion.identity);
                    shootData.ValueRW.lastShootTime = currentTime;
                }
                inputs.ValueRW.shoot = 0;
            }
        }
    }
}

public partial struct CharacterDashSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float currentTime = (float)SystemAPI.Time.ElapsedTime;

        foreach (var (inputs, dashData, transform) in SystemAPI.Query<RefRW<InputsData>, RefRW<DashData>, RefRW<LocalTransform>>())
        {
            if (inputs.ValueRO.dash > 0 && currentTime > dashData.ValueRO.lastDashTime + dashData.ValueRO.dashCooldown)
            {
                float3 dashDirection = math.mul(transform.ValueRO.Rotation, new float3(0, 0, 1));

                transform.ValueRW.Position += dashDirection * dashData.ValueRO.dashDistance;
                dashData.ValueRW.lastDashTime = currentTime;
                inputs.ValueRW.dash = 0;
            }
        }
    }
}