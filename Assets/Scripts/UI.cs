using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image jumpBar;
    private GameManager gameManager;
    private Player player;
    private float difference;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }
    private void Start()
    {
        player = gameManager.GetPlayer();
        difference = player.jumpForce / player.jumpForceMax;
    }

    private void Update()
    {
        healthBar.fillAmount = player.Health / 100;
        jumpBar.fillAmount = ((player.jumpForce) / player.jumpForceMax) - difference;
    }
}
