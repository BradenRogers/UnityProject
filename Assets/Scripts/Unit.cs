using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Unit : MonoBehaviour
{
    protected Rigidbody2D rB;
    [HideInInspector]
    public float Health {set; get;} = 10;
    [HideInInspector]
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
        Health = inHealth;
        attackDamage = inAttackDamage;
    }

    public virtual void ApplyDamage(float inDamage)
    {
        Health -= inDamage;
        if(Health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(this.gameObject);
    }

}
