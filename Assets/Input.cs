using UnityEngine;

[CreateAssetMenu(menuName = "PlayerOneInput")]
public abstract class Input : ScriptableObject
{
    public InputManager input;

    public Vector2 movementInput { get; protected set; }
    public bool attack { get; protected set; }

    private void Awake()
    {
        input = new InputManager();

    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
