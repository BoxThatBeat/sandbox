using UnityEngine;

public class PlayerTwoInput : Input
{
  
    private void Awake()
    {
        input.PlayerTwoControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        input.PlayerTwoControls.Attack.performed += ctx => attack = ctx.ReadValue<bool>();

    }
}
