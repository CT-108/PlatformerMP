using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] int BGLayer;
    public GameObject backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        backgroundManager = GameObject.Find("BackgroundManager");
        Debug.Log("salam j'ai spawn");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (BGLayer == 1)
            transform.position += Vector3.down * backgroundManager.GetComponent<BackroundManager>().blockSpeed * Time.deltaTime;

        if (BGLayer == 2)
            transform.position += Vector3.down * backgroundManager.GetComponent<BackroundManager>().blockSpeed2 * Time.deltaTime;
    }
}
