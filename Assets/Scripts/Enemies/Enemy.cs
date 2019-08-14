using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected Player player;
    Unit unit;

    [Header("Movements")]
    [SerializeField] protected float movementSpeed = 40.0f;

    public Enemy(float inHealth, float inDamage)
    {
        unit = new Unit(inHealth, inDamage);
    }

    public Enemy() {}

    private void Start()
    {
        //player = gameManager.GetPlayer();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            otherCollider.collider.GetComponent<Player>().ApplyDamage(100);
        }
    }
}
