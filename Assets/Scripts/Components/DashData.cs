using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DashData : IComponentData
{
    public float dashCooldown;
    public float lastDashTime;
    public float dashDistance;
}
 
