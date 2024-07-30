using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 2f;
    public float shootCooldown = 1f;
}

public struct CharacterData : IComponentData
{
    public float speed;
}

public class CharacterBaker : Baker<Character>
{
    public override void Bake(Character authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new CharacterData
        {
            speed = authoring.speed,
        });

        AddComponent(entity, new InputsData
        {
            
        });

        AddComponent(entity, new ShootData
        {
            lastShootTime = 0f,
            shootCooldown = authoring.shootCooldown
        });

        AddComponent(entity, new DashData
        {
            dashCooldown = 1f,
            lastDashTime = 0f,
            dashDistance = 10f
        });
    }
}