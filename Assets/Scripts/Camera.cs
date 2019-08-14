using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;

    [SerializeField] private float zOffSet = -10.0f;
    [SerializeField] private float yOffSet = 0.0f;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(player != null)
        {
            this.gameObject.transform.position = new Vector3(player.transform.position.x, yOffSet, zOffSet);
        }
    }
}
