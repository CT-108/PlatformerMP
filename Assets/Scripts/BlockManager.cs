using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private float waiter;
    [Space]
    [Header("Speed")]
    [SerializeField] private float fallSpeed;
    [HideInInspector] public float blockSpeed;
    private float speedTimer = 0;
    [Header("Spawner")]
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnTriggerPos;
    Vector3 spawnPos;
    private bool spawnAvailable = true;
    [Space]
    [SerializeField] private List<GameObject> Blocks = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(startWait());

        //Spawner
        if (spawnAvailable)
        {
            int picker = Random.Range(0, Blocks.Count);
            spawnPos = new Vector3(0, spawnHeight, 0);
            GameObject newBlock = Instantiate(Blocks[picker], spawnPos, Quaternion.identity);
            StartCoroutine(spawnTrigger(newBlock));
            StartCoroutine(blockDestroy(newBlock));
            spawnAvailable = false;
        }


        //Speed
        speedTimer += Time.deltaTime;
        float speed = Mathf.Log10(speedTimer);
        if (speed < 0)
            speed = 0;
        blockSpeed = speed * fallSpeed;
        Debug.Log(blockSpeed);
    }

    IEnumerator spawnTrigger(GameObject block)
    {
        yield return new WaitUntil(() => block.transform.position.y <= spawnTriggerPos);
            spawnAvailable = true;
    }

    IEnumerator blockDestroy(GameObject block)
    {
        yield return new WaitUntil(() => block.transform.position.y <= -20);
            Destroy(block, 2);
    }

    IEnumerator startWait()
    {
        yield return new WaitForSeconds(waiter);
    }
}
