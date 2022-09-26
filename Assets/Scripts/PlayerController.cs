using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerControls controls;

    Vector2 move;

    private bool isJumping = false;
    private Rigidbody2D body;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Debug.Log("coucou");
    }

    private void FixedUpdate()
    {
        Vector2 m = new Vector2(move.x * speed, body.velocity.y);
        body.velocity = m;

        if (isJumping == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            Debug.Log("coucou2");
            isJumping = false;
        }
    }



    public void Jump()
    {
        isJumping = true;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

}
