using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slam : Enemy
{
    Enemy enemy = new Enemy(float.MaxValue, 25);
    [SerializeField] private LayerMask playerLayer = 1<<0;
    [SerializeField] private float rayCastLength = 5.0f;
    [SerializeField] private float resetTime = 5.0f;
    [SerializeField] private float returnSpeed = 0.01f;
    private Vector3 originalPosistion;

    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
        originalPosistion = transform.position;
    }
    
    private void Update()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, playerLayer))
        {
            rB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            StartCoroutine(ResetTimer());
        }
    }

    IEnumerator ResetTimer()
    {
        yield return new WaitForSecondsRealtime(resetTime);
        rB.constraints |= RigidbodyConstraints2D.FreezePositionY;
        float distance = Vector3.Distance(originalPosistion, transform.position);
        //checks to see if it is at its original posistion with  a little margin for errors
        while(distance > 0.01f)
        {
            
            distance = Vector3.Distance(originalPosistion, transform.position);
            float y = Mathf.LerpUnclamped(transform.position.y, originalPosistion.y, returnSpeed * Time.deltaTime);
            transform.position = new Vector3(originalPosistion.x, y, originalPosistion.z);

            yield return null;
        }
        yield break;
    }

}
