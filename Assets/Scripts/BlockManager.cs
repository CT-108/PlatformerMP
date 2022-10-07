using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float fallSpeed;
    [HideInInspector] public float blockSpeed;
    private float speedTimer = 0;
    [Header("Spawner")]
    [SerializeField] private float SpawnRate;
    private float timer = 0;
    [Space]
    [SerializeField] private List<GameObject> Blocks = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        int picker = Random.Range(0, Blocks.Count);
        GameObject newBlock = Instantiate(Blocks[picker]);
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn
        float spawn = SpawnRate - blockSpeed;
        if (spawn < 4)
            spawn = 4;

        if (timer > spawn)
        {
            int picker = Random.Range(0, Blocks.Count);
            GameObject newBlock = Instantiate(Blocks[picker]);
            Destroy(newBlock, 10);
            timer = 0;
        }

        timer += Time.deltaTime;


        //Speed
        speedTimer += Time.deltaTime;
        float speed = Mathf.Log10(speedTimer);
        if (speed < 0)
            speed = 0;
        blockSpeed = speed * fallSpeed;
        Debug.Log(blockSpeed);
    }
}
