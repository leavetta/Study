using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Range(0.1f, 3.0f)] public float verticalMovementDistance = 0.30f;
    public float ti = 1;

    private float initialCoinVerticalPosition;

    void Start()
    {
        initialCoinVerticalPosition = transform.position.y;
    }

    void Update()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}
