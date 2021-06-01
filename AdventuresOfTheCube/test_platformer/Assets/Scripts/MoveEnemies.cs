using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemies : Enemies
{
    [Range(0.1f, 3.0f)] public float horizontalMovementDistance = 0.30f;

    private float initialCoinHorizontalPosition;

    protected override void Start()
    {
        initialCoinHorizontalPosition = transform.position.x;
    }

    protected override void Update()
    {
        CalculateCoinHorizontalPosition();
    }
    void CalculateCoinHorizontalPosition()
    {
        float coinVerticalPosition = Mathf
          .Lerp(
            initialCoinHorizontalPosition - (horizontalMovementDistance / 2),
            initialCoinHorizontalPosition + (horizontalMovementDistance / 2),
            Mathf.PingPong(Time.time * 0.5f, 1.5f)
          );

        transform.position = new Vector3(coinVerticalPosition, transform.position.y, transform.position.z);
    }
    

}
