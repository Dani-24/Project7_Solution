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
    [SerializeField]
    private bool win = false;
    [SerializeField]
    private bool die = false;

    private Animator animator;

    Collider[] colliders;

    [Header("Colliders (Se buscan solos del Prefab)")]
    public Collider bodyCollider;
    public Collider hitCollider;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        controller = gameObject.GetComponent<CharacterController>();

        colliders = gameObject.GetComponentsInChildren<Collider>();
        
        foreach(Collider c in colliders)
        {
            if(c.tag == "Body")
            {
                bodyCollider = c;
            }
            else if(c.tag == "Hit")
            {
                hitCollider = c;
            }
        }
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

        // Este IF decide la dirección en la que mira el personaje
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

        // ANIMACIONES

        animator.SetBool("jumping", jumped);

        if (movementInput > 0)
        {
            animator.SetBool("walking", true);
        }
        else if(movementInput < 0)
        {
            animator.SetBool("walkingBack", true);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("walkingBack", false);
        }

        animator.SetBool("blocking", blocked);

        animator.SetBool("quickAttacking", quickAttacked);

        animator.SetBool("slowAttacking", slowAttacked);

        animator.SetBool("lowQuickAttacking", lowQuickAttacked);

        animator.SetBool("lowSlowAttacking", lowSlowAttacked);

        animator.SetBool("Winning", win);

        animator.SetBool("Dying", die);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == bodyCollider/* && collision.gameObject.GetComponent<Collider>().tag == "Hit" && blocked == false*/)
        {
            Debug.Log("Hit");
        }
    }
}