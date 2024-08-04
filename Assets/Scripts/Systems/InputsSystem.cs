using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputsSystem : SystemBase
{
    private Controls inputs = null;

    protected override void OnCreate()
    {
        inputs = new Controls();
        inputs.Enable();
    }

    protected override void OnUpdate()
    {
        foreach (var data in SystemAPI.Query<RefRW<InputsData>>())
        {
            data.ValueRW.move = inputs.Character.Move.ReadValue<Vector2>();

            if (Keyboard.current.spaceKey.isPressed)
            {
                data.ValueRW.shoot = 1f;
            }
            else
            {
                data.ValueRW.shoot = 0f;
            }


            if (Keyboard.current.leftShiftKey.isPressed)
            {
                data.ValueRW.dash = 1f; 
            }
            else
            {
                data.ValueRW.dash = 0f;
            }
        }
    }
}
