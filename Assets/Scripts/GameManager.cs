using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject playerSpawnPoint;
    [SerializeField] private GameObject playerPrefab = null;
    private Player player;
    [SerializeField] private bool disableSpawning = false;

    private void Awake()
    {   
        if(!disableSpawning)
        {
            playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
            Instantiate(playerPrefab, playerSpawnPoint.transform.position, new Quaternion(0,0,0,0));
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        
    }

    public Player GetPlayer()
    {
        return player;
    }
}
