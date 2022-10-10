using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{

    public GameObject blockManager;

    // Start is called before the first frame update
    void Start()
    {
        blockManager = GameObject.Find("BackgroundManager");
        Debug.Log("salam j'ai spawn");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.down * blockManager.GetComponent<BackroundManager>().blockSpeed * Time.deltaTime;
    }
}
