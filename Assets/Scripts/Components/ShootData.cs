using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ShootData : IComponentData
{
    public float lastShootTime;
    public float shootCooldown;
}