using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject BlockManager;
    PlayerInputManager playerInputManager;

    [Space]
    public Transform[] SpawnSlots;
    public bool[] AvailableSpawnSlots;

    [Space]
    public Transform[] ReSpawnSlots;
    public bool[] AvailableReSpawnSlots;

    [Space]
    [SerializeField] float respawnTime;

    List <GameObject> playerList = new List <GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        playerInputManager.DisableJoining();
        foreach (var item in playerList)
        {
            item.GetComponent<PlayerControler>().hasStarted = true;
        }
        BlockManager.GetComponent<BlockManager>().gameOn = true;
    }

    public void Spawn(GameObject player)
    {
        for (int i = 0; i < AvailableSpawnSlots.Length; i++)
        {
            if (AvailableSpawnSlots[i] == true)
            {
                player.transform.position = SpawnSlots[i].position;
                AvailableSpawnSlots[i] = false;
                playerList.Add(player);
                return;
            }
        }
    }

    public void Death(GameObject player)
    {
        PlayerControler controler = player.GetComponent<PlayerControler>();

        controler.health--;

        if (controler.health > 0)
        {
            int spawnerIndex = -1;

            for (int i = 0; i < AvailableReSpawnSlots.Length; i++)
            {
                if (AvailableReSpawnSlots[i] == true)
                {
                    player.transform.position = ReSpawnSlots[i].position;
                    AvailableReSpawnSlots[i] = false;
                    spawnerIndex = i;
                    StartCoroutine(respawnProcessor(player, spawnerIndex));
                    return;
                }
            }
        }
        else
        {
            player.SetActive(false);
        }
    }

    IEnumerator respawnProcessor(GameObject player, int spawnerIndex)
    {
        Debug.Log("cc j'suis décédead");
        player.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        player.SetActive(true);
        AvailableReSpawnSlots[spawnerIndex] = true;

    }

 }

