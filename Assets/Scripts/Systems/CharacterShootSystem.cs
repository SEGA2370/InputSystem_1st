using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

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
