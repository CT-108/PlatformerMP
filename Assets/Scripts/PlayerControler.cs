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
        Debug.Log("cc");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.action.triggered;
    }

    private void FixedUpdate()
    {
        Vector2 m = new Vector2(inputMovement.x * speed, body.velocity.y).normalized;
        body.velocity = m;

        if (isJumping == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            Debug.Log("coucou2");
            isJumping = false;
        }
    }
}
