using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoutreItem : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;
    bool clicked = false;

    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    private void OnMouseOver()
    {
        if (clicked)
        {
            Destroy(this.gameObject);
            editor.ItemButtons[ID].quantity++;
            editor.ItemButtons[ID].quantityText.text = editor.ItemButtons[ID].quantity.ToString();
            clicked = false;
        }
    }

    public void OnClick()
    {
        clicked = true;
    }
}
