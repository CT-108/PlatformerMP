using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    Vector2 inputMovement;

    private bool isJumping = false;
    private Rigidbody2D body;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    bool grounded;
    int timesJumped;

    void Awake()
    {

    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log("coucou");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();    
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.action.triggered;
    }

    private void FixedUpdate()
    {
        Vector2 m = new Vector2(inputMovement.x * speed, body.velocity.y);
        body.velocity = m;

        if (isJumping == true)
        {
            Vector2 jump = new Vector2(body.velocity.x, jumpHeight).normalized;
            print(jump);
            //jump.Normalize();
            body.velocity = jump*jumpHeight;
            Debug.Log("coucou2");
            isJumping = false;
        }
    }
}
