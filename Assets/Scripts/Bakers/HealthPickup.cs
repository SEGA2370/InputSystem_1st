using Unity.Entities;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 25f;
}

public class HealthPickupBaker : Baker<HealthPickup>
{
    public override void Bake(HealthPickup authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new HealthPickupData
        {
            healthAmount = authoring.healthAmount
        });
    }
}