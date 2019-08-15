using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IInventoryItem
{
    GameManager gameManager;
    [SerializeField] private float healAmount;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void PickUp()
    {
        // Heal player nad Destory self
        gameManager.GetPlayer().Heal(healAmount);
        Debug.Log("Healed: " + healAmount);
        Destroy(this.gameObject);
    }
}
