using Unity.Entities;
using Unity.Mathematics;

public struct InputsData : IComponentData
{
    public float2 move;
    public float shoot;
    public float dash;
}

public struct ShootData : IComponentData
{
    public float lastShootTime;
    public float shootCooldown;
}

public struct DashData : IComponentData
{
    public float dashCooldown;
    public float lastDashTime;
    public float dashDistance;
}
