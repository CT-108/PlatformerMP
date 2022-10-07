using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LevelEditorManager : MonoBehaviour
{
    Vector2 mouseInput;

    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;
    bool clicked = false;

    private void Update()
    {
        InputAction.CallbackContext ctx = new InputAction.CallbackContext();
        mouseInput = ctx.ReadValue<Vector2>();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mouseInput);

        if (clicked && ItemButtons[CurrentButtonPressed].Clicked)
        {
            ItemButtons[CurrentButtonPressed].Clicked = false;
            Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
            clicked = false;
        }
    }

    public void OnClick()
    {
        clicked = true;
    }
}
