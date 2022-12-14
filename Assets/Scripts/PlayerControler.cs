using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public int health;
    [SerializeField] float deathYThreshold;

    Vector2 inputMovement;

    private bool isJumping = false;
    private Rigidbody2D body;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    [HideInInspector] public bool hasStarted = false;
    int timesJumped;

    [Space]
    [Header("EnvoCheck")]
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    private bool isLedged;

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public int playerID;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    GameObject stickTo;
    Transform tempTrans;


    void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        PlayerManager.instance.Spawn(gameObject);
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log("coucou");
    }

    private void OnDestroy()
    {
        Debug.Break();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (hasStarted == true)
        {
            inputMovement = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.action.triggered;
    }

    public void OnStart()
    {
        PlayerManager.instance.StartGame();
    }

    private void FixedUpdate()
    {
        Vector2 m = new Vector2(inputMovement.x * speed, body.velocity.y);
        body.velocity = m;

        if (m.x > 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Walking", true);
        }
        else if (m.x < 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Walking", false);
        }

        HandleGrounded();
        HandleLedged();
        if (isLedged)
        {
            stickToPlatform();
        }
        else
        {
            RevertParent();
        }

        if (m.x > 0 && !isGrounded)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Jumping", true);
        }
        else if (m.x < 0 && !isGrounded)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Jumping", true);
        }

        if (isJumping == true && timesJumped <= 1)
        {
            Vector2 jump = new Vector2(body.velocity.x, jumpHeight).normalized;
            print(jump);
            //jump.Normalize();
            body.velocity = jump*jumpHeight;
            timesJumped++;
            isJumping = false;
        }

        if (gameObject.transform.position.x < -9.3)
        {
            gameObject.transform.position = new Vector3(9, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.x > 9)
        {
            gameObject.transform.position = new Vector3(-9, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.y < deathYThreshold)
        {
            PlayerManager.instance.Death(gameObject);
        }
    }

    void HandleGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHitY = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeight, groundLayer);

        if (raycastHitY.collider != null)
        {
            isGrounded = true;
            timesJumped = 0;
        }
        else
        {
            isGrounded = false;
        }
    }

    void HandleLedged()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHitX1 = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left, boxCollider2D.bounds.extents.x + extraHeight, groundLayer);
        RaycastHit2D raycastHitX2 = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x + extraHeight, groundLayer);

        if (raycastHitX1.collider != null)
        {
            stickTo = raycastHitX1.transform.gameObject;
            isLedged = true;
            timesJumped = 0;
        }
        else if (raycastHitX2.collider != null)
        {
            stickTo = raycastHitX2.transform.gameObject;
            isLedged = true;
            timesJumped = 0;
        }
        else
        {
            isLedged = false;
        }
    }

    //Make object 2 child of object 1.
    void stickToPlatform()
    {
        GameObject go = new GameObject("sticky");
        go.transform.position = transform.position;
        go.transform.parent = stickTo.transform;
        gameObject.transform.position = go.transform.position;

        //tempTrans = gameObject.transform.parent;
        //gameObject.transform.parent = stickTo.transform;
    }

    //Revert the parent of object 2.
    void RevertParent()
    {
        //gameObject.transform.parent = tempTrans;

    }
}
