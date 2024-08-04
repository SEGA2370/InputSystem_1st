using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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