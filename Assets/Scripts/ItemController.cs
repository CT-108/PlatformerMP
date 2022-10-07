using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    Vector2 mouseInput;

    public int ID;
    public int quantity;
    public TextMeshProUGUI quantityText;
    public bool Clicked = false;
    private LevelEditorManager editor;

    void Start()
    {
        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        if (quantity > 0)
        {
            InputAction.CallbackContext ctx = new InputAction.CallbackContext();
            mouseInput = ctx.ReadValue<Vector2>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mouseInput);
            Debug.Log(worldPosition);
            Clicked = true;
            Instantiate(editor.ItemImage[ID], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
            quantity--;
            quantityText.text = quantity.ToString();
            editor.CurrentButtonPressed = ID;
        }
    }

}
