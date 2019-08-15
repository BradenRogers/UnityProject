﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected Unit unit;
    protected Player player;

    [Header("Movements")]
    [SerializeField] protected float movementSpeed = 40.0f;

    protected override void UnitAwake()
    {
        player = gameManager.GetPlayer();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            // Apply damage to player
            otherCollider.collider.GetComponent<Player>().ApplyDamage(unit.attackDamage);
        }
    }
}
