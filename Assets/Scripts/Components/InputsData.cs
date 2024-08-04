using Unity.Entities;
using Unity.Mathematics;

public struct InputsData : IComponentData
{
    public float2 move;
    public float shoot;
    public float dash;
}