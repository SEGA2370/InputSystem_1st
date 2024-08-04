using Unity.Entities;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
}

public class CharacterHealthBaker : Baker<CharacterHealth>
{
    public override void Bake(CharacterHealth authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new HealthData
        {
            health = authoring.health,
            maxHealth = authoring.maxHealth
        });
    }
}