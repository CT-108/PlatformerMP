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

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;

    GameObject stickTo;
    Transform tempTrans;


    void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        PlayerManager.instance.Spawn(gameObject);
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log("coucou");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (hasStarted == true)
            inputMovement = context.ReadValue<Vector2>();    
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
        Debug.Log(isGrounded);
        Vector2 m = new Vector2(inputMovement.x * speed, body.velocity.y);
        body.velocity = m;

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
        tempTrans = gameObject.transform.parent;
        gameObject.transform.parent = stickTo.transform;
    }

    //Revert the parent of object 2.
    void RevertParent()
    {
        gameObject.transform.parent = tempTrans;

    }
}
