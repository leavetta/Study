using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemies
{
    [Range(0.1f, 3.0f)] public float verticalMovementDistance = 0.30f;
    public float ti = 1;

    private float initialCoinVerticalPosition;

    protected override void Start()
    {
        initialCoinVerticalPosition = transform.position.y;
    }

    protected override void Update()
    {
        CalculateCoinVerticalPosition();
    }
    void CalculateCoinVerticalPosition()
    {
        float coinVerticalPosition = Mathf
          .Lerp(
            initialCoinVerticalPosition - (verticalMovementDistance / 2),
            initialCoinVerticalPosition + (verticalMovementDistance / 2),
            Mathf.PingPong(Time.time * 0.5f, ti)
          );

        transform.position = new Vector3(transform.position.x, coinVerticalPosition, transform.position.z);
    }
}
