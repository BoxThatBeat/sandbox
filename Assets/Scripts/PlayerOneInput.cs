using UnityEngine;

public class PlayerOneInput : Input
{

    private void Awake()
    {
        input.PlayerOneControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        input.PlayerOneControls.Attack.performed += ctx => attack = ctx.ReadValue<bool>();

    }

}

