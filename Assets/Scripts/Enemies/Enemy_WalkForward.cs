using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkForward : Enemy
{
    private Vector3 velocity = Vector3.zero;
    [Range(0,0.3f), SerializeField] private float movementSmoothing = 0.5f;

    protected override void UnitAwake()
    {
        rB = GetComponent<Rigidbody2D>();
        unit = new Unit(10, 20);
    }

    private void FixedUpdate()
    {
        float test = -1 * movementSpeed;
        test = test * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(test * 10f, rB.velocity.y);
		rB.velocity = Vector3.SmoothDamp(rB.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
}
