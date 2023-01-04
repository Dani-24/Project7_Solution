using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public bool alive = true;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float movementInput = 0.0f;
    [SerializeField]
    private bool jumped = false;
    [SerializeField]
    private bool blocked = false;
    [SerializeField]
    private bool quickAttacked = false;
    [SerializeField]
    private bool slowAttacked = false;
    [SerializeField]
    private bool lowQuickAttacked = false;
    [SerializeField]
    private bool lowSlowAttacked = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        blocked = context.action.triggered;
    }

    public void OnQuickAttack(InputAction.CallbackContext context)
    {
        quickAttacked = context.action.triggered;
    }

    public void OnSlowAttack(InputAction.CallbackContext context)
    {
        slowAttacked = context.action.triggered;
    }

    public void OnLowQuickAttack(InputAction.CallbackContext context)
    {
        lowQuickAttacked = context.action.triggered;
    }

    public void OnLowSlowAttack(InputAction.CallbackContext context)
    {
        lowSlowAttacked = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
