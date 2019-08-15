using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private GameObject playerSpawnPoint;
    [SerializeField] private GameObject playerPrefab = null;
    private Player player;
    [SerializeField] private bool disableSpawning = false;
    private bool isPaused = false;
    private UI ui;

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
        }

        // Disables the spawning for testing
        if(!disableSpawning)
        {
            playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
            player =  Instantiate(playerPrefab, playerSpawnPoint.transform.position, new Quaternion(0,0,0,0)).GetComponent<Player>();
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
        ui = FindObjectOfType<UI>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    private void Pause()
    {
        ui.Pause(isPaused);
        if(!isPaused)
        {
            // Pause
            Time.timeScale = 0.0f;
            isPaused = true;
        }
        else
        {
            // UnPause
            Time.timeScale = 1.0f;
            isPaused = false;
        }
    }

    public Player GetPlayer()
    {
        // Get the player
        return player;
    }

    public UI GetUI()
    {
        // Get the UI
        return ui;
    }
}
