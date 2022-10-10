using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundManager : MonoBehaviour
{
    [SerializeField] private float waiter;
    bool start = false;
    [HideInInspector] public bool gameOn = false;
    [Space]
    [Header("Speed")]
    [SerializeField] private float fallSpeedBG;
    [SerializeField] private float fallSpeedBG2;
    [HideInInspector] public float blockSpeed;
    [HideInInspector] public float blockSpeed2;
    private float speedTimer = 0;
    [Header("Spawner")]
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnTriggerPos;
    Vector3 spawnPos;
    private bool spawnAvailable = true;
    private bool spawn2Available = true;
    [Space]
    [SerializeField] private List<GameObject> BG = new List<GameObject>();
    [SerializeField] private List<GameObject> BG2 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(startWait());

        if (start == true)
        {
            Debug.Log("les Bg spawn LEZGO");
            BGManager();
            BG2Manager();
        }
    }

    IEnumerator startWait()
    {
        yield return new WaitUntil(() => gameOn == true);
        yield return new WaitForSeconds(waiter);
        start = true;

    }

    IEnumerator spawnTrigger(GameObject block)
    {
        yield return new WaitUntil(() => block.transform.position.y <= spawnTriggerPos);
        spawnAvailable = true;
    }

    IEnumerator spawnTrigger2(GameObject block)
    {
        yield return new WaitUntil(() => block.transform.position.y <= spawnTriggerPos);
        spawn2Available = true;
    }

    IEnumerator blockDestroy(GameObject block)
    {
        yield return new WaitUntil(() => block.transform.position.y <= -20);
        Destroy(block, 2);
    }

    void BGManager()
    {
        //Spawner
        if (spawnAvailable)
        {
            int picker = Random.Range(0, BG.Count);
            spawnPos = new Vector3(0, spawnHeight, 0);
            GameObject newBlock = Instantiate(BG[picker], spawnPos, Quaternion.identity);
            StartCoroutine(spawnTrigger(newBlock));
            StartCoroutine(blockDestroy(newBlock));
            spawnAvailable = false;
        }

        //Speed
        speedTimer += Time.deltaTime;
        float speed = Mathf.Log10(speedTimer);
        if (speed < 0)
            speed = 0;
        blockSpeed = speed * fallSpeedBG;
    }

    void BG2Manager()
    {
        //Spawner
        if (spawn2Available)
        {
            spawnPos = new Vector3(0, spawnHeight, 0);
            GameObject newBlock = Instantiate(BG2[0], spawnPos, Quaternion.identity);
            StartCoroutine(spawnTrigger2(newBlock));
            StartCoroutine(blockDestroy(newBlock));
            spawn2Available = false;
        }

        //Speed
        speedTimer += Time.deltaTime;
        float speed = Mathf.Log10(speedTimer);
        if (speed < 0)
            speed = 0;
        blockSpeed2 = speed * fallSpeedBG2;
    }
}
