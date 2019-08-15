using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Unit : MonoBehaviour
{
    protected Rigidbody2D rB;
    
    public float health = 10;

    public float attackDamage;
    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rB = GetComponent<Rigidbody2D>();
        UnitAwake();
    }

    protected virtual void UnitAwake() {}

    public Unit() {}
    
    public Unit(float inHealth, float inAttackDamage)
    {
        health = inHealth;
        attackDamage = inAttackDamage;
    }

    public virtual void ApplyDamage(float inDamage)
    {
        health -= inDamage;
        if(health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(this.gameObject);
    }

}
