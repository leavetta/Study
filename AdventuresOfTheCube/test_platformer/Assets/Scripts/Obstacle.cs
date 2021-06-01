using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerController ch;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ch = collider.GetComponent<PlayerController>();
        if (ch)
        {
            ch.ReceiveDamage();
        }
    }
}
