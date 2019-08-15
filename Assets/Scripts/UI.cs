using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image jumpBar;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TextMeshProUGUI coinIndecator;
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

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = player.health / 100.0f;
    }

    public void UpdateJumpBar()
    {
        jumpBar.fillAmount = ((player.jumpForce / player.jumpForceMax) - difference) * 2.5f;
    }

    public void Pause(bool isPaused)
    {
        if(!isPaused)
        {
            // Pause
            pauseScreen.SetActive(true);
        }
        else
        {
            // UnPause
            pauseScreen.SetActive(false);
        }
    }

    public void UpdateCoinUI(int numCoins)
    {
        coinIndecator.text = numCoins.ToString();
    }
}
