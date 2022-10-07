using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowScript : MonoBehaviour
{
    Vector2 mouseInput;
    bool moving = false;

    void Update()
    {
        if (moving)
        {
            InputAction.CallbackContext ctx = new InputAction.CallbackContext();
            mouseInput = ctx.ReadValue<Vector2>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mouseInput);
            transform.position = worldPosition;
        }
    }

    public void OnMove()
    {
        moving = true;
    }
}
