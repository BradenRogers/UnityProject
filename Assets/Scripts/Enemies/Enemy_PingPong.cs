using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PingPong : Enemy
{
    [SerializeField] private GameObject[] patrolPoint;
    private int point = 1;

    protected override void UnitAwake()
    {
        rB = GetComponent<Rigidbody2D>();
        unit = new Unit(10, 20);
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, patrolPoint[point].transform.position, Time.deltaTime);
            if(transform.position == patrolPoint[point].transform.position)
            {  
                if(point == 1){point=0;}  
                else if(point == 0){point=1;}  
            }
    }
}
