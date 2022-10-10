using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject BlockManager;
    public GameObject BackgroundManager;
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
    int playersCounts = 0;
    bool gameStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        WinCheck();
    }

    public void StartGame()
    {
        playerInputManager.DisableJoining();
        int countID = 0;
        foreach (var item in playerList)
        {
            item.GetComponent<PlayerControler>().hasStarted = true;
            countID++;
            item.GetComponent<PlayerControler>().playerID = countID;
        }
        BlockManager.GetComponent<BlockManager>().gameOn = true;
        BackgroundManager.GetComponent<BackroundManager>().gameOn = true;
        gameStarted = true;
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
                playersCounts++;
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
                    playersCounts--;
                    return;
                }
            }
        }
        else
        {
            playerList.Remove(player);
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

    void WinCheck()
    {
        if (playersCounts == 1 && gameStarted)
        {
            int winner = playerList[0].GetComponent<PlayerControler>().playerID;
            Debug.Log(winner);
        }
    }

 }

