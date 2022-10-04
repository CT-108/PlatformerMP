using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> Blocks = new List<GameObject>();
    [SerializeField] private float SpawnRate;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        int picker = Random.Range(0, Blocks.Count);
        GameObject newBlock = Instantiate(Blocks[picker]);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > SpawnRate)
        {
            int picker = Random.Range(0, Blocks.Count);
            GameObject newBlock = Instantiate(Blocks[picker]);
            Destroy(newBlock, 10);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
